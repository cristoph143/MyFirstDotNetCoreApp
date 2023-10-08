using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;
using Services;

namespace MyFirstDotNetCoreApp.Controllers;

[Route("[controller]")]
public class HomeController : Controller
{
    private readonly CitiesService _citiesService;
    public HomeController(ILogger<HomeController> logger)
    {
        Logger1 = logger;
        _citiesService = new CitiesService();
    }

    public ILogger<HomeController> Logger1 { get; }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }

    [Route("/get-cities")]
    public IActionResult GetCities()
    {
        var cities1 = _citiesService.GetCities();
        return View(cities1);
    }

    [Route("/")]
    public IActionResult Index()
    {
        ViewData["ListTitle"] = "Cities";
        ViewData["ListItems"] = new List<string>
        {
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
        return View();
    }

    [Route("/contact-support")]
    public IActionResult Contact()
    {
        return View();
    }

    [Route("/programming-languages")]
    public IActionResult ProgrammingLanguages()
    {
        var listModel = new ListModel
        {
            ListTitle = "Programming Languages List",
            ListItems = new List<string>
            {
                "Python",
                "C#",
                "Go"
            }
        };

        return PartialView("_ListPartialView", listModel);
    }

    [Route("/friends-list")]
    public IActionResult LoadFriendsList()
    {
        var personGridModel = new PersonGridModel
        {
            GridTitle = "Friends",
            Persons = new List<Person>
            {
                new() { PersonName = "Mia", JobTitle = "Developer" },
                new() { PersonName = "Emma", JobTitle = "UI Designer" },
                new() { PersonName = "Avva", JobTitle = "QA" }
            }
        };

        return ViewComponent("Grid", new { grid = personGridModel });
    }
}