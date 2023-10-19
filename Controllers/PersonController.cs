using Microsoft.AspNetCore.Mvc;

namespace MyFirstDotNetCoreApp.Controllers;

public class PersonController : Controller
{
    [Route("/persons/index")]
    public IActionResult Index()
    {
        return View();
    }
}