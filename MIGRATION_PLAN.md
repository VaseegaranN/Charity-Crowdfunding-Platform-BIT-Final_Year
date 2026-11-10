# Migration Plan: PHP/MySQL → C#/SQL Server (ASP.NET Core 8 MVC)

## Overview

Convert the UnityFunds crowdfunding platform from raw PHP + MySQL to ASP.NET Core 8 MVC + Entity Framework Core + SQL Server.

---

## Target Stack

| Component | Old | New |
|-----------|-----|-----|
| Language | PHP 7/8 | C# 12 (.NET 8) |
| Framework | None (raw PHP) | ASP.NET Core 8 MVC |
| Database | MySQL (`crowdfund`) | SQL Server |
| ORM | mysqli / PDO | Entity Framework Core |
| Frontend | Inline HTML+CSS+JS | Razor Views + existing CSS |
| Auth | PHP Sessions | ASP.NET Core Identity |

---

## Project Structure

```
UnityFunds/
├── UnityFunds.sln
├── UnityFunds.Web/                    # Main MVC project
│   ├── Program.cs                     # App entry, DI config
│   ├── appsettings.json               # SQL Server connection string
│   ├── Models/                        # EF Core entity classes
│   │   ├── Admin.cs
│   │   ├── Donor.cs
│   │   ├── Ngo.cs
│   │   ├── LogisticsPartner.cs
│   │   ├── GovernmentStaff.cs
│   │   ├── AdminPost.cs
│   │   ├── PublicPost.cs
│   │   ├── NgoPostDetail.cs
│   │   ├── GovernmentPost.cs
│   │   ├── PublicContactHelp.cs
│   │   ├── UserReport.cs
│   │   └── ...
│   ├── Data/
│   │   └── AppDbContext.cs            # EF Core DbContext
│   ├── Controllers/
│   │   ├── HomeController.cs          # Landing page
│   │   ├── Admin/
│   │   │   ├── AdminController.cs         # Login, landing
│   │   │   ├── AdminProfileController.cs  # Profile management
│   │   │   ├── AdminPostController.cs     # Create/manage posts
│   │   │   ├── AdminNgoController.cs      # NGO management
│   │   │   ├── AdminGovnController.cs     # Government staff mgmt
│   │   │   ├── AdminUserController.cs     # User management
│   │   │   ├── AdminHelpController.cs     # Public help/questions
│   │   │   ├── AdminReportsController.cs  # User reports
│   │   │   └── AdminAccountReqController.cs # Approval requests
│   │   ├── Donor/
│   │   │   ├── DonorController.cs         # Signup, login, landing
│   │   ├── Ngo/
│   │   │   ├── NgoController.cs           # Signup, login, landing
│   │   ├── Logistics/
│   │   │   ├── LogisticsController.cs     # Signup, login, landing
│   ├── Views/
│   │   ├── Shared/
│   │   │   ├── _Layout.cshtml             # Common layout
│   │   │   └── _ValidationScriptsPartial.cshtml
│   │   ├── Home/
│   │   │   └── Index.cshtml               # Landing page (from index.php)
│   │   ├── Admin/
│   │   │   ├── Login.cshtml
│   │   │   ├── Landing.cshtml
│   │   │   ├── ProfileManage.cshtml
│   │   │   ├── PostCreate.cshtml
│   │   │   ├── PostManage.cshtml
│   │   │   ├── NgoManage.cshtml
│   │   │   ├── NgoUpdate.cshtml
│   │   │   ├── GovnManage.cshtml
│   │   │   ├── GovnUpdate.cshtml
│   │   │   ├── UserManagement.cshtml
│   │   │   ├── UserProfileUpdate.cshtml
│   │   │   ├── PublicHelp.cshtml
│   │   │   ├── PublicHelpUpdate.cshtml
│   │   │   ├── ReportsManage.cshtml
│   │   │   ├── NgoAccountRequests.cshtml
│   │   │   └── LogisticsAccountRequests.cshtml
│   │   ├── Donor/
│   │   │   ├── Signup.cshtml
│   │   │   ├── Login.cshtml
│   │   │   └── Landing.cshtml
│   │   ├── Ngo/
│   │   │   ├── Signup.cshtml
│   │   │   ├── Login.cshtml
│   │   │   └── Landing.cshtml
│   │   └── Logistics/
│   │       ├── Signup.cshtml
│   │       ├── Login.cshtml
│   │       └── Landing.cshtml
│   ├── wwwroot/
│   │   ├── css/                        # Existing CSS files (copied)
│   │   │   ├── site.css
│   │   │   ├── admin_landing_style.css
│   │   │   ├── donor_style.css
│   │   │   ├── ngo_style.css
│   │   │   └── logistics_style.css
│   │   ├── images/                     # Existing images (copied)
│   │   └── js/
│   │       └── site.js
│   └── Migrations/                     # EF Core auto-generated
└── UnityFunds.Data/                    # Class library (optional separation)
    ├── UnityFunds.Data.csproj
    ├── Models/
    └── AppDbContext.cs
```

---

## Database Tables (SQL Server)

Derived from all SQL queries in the PHP codebase:

