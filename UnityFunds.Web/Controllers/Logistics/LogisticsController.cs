using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Data;
using UnityFunds.Web.Models;
using UnityFunds.Web.Services;
using UnityFunds.Web.Models.ViewModels;

namespace UnityFunds.Web.Controllers.Logistics;

public class LogisticsController : Controller
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwords;
    public LogisticsController(AppDbContext context, PasswordService passwords) { _context = context; _passwords = passwords; }
    private const long MaxLogisticsImageSize = 5 * 1024 * 1024; // 5 MB

    public IActionResult Signup()
    {
        return View(new LogisticsSignupViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signup(LogisticsSignupViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var email = model.logistics_user_email.Trim().ToLowerInvariant();
        var username = model.logistics_user_name.Trim();

        var accountExists = await _context.LogisticsDetails.AnyAsync(x =>
            x.logistics_user_email.ToLower() == email ||
            x.logistics_user_name.ToLower() == username.ToLower());

        if (accountExists)
        {
            ModelState.AddModelError(
                nameof(model.logistics_user_email),
                "An account with this email or username already exists.");

            return View(model);
        }

        var file = model.logistics_id_image;

        if (file == null || file.Length == 0 ||
            file.Length > MaxLogisticsImageSize)
        {
            ModelState.AddModelError(
                nameof(model.logistics_id_image),
                "Please upload a valid image smaller than 5 MB.");

            return View(model);
        }

        // Read the file only after checking its size.
        await using var ms = new MemoryStream();
        await file.CopyToAsync(ms);

        var imageBytes = ms.ToArray();

        // Validate the actual file signature.
        bool isJpeg =
            imageBytes.Length >= 3 &&
            imageBytes[0] == 0xFF &&
            imageBytes[1] == 0xD8 &&
            imageBytes[2] == 0xFF;

        bool isPng =
            imageBytes.Length >= 8 &&
            imageBytes[0] == 0x89 &&
            imageBytes[1] == 0x50 &&
            imageBytes[2] == 0x4E &&
            imageBytes[3] == 0x47 &&
            imageBytes[4] == 0x0D &&
            imageBytes[5] == 0x0A &&
            imageBytes[6] == 0x1A &&
            imageBytes[7] == 0x0A;

        if (!isJpeg && !isPng)
        {
            ModelState.AddModelError(
                nameof(model.logistics_id_image),
                "Only valid JPG, JPEG or PNG images are allowed.");

            return View(model);
        }

        var logistics = new LogisticsDetails
        {
            logistics_id = Guid.NewGuid().ToString("N")[..12],

            logistics_user_name = username,
            logistics_user_position = model.logistics_user_position.Trim(),
            logistics_user_phone = model.logistics_user_phone.Trim(),
            logistics_user_email = email,
            logistics_user_pwd = _passwords.Hash(model.logistics_user_pwd),

            logistics_org_name = model.logistics_org_name.Trim(),
            logistics_org_place = model.logistics_org_place.Trim(),
            logistics_org_phone = model.logistics_org_phone.Trim(),
            logistics_org_mail = model.logistics_org_mail.Trim(),

            logistics_id_image = imageBytes,
            logistics_account_req_status = "Pending",

            childhood_pet = model.childhood_pet,
            vacation_destination = model.vacation_destination,
            favourite_hobby = model.favourite_hobby
        };

        _context.LogisticsDetails.Add(logistics);
        await _context.SaveChangesAsync();

        ViewBag.Message =
            "Registration successful! Account approval is pending.";

        return View(new LogisticsSignupViewModel());
    }

    public IActionResult Login() => View();

    [HttpPost]
        [EnableRateLimiting("auth")]
    public async Task<IActionResult> Login(string email, string password)
    {
        email = (email ?? string.Empty).Trim();
        password = password ?? string.Empty;

        var log = await _context.LogisticsDetails.FirstOrDefaultAsync(x => (x.logistics_user_email == email || x.logistics_user_name == email));
        if (log == null || !_passwords.VerifyAndUpgrade(log.logistics_user_pwd, password, out var upgradedPassword)) { ViewBag.Error = "Invalid credentials."; return View(); }
        if (log.logistics_user_pwd != upgradedPassword) { log.logistics_user_pwd = upgradedPassword; await _context.SaveChangesAsync(); }

        if (!string.Equals(log.logistics_account_req_status, "Approved", StringComparison.OrdinalIgnoreCase))
        {
            ViewBag.Error = "Your account is not approved yet. Please contact the administrator.";
            return View();
        }

        HttpContext.Session.SetString("LogisticsId", log.logistics_id);
        HttpContext.Session.SetString("LogisticsName", log.logistics_user_name);
        return RedirectToAction("Landing");
    }

    public async Task<IActionResult> Landing()
    {
        var logisticsId = HttpContext.Session.GetString("LogisticsId");
        if (string.IsNullOrEmpty(logisticsId)) return RedirectToAction("Login");

        var logistics = await _context.LogisticsDetails.FindAsync(logisticsId);
        if (logistics == null) return RedirectToAction("Login");

        ViewBag.Logistics = logistics;
        ViewBag.Posts = await _context.PublicPosts
            .Where(p => p.pp_user_id == logisticsId)
            .OrderByDescending(p => p.pp_time)
            .ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(string content, string? postType)
    {
        var logisticsId = HttpContext.Session.GetString("LogisticsId");
        if (string.IsNullOrEmpty(logisticsId)) return RedirectToAction("Login");

        var logistics = await _context.LogisticsDetails.FindAsync(logisticsId);
        if (logistics == null) return RedirectToAction("Login");

        if (!string.IsNullOrWhiteSpace(content))
        {
            _context.PublicPosts.Add(new PublicPost
            {
                pp_id = Guid.NewGuid().ToString("N")[..12],
                pp_user_id = logisticsId,
                pp_username = logistics.logistics_user_name,
                pp_userphone = logistics.logistics_user_phone,
                pp_userloc = logistics.logistics_org_place,
                pp_date = DateTime.Now.ToString("yyyy-MM-dd"),
                pp_type = string.IsNullOrWhiteSpace(postType) ? "General" : postType,
                pp_userpost_description = content.Trim(),
                pp_media = "",
                pp_time = DateTime.Now
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
