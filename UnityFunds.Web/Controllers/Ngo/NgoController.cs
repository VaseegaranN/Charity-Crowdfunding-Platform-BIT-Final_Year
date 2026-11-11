using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Data;
using UnityFunds.Web.Models;
using UnityFunds.Web.Services;
using UnityFunds.Web.Models.ViewModels;

namespace UnityFunds.Web.Controllers.Ngo;

public class NgoController : Controller
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwords;
    public NgoController(AppDbContext context, PasswordService passwords) { _context = context; _passwords = passwords; }

    
    private const long MaxNgoImageSize = 5 * 1024 * 1024; // 5 MB

    public IActionResult Signup()
    {
        return View(new NgoSignupViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signup(NgoSignupViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var email = model.ngo_user_email.Trim().ToLowerInvariant();

        var emailExists = await _context.NgoDetails.AnyAsync(
            x => x.ngo_user_email.ToLower() == email);

        if (emailExists)
        {
            ModelState.AddModelError(
                nameof(model.ngo_user_email),
                "An account with this email already exists.");

            return View(model);
        }

        // Validate uploaded image size.
        if (model.ngo_id_image == null ||
            model.ngo_id_image.Length == 0 ||
            model.ngo_id_image.Length > MaxNgoImageSize)
        {
            ModelState.AddModelError(
                nameof(model.ngo_id_image),
                "Please upload an image smaller than 5 MB.");

            return View(model);
        }

        // Validate actual file signature, not just the filename.
        byte[] header = new byte[8];

        await using (var stream = model.ngo_id_image.OpenReadStream())
        {
            int bytesRead = await stream.ReadAsync(header.AsMemory());

            bool isJpeg =
                bytesRead >= 3 &&
                header[0] == 0xFF &&
                header[1] == 0xD8 &&
                header[2] == 0xFF;

            bool isPng =
                bytesRead >= 8 &&
                header[0] == 0x89 &&
                header[1] == 0x50 &&
                header[2] == 0x4E &&
                header[3] == 0x47 &&
                header[4] == 0x0D &&
                header[5] == 0x0A &&
                header[6] == 0x1A &&
                header[7] == 0x0A;

            if (!isJpeg && !isPng)
            {
                ModelState.AddModelError(
                    nameof(model.ngo_id_image),
                    "Only valid JPG, JPEG or PNG images are allowed.");

                return View(model);
            }

            stream.Position = 0;

            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            var ngo = new NgoDetails
            {
                ngo_id = Guid.NewGuid().ToString("N")[..12],
                ngo_user_name = model.ngo_user_name.Trim(),
                ngo_user_position = model.ngo_user_position.Trim(),
                ngo_user_phone = model.ngo_user_phone.Trim(),
                ngo_user_email = email,
                ngo_user_pwd = _passwords.Hash(model.ngo_user_pwd),

                ngo_org_name = model.ngo_org_name.Trim(),
                ngo_org_place = model.ngo_org_place.Trim(),
                ngo_org_phone = model.ngo_org_phone.Trim(),
                ngo_org_mail = model.ngo_org_mail.Trim(),

                ngo_id_image = ms.ToArray(),
                ngo_acc_req_status = "Pending",

                childhood_pet = model.childhood_pet,
                vacation_destination = model.vacation_destination,
                favourite_hobby = model.favourite_hobby
            };

            _context.NgoDetails.Add(ngo);
            await _context.SaveChangesAsync();
        }

        ViewBag.Message =
            "Registration successful! Account approval is pending.";

        return View(new NgoSignupViewModel());
    }

    public IActionResult Login() => View();

    [HttpPost]
        [EnableRateLimiting("auth")]
    public async Task<IActionResult> Login(string email, string password)
    {
        email = (email ?? string.Empty).Trim();
        password = password ?? string.Empty;

        var ngo = await _context.NgoDetails.FirstOrDefaultAsync(x => (x.ngo_user_email == email || x.ngo_user_name == email));
        if (ngo == null || !_passwords.VerifyAndUpgrade(ngo.ngo_user_pwd, password, out var upgradedPassword)) { ViewBag.Error = "Invalid credentials."; return View(); }
        if (ngo.ngo_user_pwd != upgradedPassword) { ngo.ngo_user_pwd = upgradedPassword; await _context.SaveChangesAsync(); }

        if (!string.Equals(ngo.ngo_acc_req_status, "Approved", StringComparison.OrdinalIgnoreCase))
        {
            ViewBag.Error = "Your account is not approved yet. Please contact the administrator.";
            return View();
        }

        HttpContext.Session.SetString("NgoId", ngo.ngo_id);
        HttpContext.Session.SetString("NgoName", ngo.ngo_user_name);
        return RedirectToAction("Landing");
    }

    public async Task<IActionResult> Landing()
    {
        var ngoId = HttpContext.Session.GetString("NgoId");
        if (string.IsNullOrEmpty(ngoId)) return RedirectToAction("Login");

        var ngo = await _context.NgoDetails.FindAsync(ngoId);
        if (ngo == null) return RedirectToAction("Login");

        ViewBag.Ngo = ngo;
        ViewBag.Posts = await _context.NgoPostDetails
            .Where(p => p.ngo_user_id == ngoId)
            .OrderByDescending(p => p.ngo_post_time)
            .ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(string content, string? postType)
    {
        var ngoId = HttpContext.Session.GetString("NgoId");
        if (string.IsNullOrEmpty(ngoId)) return RedirectToAction("Login");

        var ngo = await _context.NgoDetails.FindAsync(ngoId);
        if (ngo == null) return RedirectToAction("Login");

        if (!string.IsNullOrWhiteSpace(content))
        {
            _context.NgoPostDetails.Add(new NgoPostDetail
            {
                ngo_post_id = Guid.NewGuid().ToString("N")[..12],
                ngo_user_id = ngoId,
                ngo_user_name = ngo.ngo_user_name,
                ngo_user_email = ngo.ngo_user_email,
                ngo_user_phone = ngo.ngo_user_phone,
                ngo_post_date = DateTime.Now.ToString("yyyy-MM-dd"),
                ngo_post_type = string.IsNullOrWhiteSpace(postType) ? "General" : postType,
                ngo_post_desc = content.Trim(),
                ngo_post_media = "",
                ngo_post_time = DateTime.Now
            });
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Landing");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }
}
