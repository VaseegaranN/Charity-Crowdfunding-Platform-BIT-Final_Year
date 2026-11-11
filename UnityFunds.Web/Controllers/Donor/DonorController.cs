using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Data;
using UnityFunds.Web.Models;
using UnityFunds.Web.Services;
using UnityFunds.Web.Models.ViewModels;

namespace UnityFunds.Web.Controllers.Donor;

public class DonorController : Controller
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwords;
    public DonorController(AppDbContext context, PasswordService passwords) { _context = context; _passwords = passwords; }

    public IActionResult Signup()
    {
        return View(new DonorSignupViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Signup(DonorSignupViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var email = model.donor_user_email.Trim().ToLowerInvariant();
        var username = model.donor_user_name.Trim();

        var alreadyExists = await _context.DonorDetails.AnyAsync(d =>
            d.donor_user_email.ToLower() == email ||
            d.donor_user_name.ToLower() == username.ToLower());

        if (alreadyExists)
        {
            ModelState.AddModelError(
                nameof(model.donor_user_email),
                "An account with this email or username already exists.");

            return View(model);
        }

        var donor = new DonorDetails
        {
            donor_user_name = username,
            donor_user_email = email,
            donor_user_phone = model.donor_user_phone.Trim(),
            donor_user_gender = model.donor_user_gender,
            donor_user_street = model.donor_user_street.Trim(),
            donor_user_city = model.donor_user_city.Trim(),
            donor_user_state = model.donor_user_state.Trim(),
            donor_user_zip = model.donor_user_zip.Trim(),
            donor_user_country = model.donor_user_country.Trim(),

            donor_user_pwd = _passwords.Hash(model.donor_user_pwd),

            fundingType = model.fundingType,
            paymentType = model.paymentType,

            childhood_pet = model.childhood_pet,
            vacation_destination = model.vacation_destination,
            favourite_hobby = model.favourite_hobby,

            donationAmount = "-",
            upiId = "-"
        };

        _context.DonorDetails.Add(donor);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Login));
    }

    public IActionResult Login() => View();

    [HttpPost]
        [EnableRateLimiting("auth")]
    public async Task<IActionResult> Login(string email, string password)
    {
        email = (email ?? string.Empty).Trim();
        password = password ?? string.Empty;

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
