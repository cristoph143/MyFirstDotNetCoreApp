using ServiceContracts;

namespace Services;

public class CitiesService : ICitiesService, IDisposable
{
    private readonly List<string> _cities = new() {
        "London",
        "Paris",
        "New York",
        "Tokyo",
        "Rome"
    };

    public Guid ServiceInstanceId { get; } = Guid.NewGuid();

    //TO DO: Add logic to open the db connection

    public List<string> GetCities()
    {
        return _cities;
    }
    public void Dispose()
    {
        //TO DO: add logic to close db connection
    }
}