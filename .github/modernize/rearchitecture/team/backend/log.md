## [t12] Implement models and in-memory catalog/cart services
- The existing Blazor page parses catalog JSON into `JsonElement`; the new service maps the same `System.Text.Json` web-cased properties into typed models while preserving the four-file order.
- The frontend contract disables Orange based on `inStock`; `TryAddToCart` independently enforces that rule so the backend service cannot accept unavailable products.
- Use scoped DI for per-circuit demo state. The service does not persist across app restarts or users.
- A running Release app locks its output DLL and apphost on Windows; validation succeeded with an isolated `BaseOutputPath` and did not stop the user's process.
- Learnings consumed: none.
