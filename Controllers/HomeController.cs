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
        return View();
    }

    [Route("/about-company")]
    public IActionResult About()
    {
        return View();
    }

    [Route("/contact-support")]
    public IActionResult Contact()
    {
        return View();
    }
}