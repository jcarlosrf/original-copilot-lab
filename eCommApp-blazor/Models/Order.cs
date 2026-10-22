namespace ECommAppBlazor.Models;

public sealed record Order(
    Guid Id,
    IReadOnlyList<CartItem> Items,
    decimal Total,
    DateTimeOffset CreatedAt);