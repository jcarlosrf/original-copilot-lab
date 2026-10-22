# Blazor .NET 8 Scaffold

For this rewrite, keep the Blazor project at the requested output directory root and activate the pinned user-profile SDK in fresh terminals.

## What Happened

In `original-copilot-lab` task t3, `dotnet new blazor --name ECommAppBlazor` created an extra nested directory. A subsequent root-level generation duplicated `Program.cs` and the `Routes` component until the nested generated copy was removed. The project-scoped `global.json` pinned SDK `8.0.426`; a new PowerShell terminal also needed `%USERPROFILE%\.dotnet` prepended to PATH.

## Takeaway

Run the template with `--output .` from the intended project directory. In fresh terminals, set `DOTNET_ROOT` and PATH using the preparation instructions in `t2-devops.md`, then verify `dotnet --version` before build or run.

## History
- 2026-10-22 (original-copilot-lab/t3): initial