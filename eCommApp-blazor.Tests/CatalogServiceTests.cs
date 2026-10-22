using ECommAppBlazor.Models;
using ECommAppBlazor.Services;
using Xunit;

namespace ECommAppBlazor.Tests;

public sealed class CatalogServiceTests
{
    [Fact]
    public async Task GetProductsAsync_LoadsJsonAndAddReviewPrependsIt()
    {
        var productsDirectory = Directory.CreateTempSubdirectory("ecomm-catalog-").FullName;
        try
        {
            await File.WriteAllTextAsync(Path.Combine(productsDirectory, "apple.json"),
                "{\"id\":\"1\",\"name\":\"Apple\",\"price\":0.5,\"inStock\":true,\"reviews\":[]}");
            await File.WriteAllTextAsync(Path.Combine(productsDirectory, "grapes.json"),
                "{\"id\":\"2\",\"name\":\"Grapes\",\"price\":2.0,\"inStock\":true,\"reviews\":[]}");
            await File.WriteAllTextAsync(Path.Combine(productsDirectory, "orange.json"),
                "{\"id\":\"3\",\"name\":\"Orange\",\"price\":0.75,\"inStock\":false,\"reviews\":[]}");
            await File.WriteAllTextAsync(Path.Combine(productsDirectory, "pear.json"),
                "{\"id\":\"4\",\"name\":\"Pear\",\"price\":1.25,\"inStock\":true,\"reviews\":[]}");
            var catalog = new CatalogService(productsDirectory);

            var products = await catalog.GetProductsAsync();
            var reviewAdded = await catalog.AddReviewAsync("1", new Review
            {
                Author = "Sam",
                Comment = "Fresh and crisp"
            });

            Assert.Equal(new[] { "Apple", "Grapes", "Orange", "Pear" }, products.Select(product => product.Name));
            Assert.False(products[2].InStock);
            Assert.True(reviewAdded);
            Assert.Equal("Sam", products[0].Reviews[0].Author);
            Assert.False(string.IsNullOrWhiteSpace(products[0].Reviews[0].Date));
            Assert.False(await catalog.AddReviewAsync("missing", new Review { Author = "Sam", Comment = "Review" }));
        }
        finally
        {
            Directory.Delete(productsDirectory, recursive: true);
        }
    }
}