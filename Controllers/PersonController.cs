﻿using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using ServiceContracts.DTO;

namespace MyFirstDotNetCoreApp.Controllers;

public class PersonController(IPersonService personsService) : Controller
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
}