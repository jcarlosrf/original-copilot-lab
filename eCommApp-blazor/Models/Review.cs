namespace ECommAppBlazor.Models;

public sealed record Review
{
    public string Author { get; init; } = string.Empty;

    public string Comment { get; init; } = string.Empty;

    public string Date { get; init; } = string.Empty;
}