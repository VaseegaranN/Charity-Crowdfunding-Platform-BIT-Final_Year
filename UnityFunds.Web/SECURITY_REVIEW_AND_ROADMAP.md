# UnityFunds modernization review

## Current baseline

The repository contains a legacy PHP/MySQL application and an in-progress ASP.NET Core MVC / SQL Server application. The MVC project is the selected modernization target. Existing donor, NGO, logistics, government-staff, admin, post and help models are retained for compatibility while the domain is mapped before schema changes.

## Changes in this first pass

- Replaced the legacy landing page with a responsive UnityFunds landing experience while preserving existing login and registration routes.
- Added accessible mobile navigation, keyboard-operable dropdowns, responsive layouts and reduced-motion handling.
- Added page metadata and cache-busting for the main stylesheet.
- Hardened the session cookie (HTTP-only, secure-only, SameSite=Lax) and reduced idle lifetime to 30 minutes.
- Removed automatic creation of the known `admin / admin123` account. Admin accounts must be provisioned through a controlled process.
- Added a centralized session gate to admin controller actions so the dashboard and management endpoints are not anonymous by default.
- Normalized admin list pagination to reject page values below 1.

## High-priority issues found; do not deploy publicly until addressed

1. **Plain-text passwords:** donor, NGO, logistics and admin login queries compare submitted passwords directly to database values. Introduce ASP.NET Core `PasswordHasher<TUser>` or Identity, migrate existing credentials safely, and never log or return password values.
2. **Payment-card data:** the donor model contains card number, expiry and CVV fields. CVV must never be stored. Remove raw card data from application/database flows and use a PCI-compliant payment provider token; perform a reviewed data migration and secure deletion of historical sensitive values.
3. **Admin account provisioning:** the default seed credential has been removed. Add a one-time, operator-controlled bootstrap command that hashes a strong password and cannot be invoked remotely.
4. **Authorization consistency:** NGO, logistics and donor endpoints currently rely on per-action session checks. Replace these with a unified authentication/authorization system and role policies, then test every route.
5. **State-changing GET endpoints:** admin delete operations are currently implemented as GET actions. Convert them to POST with antiforgery validation and confirmation UI.
6. **Input validation:** several registration and update actions save posted models without consistently checking `ModelState`. Add dedicated view models, validation attributes, length limits and server-side allow-lists.
7. **Auditability and privacy:** add audit events for approvals, account changes and donation lifecycle actions; minimize personal data and define retention rules.
8. **Database migration safety:** review the existing SQL schema and migrations against production data before altering keys, column types or constraints. Back up the database and test migrations on a restored copy.

## Delivery phases

### Phase 1 — Foundation and experience
- Responsive landing page and shared design system (started).
- Normalize layouts, navigation, validation and accessibility across donor, NGO, logistics and admin screens.
- Establish baseline build, smoke tests and database backup procedure.

### Phase 2 — Security and identity
- Password hashing and credential migration.
- Role-based authorization and secure admin bootstrap.
- Antiforgery protection, POST-only state changes, validation and rate limiting.
- Remove sensitive card fields; integrate payment-provider tokens only.

### Phase 3 — Crowdfunding domain
- Campaign lifecycle, categories, funding goals, progress calculations and campaign discovery.
- Donation records, receipts, payment status and donor history.
- NGO verification, moderation and transparent impact updates.
- Logistics fulfilment and delivery status for physical donations.

### Phase 4 — Data and performance
- Review entity relationships, indexes, constraints and query plans.
- Add pagination/filtering, async query consistency, structured logging and health checks.
- Add reporting dashboards and export controls with role checks.

### Phase 5 — Quality and release
- Unit, integration, authorization and UI tests.
- Accessibility, responsive and security review.
- Deployment configuration, secrets management, backups and rollback plan.

## Build environment note

The current execution environment does not have the .NET SDK installed, so compilation and automated tests could not be run here. Build and test the solution with the .NET SDK matching `net10.0` before merging or deploying these changes.
