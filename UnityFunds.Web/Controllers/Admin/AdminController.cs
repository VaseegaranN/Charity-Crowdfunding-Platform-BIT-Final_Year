using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UnityFunds.Web.Data;
using UnityFunds.Web.Services;

namespace UnityFunds.Web.Controllers.Admin;

public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly PasswordService _passwords;
    private const int PageSize = 15;

    public AdminController(AppDbContext context, PasswordService passwords) { _context = context; _passwords = passwords; }

    // Protect every admin endpoint by default. Only the login page and login POST
    // are anonymous; checking the session in one place avoids forgotten guards.
    public override void OnActionExecuting(Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext context)
    {
        var action = context.ActionDescriptor.RouteValues["action"];
        if (!string.Equals(action, nameof(Login), StringComparison.OrdinalIgnoreCase)
            && HttpContext.Session.GetInt32("AdminId") is null)
        {
            context.Result = RedirectToAction(nameof(Login));
            return;
        }

        base.OnActionExecuting(context);
    }

    public IActionResult Login() => View();

    [HttpPost]
        [EnableRateLimiting("auth")]
    public async Task<IActionResult> Login(string username, string user_email, string password)
    {
        var admin = await _context.AdminDetails.FirstOrDefaultAsync(a => a.admin_name == username && a.admin_email == user_email);
        if (admin == null || !_passwords.VerifyAndUpgrade(admin.admin_pass, password, out var upgradedPassword)) { ViewBag.Error = "Invalid credentials."; return View(); }
        if (admin.admin_pass != upgradedPassword) { admin.admin_pass = upgradedPassword; await _context.SaveChangesAsync(); }

        HttpContext.Session.SetInt32("AdminId", admin.admin_id);
        HttpContext.Session.SetString("AdminName", admin.admin_name);
        HttpContext.Session.SetString("AdminEmail", admin.admin_email);
        HttpContext.Session.SetString("AdminPhone", admin.admin_phone_number);
        return RedirectToAction("Landing");
    }

    public async Task<IActionResult> Landing()
    {
        ViewBag.Donors = await _context.DonorDetails.CountAsync();
        ViewBag.Ngos = await _context.NgoDetails.CountAsync();
        ViewBag.Logistics = await _context.LogisticsDetails.CountAsync();
        ViewBag.GovStaff = await _context.GovernmentStaffDetails.CountAsync();
        ViewBag.PublicPosts = await _context.PublicPosts.CountAsync();
        ViewBag.AdminPosts = await _context.AdminPosts.CountAsync();
        ViewBag.NgoPosts = await _context.NgoPostDetails.CountAsync();
        ViewBag.GovnPosts = await _context.GovernmentPosts.CountAsync();
        ViewBag.PendingNgo = await _context.NgoDetails.CountAsync(n => n.ngo_acc_req_status == "Pending");
        ViewBag.PendingLogistics = await _context.LogisticsDetails.CountAsync(l => l.logistics_account_req_status == "Pending");
        ViewBag.Questions = await _context.PublicContactHelp.CountAsync();
        ViewBag.RecentQuestions = await _context.PublicContactHelp
            .OrderByDescending(q => q.pch_id).Take(5).ToListAsync();
        return View();
    }

    public async Task<IActionResult> CampaignReview()
    {
        return View(await _context.Campaigns.AsNoTracking().OrderBy(c => c.CreatedAtUtc).ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> ReviewCampaign(int id, string decision)
    {
        var campaign = await _context.Campaigns.FindAsync(id);
        if (campaign == null) return NotFound();
        if (decision is not ("Approved" or "Rejected")) return BadRequest();
        campaign.Status = decision;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(CampaignReview));
    }

    public async Task<IActionResult> Analytics()
    {
        ViewBag.TotalDonations = await _context.Donations.Where(d => d.Status == "Succeeded").SumAsync(d => (decimal?)d.Amount) ?? 0m;
        ViewBag.PendingPayments = await _context.Donations.CountAsync(d => d.Status == "Pending");
        ViewBag.ActiveCampaigns = await _context.Campaigns.CountAsync(c => c.Status == "Approved");
        ViewBag.PendingCampaigns = await _context.Campaigns.CountAsync(c => c.Status == "Pending");
        ViewBag.DonationCount = await _context.Donations.CountAsync(d => d.Status == "Succeeded");
        ViewBag.RecentDonations = await _context.Donations.OrderByDescending(d => d.CreatedAtUtc).Take(10).ToListAsync();
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> ManageAllPosts(string option = "public_posts", int page = 1)
    {
        ViewBag.SelectedOption = option;
        ViewBag.CurrentPage = page;

        int totalPosts = option switch
        {
            "public_posts" => await _context.PublicPosts.CountAsync(),
            "ngo_post_details" => await _context.NgoPostDetails.CountAsync(),
            "admin_posts" => await _context.AdminPosts.CountAsync(),
            "govn_posts" => await _context.GovernmentPosts.CountAsync(),
            _ => 0
        };

        ViewBag.TotalPages = Math.Max(1, (int)Math.Ceiling(totalPosts / (double)PageSize));

        var rows = new List<object>();

        switch (option)
        {
            case "public_posts":
                rows = (await _context.PublicPosts.OrderByDescending(p => p.pp_time)
                        .Skip((page - 1) * PageSize).Take(PageSize).ToListAsync())
                    .Select(p => (object)new { Id = p.pp_id, User = p.pp_username, Type = p.pp_type, Description = p.pp_userpost_description, Date = p.pp_date })
                    .ToList();
                break;
            case "ngo_post_details":
                rows = (await _context.NgoPostDetails.OrderByDescending(p => p.ngo_post_time)
                        .Skip((page - 1) * PageSize).Take(PageSize).ToListAsync())
                    .Select(p => (object)new { Id = p.ngo_post_id, User = p.ngo_user_name, Type = p.ngo_post_type, Description = p.ngo_post_desc, Date = p.ngo_post_date })
                    .ToList();
                break;
            case "admin_posts":
                rows = (await _context.AdminPosts.OrderByDescending(p => p.ap_time)
                        .Skip((page - 1) * PageSize).Take(PageSize).ToListAsync())
                    .Select(p => (object)new { Id = p.ap_id, User = p.ap_admin_name, Type = p.ap_type, Description = p.ap_desc, Date = p.ap_date })
                    .ToList();
                break;
            case "govn_posts":
                rows = (await _context.GovernmentPosts.OrderByDescending(p => p.gp_time)
                        .Skip((page - 1) * PageSize).Take(PageSize).ToListAsync())
                    .Select(p => (object)new { Id = p.gp_id, User = p.gp_staff_name, Type = p.gp_type, Description = p.gp_desc, Date = p.gp_date })
                    .ToList();
                break;
        }

        ViewBag.Posts = rows;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> DeletePost(string table, string id_column, string id)
    {
        switch (table)
        {
            case "public_posts": var pp = await _context.PublicPosts.FindAsync(id); if (pp != null) _context.PublicPosts.Remove(pp); break;
            case "ngo_post_details": var np = await _context.NgoPostDetails.FindAsync(id); if (np != null) _context.NgoPostDetails.Remove(np); break;
            case "admin_posts": var ap = await _context.AdminPosts.FindAsync(id); if (ap != null) _context.AdminPosts.Remove(ap); break;
            case "govn_posts": var gp = await _context.GovernmentPosts.FindAsync(id); if (gp != null) _context.GovernmentPosts.Remove(gp); break;
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(ManageAllPosts));
    }

    public async Task<IActionResult> NgoManage(int page = 1)
    {
        page = Math.Max(1, page);
        var total = await _context.NgoDetails.CountAsync();
        ViewBag.TotalPages = (int)Math.Ceiling(total / (double)PageSize);
        ViewBag.CurrentPage = page;
        var ngos = await _context.NgoDetails.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();
        return View(ngos);
    }

    public async Task<IActionResult> NgoUpdate(string id)
    {
        var ngo = await _context.NgoDetails.FindAsync(id);
        if (ngo == null) return NotFound();
        return View(ngo);
    }

    [HttpPost]
    public async Task<IActionResult> NgoUpdate(Models.NgoDetails model)
    {
        var existing = await _context.NgoDetails.FindAsync(model.ngo_id);
        if (existing == null) return NotFound();
        existing.ngo_user_name = model.ngo_user_name;
        existing.ngo_user_position = model.ngo_user_position;
        existing.ngo_user_phone = model.ngo_user_phone;
        existing.ngo_user_email = model.ngo_user_email;
        existing.ngo_org_name = model.ngo_org_name;
        existing.ngo_org_place = model.ngo_org_place;
        existing.ngo_org_phone = model.ngo_org_phone;
        existing.ngo_org_mail = model.ngo_org_mail;
        existing.ngo_acc_req_status = model.ngo_acc_req_status;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(NgoManage));
    }

    [HttpPost]
    public async Task<IActionResult> NgoDelete(string id)
    {
        var ngo = await _context.NgoDetails.FindAsync(id);
        if (ngo != null) { _context.NgoDetails.Remove(ngo); await _context.SaveChangesAsync(); }
        return RedirectToAction(nameof(NgoManage));
    }

    public async Task<IActionResult> GovnManage(int page = 1)
    {
        page = Math.Max(1, page);
        var total = await _context.GovernmentStaffDetails.CountAsync();
        ViewBag.TotalPages = (int)Math.Ceiling(total / (double)PageSize);
        ViewBag.CurrentPage = page;
        var staff = await _context.GovernmentStaffDetails.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();
        return View(staff);
    }

    public async Task<IActionResult> GovnUpdate(string id)
    {
        var staff = await _context.GovernmentStaffDetails.FindAsync(id);
        if (staff == null) return NotFound();
        return View(staff);
    }

    [HttpPost]
    public async Task<IActionResult> GovnUpdate(Models.GovernmentStaffDetails model)
    {
        var existing = await _context.GovernmentStaffDetails.FindAsync(model.govn_staff_id);
        if (existing == null) return NotFound();
        // Never overwrite existing credentials with empty form values.
        if (string.IsNullOrWhiteSpace(model.govn_staff_password))
            model.govn_staff_password = existing.govn_staff_password;
        if (string.IsNullOrWhiteSpace(model.govn_staff_mpin))
            model.govn_staff_mpin = existing.govn_staff_mpin;

        _context.Entry(existing).CurrentValues.SetValues(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(GovnManage));
    }

    [HttpPost]
    public async Task<IActionResult> GovnDelete(string id)
    {
        var staff = await _context.GovernmentStaffDetails.FindAsync(id);
        if (staff != null) { _context.GovernmentStaffDetails.Remove(staff); await _context.SaveChangesAsync(); }
        return RedirectToAction(nameof(GovnManage));
    }

    public async Task<IActionResult> UserManagement()
    {
        ViewBag.GobiUsers = await _context.GobiUsers.ToListAsync();
        ViewBag.SathyUsers = await _context.SathyUsers.ToListAsync();
        return View();
    }

    public async Task<IActionResult> UserUpdate(string table, int id)
    {
        if (table == "gobi_users")
        {
            var user = await _context.GobiUsers.FindAsync(id);
            if (user == null) return NotFound();
            ViewBag.Table = "gobi_users";
            return View(user);
        }
        else if (table == "sathy_users")
        {
            var user = await _context.SathyUsers.FindAsync(id);
            if (user == null) return NotFound();
            ViewBag.Table = "sathy_users";
            return View(user);
        }
        return RedirectToAction(nameof(UserManagement));
    }

    [HttpPost]
    public async Task<IActionResult> UserUpdate(string table, Models.GobiUser gobiModel, Models.SathyUser sathyModel)
    {
        if (table == "gobi_users")
        {
            var existing = await _context.GobiUsers.FindAsync(gobiModel.gobi_user_id);
            if (existing == null) return NotFound();
            if (string.IsNullOrWhiteSpace(gobiModel.gobi_user_password))
                gobiModel.gobi_user_password = existing.gobi_user_password;
            if (string.IsNullOrWhiteSpace(gobiModel.gobi_user_mpin))
                gobiModel.gobi_user_mpin = existing.gobi_user_mpin;
            _context.Entry(existing).CurrentValues.SetValues(gobiModel);
        }
        else if (table == "sathy_users")
        {
            var existing = await _context.SathyUsers.FindAsync(sathyModel.sathy_user_id);
            if (existing == null) return NotFound();
            if (string.IsNullOrWhiteSpace(sathyModel.sathy_user_password))
                sathyModel.sathy_user_password = existing.sathy_user_password;
            if (string.IsNullOrWhiteSpace(sathyModel.sathy_user_mpin))
                sathyModel.sathy_user_mpin = existing.sathy_user_mpin;
            _context.Entry(existing).CurrentValues.SetValues(sathyModel);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(UserManagement));
    }

    public async Task<IActionResult> UserDelete(string table, int id)
    {
        if (table == "gobi_users")
        {
            var user = await _context.GobiUsers.FindAsync(id);
            if (user != null) { _context.GobiUsers.Remove(user); await _context.SaveChangesAsync(); }
        }
        else if (table == "sathy_users")
        {
            var user = await _context.SathyUsers.FindAsync(id);
            if (user != null) { _context.SathyUsers.Remove(user); await _context.SaveChangesAsync(); }
        }
        return RedirectToAction(nameof(UserManagement));
    }

    public async Task<IActionResult> PublicHelp(int page = 1)
    {
        page = Math.Max(1, page);
        var total = await _context.PublicContactHelp.CountAsync();
        ViewBag.TotalPages = (int)Math.Ceiling(total / (double)PageSize);
        ViewBag.CurrentPage = page;
        var items = await _context.PublicContactHelp.Skip((page - 1) * PageSize).Take(PageSize).ToListAsync();
        return View(items);
    }

    public async Task<IActionResult> PublicHelpUpdate(int id)
    {
        var item = await _context.PublicContactHelp.FindAsync(id);
        if (item == null) return NotFound();
        return View(item);
    }

    [HttpPost]
    public async Task<IActionResult> PublicHelpUpdate(Models.PublicContactHelp model)
    {
        var existing = await _context.PublicContactHelp.FindAsync(model.pch_id);
        if (existing == null) return NotFound();
        existing.pch_admin_reply = model.pch_admin_reply;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(PublicHelp));
    }

    public async Task<IActionResult> PublicHelpDelete(int id)
    {
        var item = await _context.PublicContactHelp.FindAsync(id);
        if (item != null) { _context.PublicContactHelp.Remove(item); await _context.SaveChangesAsync(); }
        return RedirectToAction(nameof(PublicHelp));
    }

    public async Task<IActionResult> NgoAccountRequests()
    {
        var pending = await _context.NgoDetails.Where(n => n.ngo_acc_req_status == "Pending").ToListAsync();
        return View(pending);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveNgo(string id, string status)
    {
        var ngo = await _context.NgoDetails.FindAsync(id);
        if (ngo != null) { ngo.ngo_acc_req_status = status; await _context.SaveChangesAsync(); }
        return RedirectToAction(nameof(NgoAccountRequests));
    }

    public async Task<IActionResult> LogisticsAccountRequests()
    {
        var pending = await _context.LogisticsDetails.Where(l => l.logistics_account_req_status == "Pending").ToListAsync();
        return View(pending);
    }

    [HttpPost]
    public async Task<IActionResult> ApproveLogistics(string id, string status)
    {
        var log = await _context.LogisticsDetails.FindAsync(id);
        if (log != null) { log.logistics_account_req_status = status; await _context.SaveChangesAsync(); }
        return RedirectToAction(nameof(LogisticsAccountRequests));
    }
}
