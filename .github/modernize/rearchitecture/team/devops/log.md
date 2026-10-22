## [t2] Prepare and activate the .NET 8 environment
- Installed SDK 8.0.426 in the user profile after the MSI installer reported success without adding an SDK directory; the official script installed it correctly.
- The system has SDK 10.0.402, so downstream commands need a project-scoped `global.json` pin to avoid silently selecting .NET 10.
- Learnings consumed: [(none)]