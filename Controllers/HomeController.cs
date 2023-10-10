using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;
using ServiceContracts;

namespace MyFirstDotNetCoreApp.Controllers;

[Route("[controller]")]
public class HomeController(
    ICitiesService citiesService,
    ICitiesService _citiesService1,
    ICitiesService _citiesService2,
    ICitiesService _citiesService3,
    IServiceScopeFactory _serviceScopeFactory
    ) : Controller
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }

    [Route("/get-cities")]
    public IActionResult GetCities()
    {
        List<string> cities1 = citiesService.GetCities();
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

    [Route("/from-services")]
    public IActionResult FromServices([FromServices] ICitiesService _citiesService)
    {
        List<string> cities = _citiesService.GetCities();
        return View(cities);
    }

    [Route("/from-singleton")]
    public IActionResult Singleton()
    {
        List<string> cities = _citiesService1.GetCities();
        ViewBag.InstanceId_CitiesService_1 = _citiesService1.ServiceInstanceId;
        ViewBag.InstanceId_CitiesService_2 = _citiesService2.ServiceInstanceId;
        ViewBag.InstanceId_CitiesService_3 = _citiesService3.ServiceInstanceId;
        return View(cities);
    }

    [Route("/from-serviceScope")]
    public IActionResult ServiceScope()
    {
        List<string> cities = _citiesService1.GetCities();
        ViewBag.InstanceId_CitiesService_1 = _citiesService1.ServiceInstanceId;
        ViewBag.InstanceId_CitiesService_2 = _citiesService2.ServiceInstanceId;
        ViewBag.InstanceId_CitiesService_3 = _citiesService3.ServiceInstanceId;
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            //Inject CitiesService
            ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();
            //DB work
            ViewBag.InstanceId_CitiesServicece_InScope = citiesService.ServiceInstanceId;
        } //end of scope; it calls CitiesService.Dispose()
        return View(cities);
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