using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.Controllers;

public class WeatherController : Controller
{
    //initialize hard-coded data as instructed in the requirement
    private readonly List<CityWeather> _cities = new() {
        new CityWeather { CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"), TemperatureFahrenheit = 33 },
        new CityWeather { CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"), TemperatureFahrenheit = 60 },
        new CityWeather { CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"), TemperatureFahrenheit = 82 }
    };

    //When a HTTP GET request is received at path "/"
    [Route("/")]
    public IActionResult Index() =>
        //send cities collection to "Views/Weather/Index" view
        View(_cities);


    [Route("weather/{cityCode?}")]
    public IActionResult City(string? cityCode)
    {
        if (string.IsNullOrEmpty(cityCode))
            //send null as model object to "Views/Weather/Index" view
            return View();
        //get matching city object based on the city code
        CityWeather? city = _cities.FirstOrDefault(temp => temp.CityUniqueCode == cityCode);
        return View(city);
    }
}