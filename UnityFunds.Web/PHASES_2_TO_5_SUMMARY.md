# UnityFunds — Phases 2–5

| Phase | Delivered source changes | Validation status |
|---|---|---|
| 2 Security/authentication | Password hashing and legacy-upgrade helper for donor/NGO/logistics/admin, antiforgery, secure session cookie, security headers, login rate limiting | Static review only; .NET build unavailable |
| 3 Crowdfunding | Campaign entity, NGO campaign submission, public campaign browsing/details, donor donation intents | Gateway not connected; donations stay Pending |
| 4 Data/analytics | EF migration, SQL indexes, admin analytics view | Migration not executed against target DB |
| 5 QA/deployment | Dockerfile, Compose, deployment checklist and release gates | Not production-certified; tests/build/deploy pending |

Read `UnityFunds.Web/DEPLOYMENT_AND_SECURITY.md` for critical production steps.
