## [t3] Scaffold .NET 8 Blazor project
- The target path must contain the project file directly: `dotnet new blazor --name ...` alone created a nested project directory, so use `--output .` from the prepared output folder.
- A second template invocation left duplicate Razor components and top-level `Program.cs`; removing only that generated nested directory restored a clean build.
- `global.json` correctly resolves SDK 8.0.426, but a fresh terminal may not inherit `%USERPROFILE%\.dotnet` in PATH. Use the activation commands from `t2-devops.md` before CLI commands.
- The default template starts successfully at an HTTP URL; its HTTPS redirection warning is expected when no HTTPS endpoint is configured.
- - Learnings consumed: (none)

## [t4] Implement phase-one UI and navigation
- `t1-ux.md` is the governing UI contract: five native routes, English labels, reused CSS/assets, and no business-flow behavior yet.
- The product page reads the four copied JSON files in fixed catalog order using `JsonDocument`; clone each root element before disposing its document.
- Keep the React app untouched; the existing Vitest suite passed after adding the Blazor sibling UI.
- Stop the running Blazor executable before rebuilding, since the Windows apphost locks `bin/Debug/net8.0/ECommAppBlazor.exe`.
- Learnings consumed: [frontend/blazor-dotnet8-scaffold]