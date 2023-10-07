using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;

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
        ListModel listModel = new ListModel()
        {
            ListTitle = "Programming Languages List",
            ListItems = new List<string>() {
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
        PersonGridModel personGridModel = new PersonGridModel()
        {
            GridTitle = "Friends",
            Persons = new List<Person>()
            {
                new Person() { PersonName = "Mia", JobTitle = "Developer" },
                new Person() { PersonName = "Emma", JobTitle = "UI Designer" },
                new Person() { PersonName = "Avva", JobTitle = "QA" }
            }
        };

        return ViewComponent("Grid", new { grid = personGridModel });
    }
}