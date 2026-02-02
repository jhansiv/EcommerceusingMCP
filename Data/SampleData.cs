using EcommerceApi.Models;

namespace EcommerceApi.Data;

public static class SampleData
{
    public static readonly List<Product> Products = new()
    {
        new Product { Id = 1, Name = "Wireless Mouse", Price = 19.99m },
        new Product { Id = 2, Name = "Mechanical Keyboard", Price = 59.99m },
        new Product { Id = 3, Name = "USB-C Hub", Price = 29.99m },
        new Product { Id = 4, Name = "Noise Cancelling Headphones", Price = 129.99m }
    };
}
