using Microsoft.AspNetCore.Mvc;

namespace MyFirstDotNetCoreApp.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    public ILogger<HomeController> Logger1 { get; }

    public HomeController(ILogger<HomeController> logger)
    {
        Logger1 = logger;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
    [Route("/")]
    public IActionResult Index()
    {
        ViewData["ListTitle"] = "Cities";
        ViewData["ListItems"] = new List<string>() { 
        "Paris",
        "New York",
        "New Mumbai",
        "Rome"
        };
        return View();
    }

    [Route("/about-company")]
    public IActionResult About()
    {
        ViewBag.ListItems = new List<string> { "Item 1", "Item 2", "Item 3" };
        return View();
    }

    [Route("/contact-support")]
    public IActionResult Contact()
    {
        return View();
    }
}