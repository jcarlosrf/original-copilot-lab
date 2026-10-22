# Blazor Interactive Server Events

Enable the Interactive Server render mode on the route tree before wiring UI event handlers to scoped demo services.

## What Happened
In `original-copilot-lab` task t13, the project already registered `AddInteractiveServerComponents`, but `Routes.razor` had no render mode. Adding `@rendermode InteractiveServer` enabled `@onclick`, form submit and bindings while preserving the scoped catalog/cart services from t12. Launching the compiled DLL directly from the test output also omitted the generated scoped stylesheet bundle in the browser smoke run.

## Takeaway
Confirm both service registration and component render mode when Blazor event handlers appear inert. Use the regular project launch path for browser asset verification; an isolated test output may not serve the generated `*.styles.css` bundle.

## History
- 2026-10-22 (original-copilot-lab/t13): initial