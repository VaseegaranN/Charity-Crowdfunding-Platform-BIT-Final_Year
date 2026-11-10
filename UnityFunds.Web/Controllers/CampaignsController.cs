using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Data;
using UnityFunds.Web.Models;
namespace UnityFunds.Web.Controllers;
public class CampaignsController(AppDbContext db) : Controller {
 public async Task<IActionResult> Index(string? category, string? q) {
  var query=db.Campaigns.AsNoTracking().Where(c=>c.Status=="Approved");
  if(!string.IsNullOrWhiteSpace(category)) query=query.Where(c=>c.Category==category);
  if(!string.IsNullOrWhiteSpace(q)) query=query.Where(c=>c.Title.Contains(q)||c.Description.Contains(q));
  ViewBag.Query=q; ViewBag.Category=category;
  return View(await query.OrderByDescending(c=>c.CreatedAtUtc).ToListAsync());
 }
 public async Task<IActionResult> Details(int id) { var c=await db.Campaigns.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id&&x.Status=="Approved"); return c==null?NotFound():View(c); }
 [HttpGet] public IActionResult Create()=>HttpContext.Session.GetString("NgoId")==null?RedirectToAction("Login","Ngo"):View();
 [HttpPost] public async Task<IActionResult> Create(Campaign model) {
  var ngoId=HttpContext.Session.GetString("NgoId"); if(string.IsNullOrWhiteSpace(ngoId)) return Unauthorized();
  var ngo=await db.NgoDetails.FindAsync(ngoId); if(ngo==null) return Unauthorized();
  if(!ModelState.IsValid||model.GoalAmount<=0){ModelState.AddModelError(nameof(model.GoalAmount),"Enter a fundraising goal greater than zero.");return View(model);}
  model.Id=0; model.NgoId=ngo.ngo_id; model.NgoName=ngo.ngo_org_name; model.Status="Pending"; model.RaisedAmount=0; model.CreatedAtUtc=DateTime.UtcNow;
  db.Campaigns.Add(model); await db.SaveChangesAsync(); return RedirectToAction(nameof(Index));
 }
 [HttpPost] public async Task<IActionResult> Donate(int campaignId, decimal amount) {
  var donorId=HttpContext.Session.GetInt32("DonorId"); if(donorId==null)return RedirectToAction("Login","Donor");
  var campaign=await db.Campaigns.FirstOrDefaultAsync(c=>c.Id==campaignId&&c.Status=="Approved"); if(campaign==null)return NotFound();
  if(amount<1||amount>1000000){TempData["DonationError"]="Donation must be between ₹1 and ₹10,00,000.";return RedirectToAction(nameof(Details),new{id=campaignId});}
  db.Donations.Add(new Donation{CampaignId=campaignId,DonorId=donorId.Value,Amount=decimal.Round(amount,2),Status="Pending",PaymentProvider="NotConfigured"});
  await db.SaveChangesAsync();
  TempData["DonationInfo"]="Donation intent recorded. Payment is not collected yet; the platform owner must configure and verify a payment gateway.";
  return RedirectToAction(nameof(Details),new{id=campaignId});
 }
}
