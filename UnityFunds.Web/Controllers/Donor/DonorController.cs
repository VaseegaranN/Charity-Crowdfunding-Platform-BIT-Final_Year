using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Data;
using UnityFunds.Web.Models;
using UnityFunds.Web.Services;

namespace UnityFunds.Web.Controllers.Donor;

public class DonorController : Controller
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwords;
    public DonorController(AppDbContext context, PasswordService passwords) { _context = context; _passwords = passwords; }

    public IActionResult Signup() => View();

    [HttpPost]
    public async Task<IActionResult> Signup(DonorDetails model)
    {
        model.donor_user_pwd = _passwords.Hash(model.donor_user_pwd);
        _context.DonorDetails.Add(model);
        await _context.SaveChangesAsync();
        return RedirectToAction("Login");
    }

    public IActionResult Login() => View();

    [HttpPost]
        [EnableRateLimiting("auth")]
    public async Task<IActionResult> Login(string email, string password)
    {
        email = (email ?? string.Empty).Trim();
        password = (password ?? string.Empty).Trim();

        var donor = await _context.DonorDetails.FirstOrDefaultAsync(x => (x.donor_user_email == email || x.donor_user_name == email));
        if (donor == null || !_passwords.VerifyAndUpgrade(donor.donor_user_pwd, password, out var upgradedPassword)) { ViewBag.Error = "Invalid credentials."; return View(); }
        if (donor.donor_user_pwd != upgradedPassword) { donor.donor_user_pwd = upgradedPassword; await _context.SaveChangesAsync(); }

        HttpContext.Session.SetInt32("DonorId", donor.donor_id);
        HttpContext.Session.SetString("DonorName", donor.donor_user_name);
        return RedirectToAction("Landing");
    }

    public async Task<IActionResult> Landing()
    {
        var donorId = HttpContext.Session.GetInt32("DonorId");
        if (donorId == null) return RedirectToAction("Login");

        var donor = await _context.DonorDetails.FindAsync(donorId.Value);
        if (donor == null) return RedirectToAction("Login");

        ViewBag.Donor = donor;
        ViewBag.Posts = await _context.PublicPosts
            .Where(p => p.pp_user_id == donorId.Value.ToString())
            .OrderByDescending(p => p.pp_time)
            .ToListAsync();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost(string content, string? postType)
    {
        var donorId = HttpContext.Session.GetInt32("DonorId");
        if (donorId == null) return RedirectToAction("Login");

        var donor = await _context.DonorDetails.FindAsync(donorId.Value);
        if (donor == null) return RedirectToAction("Login");

        if (!string.IsNullOrWhiteSpace(content))
        {
            _context.PublicPosts.Add(new PublicPost
            {
                pp_id = Guid.NewGuid().ToString("N")[..12],
                pp_user_id = donorId.Value.ToString(),
                pp_username = donor.donor_user_name,
                pp_userphone = donor.donor_user_phone,
                pp_userloc = string.Join(", ", new[] { donor.donor_user_city, donor.donor_user_state }
                    .Where(s => !string.IsNullOrWhiteSpace(s))),
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
