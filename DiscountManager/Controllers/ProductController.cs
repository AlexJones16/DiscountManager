using DiscountManager.Application.Services;
using DiscountManager.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DiscountManager.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        [Route("/all")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProducts();
            if (products == null)
            {
                return NotFound($"Can`t get all products");
            }
            return Ok(products);
        }
    }
}
