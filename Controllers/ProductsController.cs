using Microsoft.AspNetCore.Mvc;

namespace MyFirstDotNetCoreApp.Controllers;

[Route("[controller]")]
public class ProductsController : Controller
{
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

    [Route("/search-products/{ProductID?}")]
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