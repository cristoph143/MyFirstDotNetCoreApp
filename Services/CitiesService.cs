namespace Services;

public class CitiesService
{
    private readonly List<string> _cities1;

    public CitiesService()
    {
        _cities1 = new List<string>
        {
            "London",
            "Paris",
            "New York",
            "Tokyo",
            "Rome"
        };
    }

    public List<string> GetCities()
    {
        return _cities1;
    }
}