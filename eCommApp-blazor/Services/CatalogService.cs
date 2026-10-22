using System.Text.Json;
using ECommAppBlazor.Models;

namespace ECommAppBlazor.Services;

public sealed class CatalogService : ICatalogService
{
    private static readonly string[] ProductFiles = ["apple.json", "grapes.json", "orange.json", "pear.json"];
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly string productsDirectory;
    private readonly SemaphoreSlim loadLock = new(1, 1);
    private IReadOnlyList<Product>? products;

    public CatalogService(IWebHostEnvironment environment)
        : this(Path.Combine(environment.WebRootPath, "products"))
    {
    }

    public CatalogService(string productsDirectory)
    {
        this.productsDirectory = productsDirectory;
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync(CancellationToken cancellationToken = default)
    {
        if (products is not null)
        {
            return products;
        }

        await loadLock.WaitAsync(cancellationToken);
        try
        {
            if (products is null)
            {
                var loadedProducts = new List<Product>(ProductFiles.Length);
                foreach (var fileName in ProductFiles)
                {
                    await using var stream = File.OpenRead(Path.Combine(productsDirectory, fileName));
                    var product = await JsonSerializer.DeserializeAsync<Product>(stream, JsonOptions, cancellationToken);
                    if (product is null)
                    {
                        throw new JsonException($"Product file '{fileName}' contained no product.");
                    }

                    loadedProducts.Add(product);
                }

                products = Array.AsReadOnly(loadedProducts.ToArray());
            }

            return products;
        }
        finally
        {
            loadLock.Release();
        }
    }

    public async Task<Product?> GetProductByIdAsync(string productId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productId);
        var catalog = await GetProductsAsync(cancellationToken);
        return catalog.FirstOrDefault(product => product.Id == productId);
    }

    public async Task<bool> AddReviewAsync(string productId, Review review, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(productId);
        ArgumentNullException.ThrowIfNull(review);
        ArgumentException.ThrowIfNullOrWhiteSpace(review.Author);
        ArgumentException.ThrowIfNullOrWhiteSpace(review.Comment);

        var product = await GetProductByIdAsync(productId, cancellationToken);
        if (product is null)
        {
            return false;
        }

        var datedReview = string.IsNullOrWhiteSpace(review.Date)
            ? review with { Date = DateTimeOffset.UtcNow.ToString("O") }
            : review;

        lock (product.Reviews)
        {
            product.Reviews.Insert(0, datedReview);
        }

        return true;
    }
}