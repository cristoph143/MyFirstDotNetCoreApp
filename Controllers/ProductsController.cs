using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyFirstDotNetCoreApp.Controllers
{
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet("/products")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/search-products")]
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet("/order-product")]
        public IActionResult Order()
        {
            return View();
        }
    }
}