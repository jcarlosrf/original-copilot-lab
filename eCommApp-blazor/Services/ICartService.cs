using ECommAppBlazor.Models;

namespace ECommAppBlazor.Services;

public interface ICartService
{
    IReadOnlyList<CartItem> Items { get; }

    decimal Total { get; }

    bool TryAddToCart(Product product);

    void Clear();

    Order? Checkout();
}