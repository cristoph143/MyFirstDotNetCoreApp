﻿using ServiceContracts;

namespace Services;

public class CitiesService : ICitiesService, IDisposable
{
    private List<string> _cities;

    public Guid ServiceInstanceId { get; }

    public CitiesService()
    {
        ServiceInstanceId = Guid.NewGuid();
        _cities = new List<string>() {
        "London",
        "Paris",
        "New York",
        "Tokyo",
        "Rome"
      };
        //TO DO: Add logic to open the db connection
    }

    public List<string> GetCities()
    {
        return _cities;
    }
    public void Dispose()
    {
        //TO DO: add logic to close db connection
    }
}