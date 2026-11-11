using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Data;
using UnityFunds.Web.Models;
using UnityFunds.Web.Models.ViewModels;
namespace UnityFunds.Web.Controllers;

public class CampaignsController(AppDbContext db) : Controller
{
    public async Task<IActionResult> Index(string? category, string? q)
    {
        var query = db.Campaigns.AsNoTracking().Where(c => c.Status == "Approved");
        if (!string.IsNullOrWhiteSpace(category)) query = query.Where(c => c.Category == category);
        if (!string.IsNullOrWhiteSpace(q)) query = query.Where(c => c.Title.Contains(q) || c.Description.Contains(q));
        ViewBag.Query = q; ViewBag.Category = category;
        return View(await query.OrderByDescending(c => c.CreatedAtUtc).ToListAsync());
    }
    public async Task<IActionResult> Details(int id) { var c = await db.Campaigns.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Status == "Approved"); return c == null ? NotFound() : View(c); }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var ngoId = HttpContext.Session.GetString("NgoId");

        if (string.IsNullOrWhiteSpace(ngoId))
            return RedirectToAction("Login", "Ngo");

        var ngo = await db.NgoDetails.FindAsync(ngoId);

        if (ngo == null ||
            !string.Equals(
                ngo.ngo_acc_req_status,
                "Approved",
                StringComparison.OrdinalIgnoreCase))
        {
            return RedirectToAction("Login", "Ngo");
        }

        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CampaignCreateViewModel model)
    {
        var ngoId = HttpContext.Session.GetString("NgoId");

        if (string.IsNullOrWhiteSpace(ngoId))
            return Unauthorized();

        var ngo = await db.NgoDetails.FindAsync(ngoId);

        if (ngo == null ||
            !string.Equals(
                ngo.ngo_acc_req_status,
                "Approved",
                StringComparison.OrdinalIgnoreCase))
        {
            return Unauthorized();
        }

        if (!ModelState.IsValid)
            return View(model);

        var campaign = new Campaign
        {
            Title = model.Title.Trim(),
            Description = model.Description.Trim(),
            Category = model.Category.Trim(),
            GoalAmount = model.GoalAmount,
            ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl)
                ? null
                : model.ImageUrl.Trim(),

            NgoId = ngo.ngo_id,
            NgoName = ngo.ngo_org_name,
            Status = "Pending",
            RaisedAmount = 0,
            CreatedAtUtc = DateTime.UtcNow
        };

        db.Campaigns.Add(campaign);
        await db.SaveChangesAsync();

        TempData["CampaignMessage"] =
            "Your campaign has been submitted for admin approval.";

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Donate(int campaignId, decimal amount)
    {
        var donorId = HttpContext.Session.GetInt32("DonorId");

        if (donorId == null)
            return RedirectToAction("Login", "Donor");

        // Verify that the donor still exists.
        var donorExists = await db.DonorDetails
            .AnyAsync(d => d.donor_id == donorId.Value);

        if (!donorExists)
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Donor");
        }

        // Only approved campaigns can receive donation intents.
        var campaign = await db.Campaigns.FirstOrDefaultAsync(
            c => c.Id == campaignId && c.Status == "Approved");

        if (campaign == null)
            return NotFound();

        // Accept amounts with no more than two decimal places.
        if (amount < 1 ||
            amount > 1000000 ||
            amount != decimal.Round(amount, 2))
        {
            TempData["DonationError"] =
                "Donation must be between ₹1 and ₹10,00,000 with up to two decimal places.";

            return RedirectToAction(nameof(Details), new { id = campaignId });
        }

        var donation = new Donation
        {
            CampaignId = campaignId,
            DonorId = donorId.Value,
            Amount = amount,
            Status = "Pending",
            PaymentReference = Guid.NewGuid().ToString("N"),
            PaymentProvider = "NotConfigured",
            CreatedAtUtc = DateTime.UtcNow,
            PaidAtUtc = null
        };

        db.Donations.Add(donation);

        await db.SaveChangesAsync();

        TempData["DonationInfo"] =
            "Donation intent recorded. Payment has not been collected yet.";

        return RedirectToAction(nameof(Details), new { id = campaignId });
    }
}
