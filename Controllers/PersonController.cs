using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace MyFirstDotNetCoreApp.Controllers;

public class PersonController(
    IPersonService personsService,
    ICountriesService countriesService) : Controller
{
    [Route("/persons/index")]
    public IActionResult Index(
        string searchBy, string? searchString,
        string sortBy = nameof(PersonResponse.PersonName),
        SortOrderOptions sortOrder = SortOrderOptions.ASC)
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
        ViewBag.CurrentSearchBy = searchBy;
        ViewBag.CurrentSearchString = searchString;
        //Sort
        List<PersonResponse> sortedPersons = personsService.GetSortedPersons(persons, sortBy, sortOrder);
        ViewBag.CurrentSortBy = sortBy;
        ViewBag.CurrentSortOrder = sortOrder.ToString();
        return View(sortedPersons); //Views/Persons/Index.cshtml
    }

    //Executes when the user clicks on "Create Person" hyperlink (while opening the create view)
    [Route("persons/create")]
    [HttpGet]
    public IActionResult Create()
    {
        List<CountryResponse> countries = countriesService.GetAllCountries();
        ViewBag.Countries = countries;

        return View();
    }

    [HttpPost]
    [Route("/persons/create")]
    public IActionResult Create(PersonAddRequest personAddRequest)
    {
        if (!ModelState.IsValid)
        {
            List<CountryResponse>? countries = countriesService.GetAllCountries();
            if (countries != null) ViewBag.Countries = countries;

            ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return View();
        }

        //call the service method
        personsService.AddPerson(personAddRequest);
        //navigate to Index() action method (it makes another get request to "persons/index"
        return RedirectToAction("Index", "Person");
    }
}