using EcommerceApi.Models;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text.Json;

namespace EcommerceusingMCP.Tools
{

    [McpServerToolType]
    public class EcommerceTools
    {
        private static readonly HttpClient httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5235/")
        };

        [McpServerTool, Description("Retrieves all available products from the e-commerce store.")]
        public static async Task<List<Product>> GetProductsAsync()
        {
            var response = await httpClient.GetAsync("api/products");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<List<Product>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }

        [McpServerTool, Description("Adds a product to the shopping cart.")]
        public static async Task<string> AddToCartAsync(
            [Description("The ID of the product to add")] int productId,
            [Description("The quantity to add")] int quantity = 1)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(new { productId, quantity }),
                System.Text.Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync("api/cart/items", content);
            
            if (!response.IsSuccessStatusCode)
            {
                return $"Failed to add product: {await response.Content.ReadAsStringAsync()}";
            }

            return await response.Content.ReadAsStringAsync();
        }

        [McpServerTool, Description("Gets the current shopping cart contents and total.")]
        public static async Task<string> GetCartAsync()
        {
            var response = await httpClient.GetAsync("api/cart");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        [McpServerTool, Description("Clears all items from the shopping cart.")]
        public static async Task<string> ClearCartAsync()
        {
            var response = await httpClient.PostAsync("api/cart/clear", null);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
