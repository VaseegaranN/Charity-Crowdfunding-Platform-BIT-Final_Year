# UnityFunds — remaining phases implementation notes

## Implemented in this delivery
- Password hashing service using ASP.NET Core `PasswordHasher<string>`; successful legacy plaintext logins are upgraded in-place. New donor/NGO/logistics signups hash passwords. Admin login also upgrades a legacy password after a successful login.
- Antiforgery validation is enabled globally for MVC unsafe methods. Ensure every POST form includes the MVC form tag helper or `@Html.AntiForgeryToken()`.
- Session cookies are HttpOnly, SameSite=Lax, Secure=Always; response security headers and a fixed-window rate limiter are enabled.
- Campaign creation for signed-in NGOs; new campaigns are Pending until an administrator review workflow is connected. Public listing/details show Approved campaigns only.
- Donation intents with amount validation and unique references. **No money is captured**: payment records remain Pending until a real provider is configured and its signed webhook is verified.
- Admin analytics for successful donation totals, pending payment intents, campaign counts and recent donation records.
- SQL Server indexes and EF Core migration for campaign and donation tables.
- Dockerfile and Compose deployment scaffold.

## Required before production
1. Install .NET 10 SDK and run `dotnet restore`, `dotnet build -c Release`, `dotnet test` (add tests), then run against a disposable SQL Server database.
2. Review existing database column lengths. The password hasher output must fit the existing `*_pwd` / `admin_pass` columns; enlarge them to `nvarchar(512)` before deploying if they are shorter. Backup database first.
3. Existing Gobi/Sathy/government user password flows are not yet converted to the shared password service. Their tables need the same column-capacity audit and login migration.
4. Existing admin management views must include antiforgery tokens on every POST. Destructive NGO/government delete actions were changed to POST; update corresponding forms to POST.
5. Add admin campaign moderation (approve/reject) and ownership/audit logging before launch. Campaigns currently stay Pending by design.
6. Integrate a PCI-compliant gateway (e.g. Razorpay/Stripe as supported in your market). Never store card number, CVV, PIN or OTP. Verify webhook signatures server-side, use idempotency keys, and only then mark donations Succeeded and increment campaign totals in a transaction.
7. Move connection strings and secrets to environment variables/secret manager. Do not commit production credentials. Enforce HTTPS at the reverse proxy and configure forwarded headers correctly.
8. Add automated tests for login, password upgrade, authorization, campaign approval, donation amount validation, webhook signature verification, concurrency and SQL migrations.
9. Add database backup/restore procedures, monitoring, structured logs, alerting, retention policy and privacy/consent review.

## Local setup
- Requires .NET 10 SDK and SQL Server.
- Set `ConnectionStrings__DefaultConnection` (or edit Development settings locally).
- From `UnityFunds.Web`: `dotnet restore`, `dotnet ef database update`, `dotnet run`.
- EF migrations currently run at application startup. For production, prefer applying reviewed migrations as a controlled deployment step and disable automatic migration at app startup.

## Important scope note
This is a source-code modernization pass, not a verified production release. The current execution environment did not have the .NET SDK or SQL Server, so compilation, migrations against your actual schema, end-to-end tests, and deployment could not be run here. Payment collection is intentionally disabled rather than simulated.
