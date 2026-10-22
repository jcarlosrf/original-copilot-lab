namespace ECommAppBlazor.Models;

public sealed class Product
{
    public string Id { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public decimal Price { get; init; }

    public string? Description { get; init; }

    public string? Image { get; init; }

    public string? Category { get; init; }

    public List<Review> Reviews { get; init; } = [];

    public bool InStock { get; init; }
}