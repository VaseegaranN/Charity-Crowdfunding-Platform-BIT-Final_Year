using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Data;
using UnityFunds.Web.Models;
using UnityFunds.Web.Services;

namespace UnityFunds.Web.Controllers.Ngo;

public class NgoController : Controller
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwords;
    public NgoController(AppDbContext context, PasswordService passwords) { _context = context; _passwords = passwords; }

    public IActionResult Signup() => View();

    [HttpPost]
    public async Task<IActionResult> Signup(NgoDetails model, IFormFile? ngo_id_image)
    {
        if (ngo_id_image != null && ngo_id_image.Length > 0)
        {
            using var ms = new MemoryStream();
            await ngo_id_image.CopyToAsync(ms);
            model.ngo_id_image = ms.ToArray();
        }
        model.ngo_acc_req_status = "Pending";
        model.ngo_user_pwd = _passwords.Hash(model.ngo_user_pwd);
        model.ngo_id = Guid.NewGuid().ToString("N")[..12];
        _context.NgoDetails.Add(model);
        await _context.SaveChangesAsync();
        ViewBag.Message = "Registration successful! Account Approval is Pending.";
        return View();
    }

    public IActionResult Login() => View();

    [HttpPost]
        [EnableRateLimiting("auth")]
    public async Task<IActionResult> Login(string email, string password)
    {
        email = (email ?? string.Empty).Trim();
        password = (password ?? string.Empty).Trim();

        var ngo = await _context.NgoDetails.FirstOrDefaultAsync(x => (x.ngo_user_email == email || x.ngo_user_name == email));
        if (ngo == null || !_passwords.VerifyAndUpgrade(ngo.ngo_user_pwd, password, out var upgradedPassword)) { ViewBag.Error = "Invalid credentials."; return View(); }
        if (ngo.ngo_user_pwd != upgradedPassword) { ngo.ngo_user_pwd = upgradedPassword; await _context.SaveChangesAsync(); }

        if (ngo.ngo_acc_req_status == "Pending" || ngo.ngo_acc_req_status == "Rejected")
        {
            ViewBag.Error = "Your account request is pending or rejected. Please wait for Approval.";
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
