using EcommerceApi.Dtos;
using EcommerceApi.Infrastructure;
using EcommerceApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/cart")]
    public class CartController : ControllerBase
    {
        private const string CartKey = "CART";

        [HttpGet]
        public IActionResult GetCart()
        {
            var cart = HttpContext.Session.GetJson<List<CartItem>>(CartKey) ?? new List<CartItem>();
            var total = cart.Sum(i => i.Price * i.Quantity);
            return Ok(new { items = cart, total });
        }

        [HttpPost("items")]
        public IActionResult AddToCart([FromBody] AddToCartRequest req)
        {
            if (req.Quantity <= 0)
                return BadRequest("Quantity must be > 0");

            var product = EcommerceApi.Data.SampleData.Products.FirstOrDefault(p => p.Id == req.ProductId);
            if (product is null)
                return NotFound("Product not found");

            var cart = HttpContext.Session.GetJson<List<CartItem>>(CartKey) ?? new List<CartItem>();

            var existing = cart.FirstOrDefault(x => x.ProductId == product.Id);
            if (existing is null)
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = req.Quantity
                });
            }
            else
            {
                existing.Quantity += req.Quantity;
            }

            HttpContext.Session.SetJson(CartKey, cart);

            var total = cart.Sum(i => i.Price * i.Quantity);
            return Ok(new { items = cart, total });
        }

        [HttpPost("clear")]
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CartKey);
            return Ok(new { items = Array.Empty<CartItem>(), total = 0m });
        }
    }
}