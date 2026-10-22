# Blazor JSON Catalog Presentation

Read the local catalog JSON directly for the presentation-only phase, then replace the temporary JSON shape with the planned domain model in phase two.

## What Happened
In `original-copilot-lab` task t4, the Blazor Products page read the four copied files under `wwwroot/products` in a fixed array order. `JsonDocument.RootElement.Clone()` was needed so the page could retain each element after disposing its document. A running Windows apphost also locked its output executable and prevented a rebuild.

## Takeaway
Use `IWebHostEnvironment.WebRootPath` and `System.Text.Json` for this UI-only catalog presentation; do not introduce domain models ahead of the planned model/service task. Stop the local Blazor process before rebuilding the Windows apphost.

## History
- 2026-10-22 (original-copilot-lab/t4): initial