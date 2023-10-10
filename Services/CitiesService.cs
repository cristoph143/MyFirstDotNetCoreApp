using ServiceContracts;

namespace Services;

public class CitiesService : ICitiesService
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
    }

    public List<string> GetCities()
    {
        return _cities;
    }
}