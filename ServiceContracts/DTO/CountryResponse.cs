using Entities;

namespace ServiceContracts.DTO;

public class CountryResponse
{
    public Guid CountryID { get; set; }
    public string? CountryName { get; set; }
}

public static class CountryExtensions
{
    //Converts from Country object to CountryResponse object
    public static CountryResponse ToCountryResponse(this Country country) => new() {  CountryID = country.CountryID, CountryName = country.CountryName };
}