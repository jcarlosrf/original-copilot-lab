# Blazor Phase-One UX Scope

For the React-to-Blazor rewrite, phase one reproduces routes and presentation while business actions remain inert until their assigned migration phase.

## What Happened
In original-copilot-lab task t1, the five existing routes included UI controls for cart, review, login, checkout, and sale behavior. Clarification explicitly deferred those business flows, while requiring the existing CSS, images, catalog JSON, and five-page experience to be reused.

## Takeaway
Preserve the controls and page composition for visual parity, but explicitly specify no data mutation, authentication, modal submission, checkout result, or promotion state in the UI-only phase. Keep `/admin` directly routable because the phase does not implement auth gating; only the existing “Admin Login” link targets `/login`.

## History
- 2026-10-22 (original-copilot-lab/t1): initial
