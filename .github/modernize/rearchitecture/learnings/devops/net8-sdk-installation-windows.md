# .NET 8 SDK Installation on Windows

When the machine-wide dotnet installer does not expose the requested SDK, install it in the user profile and explicitly pin the project SDK.

## What Happened
For original-copilot-lab task t2, the MSI installer reported success twice, but neither `dotnet --list-sdks` nor `C:\Program Files\dotnet\sdk` showed .NET 8. The official `dotnet-install.ps1 -Channel 8.0 -InstallDir "$env:USERPROFILE\.dotnet"` installed SDK 8.0.426 successfully. Prepending that directory to PATH and setting `DOTNET_ROOT` made the current PowerShell resolve 8.0.426.

## Takeaway
Verify actual SDK directories and `dotnet --list-sdks` after installation-tool success. When other SDK majors exist, use a project-level `global.json` with the required exact version before running `dotnet` commands.

## History
- 2026-10-22 (original-copilot-lab/t2): initial