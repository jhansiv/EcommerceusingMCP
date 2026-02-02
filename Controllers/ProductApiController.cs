using EcommerceApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet(Name = "GetProducts")]
        public IActionResult GetProducts()
        {
            return Ok(SampleData.Products);
        }
    }
}