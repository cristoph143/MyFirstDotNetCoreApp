using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace MyFirstDotNetCoreApp.Controllers;

public class PersonController(IPersonService personsService) : Controller
{
    [Route("/persons/index")]
    public IActionResult Index(string searchBy, string? searchString)
    {
        ViewBag.SearchFields = new Dictionary<string, string>()
      {
      {
                nameof(PersonResponse.PersonName),
                "Person Name"
      },
      {
                nameof(PersonResponse.Email),
                "Email"
      },
        { nameof(PersonResponse.DateOfBirth), "Date of Birth" },
        { nameof(PersonResponse.Gender), "Gender" },
        { nameof(PersonResponse.CountryId), "Country" },
        { nameof(PersonResponse.Address), "Address" }
      };
        List<PersonResponse> persons = personsService.GetFilteredPersons(searchBy, searchString);

        return View(persons); //Views/Persons/Index.cshtml
    }
}