using System;
using System.Collections.Generic;
using System.Linq;
using MyFirstDotNetCoreApp.Models;
using ServiceContracts;

namespace Services;

public class WeatherService : IWeatherService
{
    //readonly field to store list of hard-coded cities as instructed in the requirement
    private readonly List<CityWeather> _cities = new()
    {
        new CityWeather
        {
            CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),
            TemperatureFahrenheit = 33
        },
        new CityWeather
        {
            CityUniqueCode = "NYC", CityName = "New York", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),
            TemperatureFahrenheit = 60
        },
        new CityWeather
        {
            CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),
            TemperatureFahrenheit = 82
        }
    };

    //initialize hard-coded data as instructed in the requirement
    public List<CityWeather> GetWeatherDetails()
    {
        return _cities;
    }

    public CityWeather? GetWeatherByCityCode(string CityCode)
    {
        var city = _cities.FirstOrDefault(x => x.CityUniqueCode == CityCode);
        return city;
    }
}