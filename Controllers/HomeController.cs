using Autofac;
using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;
using ServiceContracts;

namespace MyFirstDotNetCoreApp.Controllers;

[Route("[controller]")]
public class HomeController(
    ICitiesService citiesService,
    ICitiesService citiesService1,
    ICitiesService citiesService2,
    ICitiesService citiesService3,
    IServiceScopeFactory _serviceScopeFactory,
    ILifetimeScope _lifeTimeScope,
    IWebHostEnvironment _webHostEnvironment
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
              ViewBag.CurrentEnviornment = _webHostEnvironment.EnvironmentName;
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
        List<string> cities = citiesService1.GetCities();
        ViewBag.InstanceId_CitiesService_1 = citiesService1.ServiceInstanceId;
        ViewBag.InstanceId_CitiesService_2 = citiesService2.ServiceInstanceId;
        ViewBag.InstanceId_CitiesService_3 = citiesService3.ServiceInstanceId;
        return View(cities);
    }

    [Route("/from-serviceScope")]
    public IActionResult ServiceScope()
    {
        List<string> cities = citiesService1.GetCities();
        ViewBag.InstanceId_CitiesService_1 = citiesService1.ServiceInstanceId;
        ViewBag.InstanceId_CitiesService_2 = citiesService2.ServiceInstanceId;
        ViewBag.InstanceId_CitiesService_3 = citiesService3.ServiceInstanceId;
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            //Inject CitiesService
            ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();
            //DB work
            ViewBag.InstanceId_CitiesServicece_InScope = citiesService.ServiceInstanceId;
        } //end of scope; it calls CitiesService.Dispose()
        return View(cities);
    }

    [Route("/view-injection")]
    public IActionResult ViewInjection()
    {
        List<string> cities = citiesService1.GetCities();
        ViewBag.InstanceId_CitiesService_1 = citiesService1.ServiceInstanceId;
        ViewBag.InstanceId_CitiesService_2 = citiesService2.ServiceInstanceId;
        ViewBag.InstanceId_CitiesService_3 = citiesService3.ServiceInstanceId;
        using (IServiceScope scope = _serviceScopeFactory.CreateScope())
        {
            //Inject CitiesService
            ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>();
            //DB work
            ViewBag.InstanceId_CitiesService_InScope = citiesService.ServiceInstanceId;
        } //end of scope; it calls CitiesService.Dispose()
        return View(cities);
    }

    [Route("/autoFac")]
    public IActionResult AutoFac()
    {
        List<string> cities = citiesService1.GetCities();
        ViewBag.InstanceId_CitiesService_1 = citiesService1.ServiceInstanceId;

        ViewBag.InstanceId_CitiesService_2 = citiesService2.ServiceInstanceId;

        ViewBag.InstanceId_CitiesService_3 = citiesService3.ServiceInstanceId;

        using (ILifetimeScope scope = _lifeTimeScope.BeginLifetimeScope())
        {
            //Inject CitiesService
            ICitiesService citiesService = scope.Resolve<ICitiesService>();
            //DB work

            ViewBag.InstanceId_CitiesService_InScope = citiesService.ServiceInstanceId;
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