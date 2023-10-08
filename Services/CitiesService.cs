using ServiceContracts;

namespace Services;

public class CitiesService : ICitiesService
{
    private readonly List<string> _cities = new() { 
        "London",
        "Paris",
        "New York",
        "Tokyo",
        "Rome"
    };

    public List<string> GetCities()
    {
        return _cities;
    }
}