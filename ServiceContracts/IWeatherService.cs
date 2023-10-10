using System.Collections.Generic;
using MyFirstDotNetCoreApp.Models;

namespace ServiceContracts;

public interface IWeatherService
{
    List<CityWeather> GetWeatherDetails();
    CityWeather? GetWeatherByCityCode(string CityCode);
}