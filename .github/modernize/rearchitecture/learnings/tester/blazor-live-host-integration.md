# Blazor Live Host Integration

For local Blazor integration checks, activate the pinned user-local SDK and use the existing launch profile so scoped CSS assets are served.

## What Happened

In original-copilot-lab task t8.2, the default terminal could not resolve SDK 8.0.426 because `%USERPROFILE%\.dotnet` was absent from PATH. Running the Release DLL directly used Production mode and returned 404 for the generated scoped stylesheet. The existing `http` launch profile with the user-local SDK served all CSS and passed the HTTP contract.

## Takeaway

Set `DOTNET_ROOT` to `%USERPROFILE%\.dotnet`, prepend it to PATH, and run `dotnet run --no-build --configuration Release --launch-profile http --urls http://127.0.0.1:<free-port>`. Include the scoped stylesheet URL in asset checks and stop only the test-owned Kestrel process after validation.

## History
- 2026-10-22 (original-copilot-lab/t8.2): initial