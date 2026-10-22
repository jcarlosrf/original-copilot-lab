using ECommAppBlazor.Models;
using ECommAppBlazor.Services;
using Xunit;

namespace ECommAppBlazor.Tests;

public sealed class CartServiceTests
{
    [Fact]
    public void TryAddToCart_AddsAvailableProductAndIncrementsQuantity()
    {
        var cart = new CartService();
        var apple = CreateProduct("1", "Apple", 0.50m);

        Assert.True(cart.TryAddToCart(apple));
        Assert.True(cart.TryAddToCart(apple));

        var item = Assert.Single(cart.Items);
        Assert.Equal(2, item.Quantity);
        Assert.Equal(1.00m, cart.Total);
    }

    [Fact]
    public void TryAddToCart_RejectsUnavailableProduct()
    {
        var cart = new CartService();

        Assert.False(cart.TryAddToCart(CreateProduct("3", "Orange", 0.75m, inStock: false)));
        Assert.Empty(cart.Items);
    }

    [Fact]
    public void Checkout_CreatesOrderAndEmptiesCart()
    {
        var cart = new CartService();
        cart.TryAddToCart(CreateProduct("1", "Apple", 0.50m));
        cart.TryAddToCart(CreateProduct("2", "Grapes", 2.00m));

        var order = Assert.IsType<Order>(cart.Checkout());

        Assert.Equal(2.50m, order.Total);
        Assert.Equal(2, order.Items.Count);
        Assert.Empty(cart.Items);
        Assert.Null(cart.Checkout());
    }

    private static Product CreateProduct(string id, string name, decimal price, bool inStock = true) => new()
    {
        Id = id,
        Name = name,
        Price = price,
        InStock = inStock
    };
}