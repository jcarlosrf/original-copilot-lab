## [t3] Scaffold .NET 8 Blazor project
- The target path must contain the project file directly: `dotnet new blazor --name ...` alone created a nested project directory, so use `--output .` from the prepared output folder.
- A second template invocation left duplicate Razor components and top-level `Program.cs`; removing only that generated nested directory restored a clean build.
- `global.json` correctly resolves SDK 8.0.426, but a fresh terminal may not inherit `%USERPROFILE%\.dotnet` in PATH. Use the activation commands from `t2-devops.md` before CLI commands.
- The default template starts successfully at an HTTP URL; its HTTPS redirection warning is expected when no HTTPS endpoint is configured.
- Learnings consumed: (none)