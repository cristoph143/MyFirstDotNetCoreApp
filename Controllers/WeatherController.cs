using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace MyFirstDotNetCoreApp.Controllers;

[Route("[controller]")]
public class WeatherController(IWeatherService weatherService) : Controller
{
    //When a HTTP GET request is received at path "/"
    [Route("/")]
    public IActionResult Index()
    {
        //invoke service method
        var cities = weatherService.GetWeatherDetails();
        //send cities collection to "Views/Weather/Index" view
        return View(cities);
    }


    [Route("/weather/{cityCode?}")]
    public IActionResult City(string? cityCode)
    {
        //if cityCode is not supplied in the route parameter
        if (string.IsNullOrEmpty(cityCode))
            //send null as model object to "Views/Weather/Index" view
            return View();
        //invoke service method (get matching city object based on the city code)
        var city = weatherService.GetWeatherByCityCode(cityCode);
        //send matching city object to "Views/Weather/Index" view
        return View(city);
    }
}