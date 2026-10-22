# Empty NuGet Graph in Web SDK Projects

An empty `dotnet list package --include-transitive` result means the NuGet package scan has no coordinates, but it does not assess the shared ASP.NET Core runtime.

## What Happened
In original-copilot-lab task t9, the .NET 8 Blazor Web SDK project declared no NuGet packages. The advisory tool returned no findings for the empty input. `dotnet test` built the web app but did not execute test cases.

## Takeaway
Record the resolved package count and scope of the advisory scan. Do not present an empty NuGet result as a runtime/shared-framework CVE assessment, and distinguish a successful `dotnet test` build from actual test execution.

## History
- 2026-10-22 (original-copilot-lab/t9): initial