## [t2] Prepare and activate the .NET 8 environment
- Installed SDK 8.0.426 in the user profile after the MSI installer reported success without adding an SDK directory; the official script installed it correctly.
- The system has SDK 10.0.402, so downstream commands need a project-scoped `global.json` pin to avoid silently selecting .NET 10.
- Learnings consumed: [(none)]

## [t9] Scan and remediate .NET dependency CVEs
- The pinned SDK 8.0.426 exists under `%USERPROFILE%\.dotnet`; this shell needed `DOTNET_ROOT` and `PATH` set explicitly to resolve the repository's exact `global.json` version.
- The Blazor Web SDK project has no resolved NuGet PackageReferences; the advisory scan therefore received an empty coordinate list and found no package CVEs.
- `dotnet test` builds the web project but discovers no tests; report this as no test project/cases, not as test coverage.
- Learnings consumed: [devops/net8-sdk-installation-windows]