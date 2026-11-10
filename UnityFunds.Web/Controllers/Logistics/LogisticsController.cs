using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Data;
using UnityFunds.Web.Models;
using UnityFunds.Web.Services;

namespace UnityFunds.Web.Controllers.Logistics;

public class LogisticsController : Controller
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwords;
    public LogisticsController(AppDbContext context, PasswordService passwords) { _context = context; _passwords = passwords; }

    public IActionResult Signup() => View();

    [HttpPost]
    public async Task<IActionResult> Signup(LogisticsDetails model, IFormFile? logistics_id_image)
    {
        if (logistics_id_image != null && logistics_id_image.Length > 0)
        {
            using var ms = new MemoryStream();
            await logistics_id_image.CopyToAsync(ms);
            model.logistics_id_image = ms.ToArray();
        }
        model.logistics_account_req_status = "Pending";
        model.logistics_user_pwd = _passwords.Hash(model.logistics_user_pwd);
        model.logistics_id = Guid.NewGuid().ToString("N")[..12];
        _context.LogisticsDetails.Add(model);
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

        var log = await _context.LogisticsDetails.FirstOrDefaultAsync(x => (x.logistics_user_email == email || x.logistics_user_name == email));
        if (log == null || !_passwords.VerifyAndUpgrade(log.logistics_user_pwd, password, out var upgradedPassword)) { ViewBag.Error = "Invalid credentials."; return View(); }
        if (log.logistics_user_pwd != upgradedPassword) { log.logistics_user_pwd = upgradedPassword; await _context.SaveChangesAsync(); }

        if (log.logistics_account_req_status == "Pending" || log.logistics_account_req_status == "Rejected")
        {
            ViewBag.Error = "Your account request is pending or rejected. Please wait for Approval.";
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
