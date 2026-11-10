# UnityFunds Full UI/UX Refresh

## What changed
- Added `wwwroot/css/unityfunds-refresh.css`, a shared visual system for the MVC application.
- Linked the shared stylesheet into standalone Razor pages and the shared layout.
- Applied a consistent forest-green / cream / lime visual identity, typography, form controls, cards, tables, buttons, status badges, and responsive layouts.
- Restyled donor, NGO, logistics and admin login screens without changing their form field names or POST actions.
- Restyled donor, NGO and logistics signup screens while retaining their existing model-bound fields and multipart upload forms.
- Restyled donor, NGO and logistics dashboards, admin dashboard/management screens, campaign listing/detail/create/review and analytics pages.
- Kept existing controllers, action routes, model bindings, form submissions and database code unchanged.

## Screens covered
- Public: Home, Privacy, campaign index, campaign details
- Donor: Login, Signup, Landing
- NGO: Login, Signup, Landing
- Logistics: Login, Signup, Landing
- Admin: Login, Landing, User Management, NGO/Government management and updates, NGO/Logistics account requests, Public Help, post management, Campaign Review, Analytics
- Shared: application layout, validation and error views

## Important scope and verification
This deliverable is a comprehensive visual refresh of the existing MVC screens, not a claim that every business workflow has been newly implemented. Existing controller functionality is preserved rather than replaced. Payment processing remains subject to payment-provider configuration.

No .NET build or live browser test was run in this environment. After extracting, run:
```powershell
cd .\UnityFunds.Web
dotnet restore
dotnet build
dotnet run
```
Check each role's login, signup and dashboard routes using your development database. The migration issue discussed separately is not altered by this UI-only refresh.