| Table | Key Columns | Source Files |
|-------|-------------|--------------|
| `AdminDetails` | admin_id, admin_name, admin_email, admin_phone_number, admin_pass, admin_profile_picture | admin_login, admin_prof_manage |
| `DonorDetails` | donor_id, full_name, email, password, phone, address, donation_type, donation_amount, card_number, expiry_date, cvv, security_question, security_answer | donor-signup |
| `NgoDetails` | ngo_id, user_name, user_email, user_phone, org_name, org_email, org_phone, org_address, password, ngo_id_proof, status (pending/approved/rejected), security_question, security_answer | ngo_signup, ngo_login, ngo_acc_req |
| `LogisticsDetails` | logistics_id, user_name, user_email, user_phone, org_name, org_email, org_phone, org_address, password, logistics_id_proof, status (pending/approved/rejected), security_question, security_answer | logistics_signup, logistics_login, logistics_acc_req |
| `GovernmentStaff` | govn_id, govn_name, govn_email, govn_phone, govn_password, govn_position, govn_department | admin_govn_manage, admin_govn_update_sep |
| `AdminPosts` | post_id, username, content, media, created_at | admin_post_creation, admin_manage_post |
| `PublicPosts` | post_id, username, content, media, created_at | admin_manageall_post |
| `NgoPostDetails` | post_id, username, content, media, created_at | admin_manageall_post |
| `GovernmentPosts` | post_id, username, content, media, created_at | admin_manageall_post |
| `PublicContactHelp` | id, name, email, subject, question, answer | admin_public_help, admin_pch_update_sep |
| `GobiUsers` | id, name, email, phone, ... | admin_user_management |
| `SathyUsers` | id, name, email, phone, ... | admin_user_management |
| `UserReports` | id, reporter, reported_user, reason, status | admin_manage_userreports |

---

## Implementation Phases

### Phase 1: Project Scaffolding (~15 min)
- [ ] `dotnet new mvc -n UnityFunds.Web`
- [ ] Add NuGet packages: `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Tools`
- [ ] Configure SQL Server connection string in `appsettings.json`
- [ ] Create `AppDbContext.cs` with all DbSets
- [ ] Create all Model classes from table schemas above

### Phase 2: Core Infrastructure (~10 min)
- [ ] `Program.cs` — Configure services, middleware, session
- [ ] Migrations: `Add-Migration InitialCreate` → `Update-Database`
- [ ] Copy images to `wwwroot/images/`
- [ ] Copy CSS files to `wwwroot/css/`
- [ ] Create `_Layout.cshtml` with navigation (matching existing nav structure)

### Phase 3: Landing Page (~5 min)
- [ ] Port `index.php` → `Views/Home/Index.cshtml`
- [ ] Link existing `style.css`
- [ ] Port footer JS for year update

### Phase 4: Admin Module (~30 min)
- [ ] **Admin Login** — `AdminController.Login()` with session management
- [ ] **Admin Landing** — Facebook-style feed page
- [ ] **Profile Management** — CRUD with image upload
- [ ] **Post Creation** — File upload + insert
- [ ] **Post Management** — List/filter/paginate across 4 post tables
- [ ] **NGO Management** — CRUD table with pagination
- [ ] **NGO Update** — Detail edit form
- [ ] **Government Staff Management** — CRUD table
- [ ] **Government Staff Update** — Detail edit form
- [ ] **User Management** — Table with search
- [ ] **User Profile Update** — Detail edit form
- [ ] **Public Help** — Questions CRUD
- [ ] **Public Help Update** — Answer form
- [ ] **Reports Management** — Block/unblock/delete
- [ ] **NGO Account Requests** — Approve/reject
- [ ] **Logistics Account Requests** — Approve/reject

### Phase 5: Donor Module (~10 min)
- [ ] **Donor Signup** — Registration form with payment fields
- [ ] **Donor Login** — Session-based auth
- [ ] **Donor Landing** — Side navigation layout

### Phase 6: NGO Module (~10 min)
- [ ] **NGO Signup** — Registration with org details + ID proof upload
- [ ] **NGO Login** — Session auth with account status check
- [ ] **NGO Landing** — Side navigation layout

### Phase 7: Logistics Module (~10 min)
- [ ] **Logistics Signup** — Registration with org details + ID proof upload
- [ ] **Logistics Login** — Session auth with account status check
- [ ] **Logistics Landing** — Side navigation layout

---

## Session/Auth Mapping

| PHP Session Key | C# Session Key | Controller |
|-----------------|----------------|------------|
| `$_SESSION['admin_id']` | `HttpContext.Session.GetInt32("AdminId")` | AdminController |
| `$_SESSION['admin_name']` | `HttpContext.Session.GetString("AdminName")` | AdminController |
| `$_SESSION['admin_email']` | `HttpContext.Session.GetString("AdminEmail")` | AdminController |
| `$_SESSION['sv_admin_id']` | `HttpContext.Session.GetInt32("AdminId")` | AdminProfileController |
| `$_SESSION['ngo_id']` | `HttpContext.Session.GetInt32("NgoId")` | NgoController |
| `$_SESSION['logistics_id']` | `HttpContext.Session.GetInt32("LogisticsId")` | LogisticsController |
| `$_SESSION['donor_id']` | `HttpContext.Session.GetInt32("DonorId")` | DonorController |

---

## Total Files to Create

| Type | Count |
|------|-------|
| Models (.cs) | ~14 |
| Controllers (.cs) | ~10 |
| Views (.cshtml) | ~22 |
| CSS (copied) | 5 |
| Images (copied) | 15 |
| Config/Program files | 3 |
| **Total** | **~69** |

---

## Estimated Time: 1.5–2 hours
