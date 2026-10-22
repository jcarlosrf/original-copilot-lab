# t2 — .NET 8 Environment Preparation

## Target Environment Preparation
- Status: READY
- Requested target: .NET 8 SDK for the Blazor Web App.
- Preparation: Installed Microsoft .NET SDK 8.0.426 to `%USERPROFILE%\.dotnet` using the official `dotnet-install.ps1` installer. No administrator privileges were needed.
- Installed versions: 8.0.426 in the user profile; system-wide SDKs 3.1.426, 5.0.303, 5.0.408, 9.0.100-rc.1.24452.12, and 10.0.402 remain installed.
- Active shell: `C:\Users\jsra\.dotnet\dotnet.exe`; `DOTNET_ROOT=C:\Users\jsra\.dotnet`; `dotnet --version` returns `8.0.426`.
- Command-resolution evidence: `Get-Command dotnet` resolved to `C:\Users\jsra\.dotnet\dotnet.exe`; `dotnet --list-sdks` returned `8.0.426 [C:\Users\jsra\.dotnet\sdk]` in the prepared shell.
- Planned build/test commands: Run from `eCommApp-blazor` after its scaffold. Add the SDK pin below before invoking `dotnet new`, `dotnet build`, or `dotnet test`; it will select the exact SDK independently of the machine-wide .NET 10 installation.

```json
{
  "sdk": {
    "version": "8.0.426",
    "rollForward": "disable",
    "allowPrerelease": false
  }
}
```

- New-terminal activation commands until the project pin is in place:

```powershell
$env:DOTNET_ROOT = Join-Path $env:USERPROFILE '.dotnet'
$env:PATH = "$env:DOTNET_ROOT;$env:DOTNET_ROOT\tools;$env:PATH"
dotnet --version
```

- The current prepared PowerShell session already has these environment variables and resolves .NET 8. New terminals must run the activation commands above; no persistent user-level PATH change was made.
- Missing tools: None for SDK discovery/build. The application project and its `global.json` do not exist yet and belong to the scaffold task.
- Blockers: None.
- Downstream implication: t3 should create the project-scoped `global.json` shown above before running the .NET CLI, so SDK selection remains deterministic despite installed .NET 9/10 SDKs.

## Test Results
- Command: `Get-Command dotnet`; `dotnet --list-sdks`; `dotnet --version`
- Passed: 3 environment checks; resolved path and active version confirmed as `C:\Users\jsra\.dotnet\dotnet.exe` and `8.0.426`.
- Failed: 0
- Skipped: Application build/tests; the C# project has not been scaffolded yet.

## Upstream Artifacts Consumed
- `.github/modernize/rearchitecture/clarification.md` — confirmed the .NET 8 Blazor target and the separate `eCommApp-blazor` output directory.

## Evidence Mapping
- `clarification.md#Frontend` → SDK target is .NET 8; exact SDK installation and resolution are recorded above.
- `clarification.md#Generic` → project-scoped SDK pin and follow-on commands apply to the new sibling app, preserving the React project.