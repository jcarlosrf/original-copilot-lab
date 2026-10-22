using ECommAppBlazor.Models;

namespace ECommAppBlazor.Services;

public interface ICatalogService
{
    Task<IReadOnlyList<Product>> GetProductsAsync(CancellationToken cancellationToken = default);

    Task<Product?> GetProductByIdAsync(string productId, CancellationToken cancellationToken = default);

    Task<bool> AddReviewAsync(string productId, Review review, CancellationToken cancellationToken = default);
}