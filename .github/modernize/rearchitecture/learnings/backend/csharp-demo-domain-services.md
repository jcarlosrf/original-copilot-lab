# C# Demo Domain Services

Use scoped Blazor services for cart/catalog state that should live only within the current interactive circuit.

## What Happened
For original-copilot-lab task t12, the typed C# models were introduced beside the Blazor project, and JSON catalog access and cart state were separated behind interfaces. `CartItem` uses composition (`Product` + `Quantity`) so UI consumers should access item fields through `item.Product`.

## Takeaway
Register stateful demo services with `AddScoped`; keep catalog JSON access in a catalog service, and enforce availability in the cart service as well as the UI. Expose cart snapshots rather than the mutable internal list. Treat checkout as an in-memory order snapshot, not persistence.

## History
- 2026-10-22 (original-copilot-lab/t12): initial
