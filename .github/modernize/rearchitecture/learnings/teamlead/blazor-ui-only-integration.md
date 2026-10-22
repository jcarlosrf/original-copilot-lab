# Blazor UI-Only Integration

For a Blazor UI-only phase with no external services, exercise the actual ASP.NET Core host and static assets over HTTP instead of waiving integration or adding a test framework solely for the phase.

## What Happened

In `original-copilot-lab` task t8.1, a missing approved strategy caused conformance to fail even though browser routes and React regression passed. The Blazor app had no external services or .NET test harness. A live Kestrel HTTP contract was selected to verify route rendering and packaged assets as host integration, while keeping Playwright as the independent browser tier.

## Takeaway

State explicitly that integration is applicable and required, list host-level assertions, and do not call process startup or a root-page 200 sufficient. Keep browser E2E independent; no Docker infrastructure is needed without external dependencies. Add maintained service/unit integration tests when business services arrive in later phases.

## History
- 2026-10-22 (original-copilot-lab/t8.1): initial