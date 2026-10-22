using ECommAppBlazor.Models;

namespace ECommAppBlazor.Services;

public sealed class CartService : ICartService
{
    private readonly object gate = new();
    private readonly List<CartItem> items = [];

    public IReadOnlyList<CartItem> Items
    {
        get
        {
            lock (gate)
            {
                return items.ToArray();
            }
        }
    }

    public decimal Total
    {
        get
        {
            lock (gate)
            {
                return items.Sum(item => item.Product.Price * item.Quantity);
            }
        }
    }

    public bool TryAddToCart(Product product)
    {
        ArgumentNullException.ThrowIfNull(product);
        if (!product.InStock || string.IsNullOrWhiteSpace(product.Id))
        {
            return false;
        }

        lock (gate)
        {
            var existingIndex = items.FindIndex(item => item.Product.Id == product.Id);
            if (existingIndex >= 0)
            {
                items[existingIndex] = items[existingIndex] with { Quantity = items[existingIndex].Quantity + 1 };
            }
            else
            {
                items.Add(new CartItem(product, 1));
            }
        }

        return true;
    }

    public void Clear()
    {
        lock (gate)
        {
            items.Clear();
        }
    }

    public Order? Checkout()
    {
        lock (gate)
        {
            if (items.Count == 0)
            {
                return null;
            }

            var order = new Order(
                Guid.NewGuid(),
                Array.AsReadOnly(items.ToArray()),
                items.Sum(item => item.Product.Price * item.Quantity),
                DateTimeOffset.UtcNow);
            items.Clear();
            return order;
        }
    }
}