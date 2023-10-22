using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace MyFirstDotNetCoreApp.Controllers;

public class PersonController(IPersonService personsService) : Controller
{
    [Route("/persons/index")]
    public IActionResult Index()
    {
        List<PersonResponse> persons = personsService.GetAllPersons();
        return View(persons);
    }
}