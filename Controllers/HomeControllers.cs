using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.Controllers;

public class HomeController : Controller
{
    [Route("home")]
    [Route("/")]
    public IActionResult Index()
    {
        ViewData["appTitle"] = "Asp.Net Core Demo App";

        List<Person> people = new List<Person>()
        {
            new() { Name = "John", DateOfBirth = DateTime.Parse("2000-05-06"), PersonGender = Gender.Male},
            new() { Name = "Linda", DateOfBirth = DateTime.Parse("2005-01-09"), PersonGender = Gender.Female},
            new() { Name = "Susan", DateOfBirth = DateTime.Parse("2008-07-12"), PersonGender = Gender.Other}
        };
        // ViewData["people"] = people;
        ViewBag.people = people;
        return View(); //Views/Home/Index.cshtml
        //return View("abc"); //abc.cshtml
        //return new ViewResult() { ViewName = "abc" };
    }
}