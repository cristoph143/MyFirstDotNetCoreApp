/// <summary>
/// ICountriesService interface defines the service contract for country management operations.
/// </summary>
using ServiceContracts.DTO;

namespace ServiceContracts; 

public interface ICountriesService
{

    /// <summary>
    /// Adds a new country.
    /// </summary>
    /// <param name="countryAddRequest">The country details to add.</param>
    /// <returns>The added country details.</returns>
    CountryResponse? AddCountry(CountryAddRequest? countryAddRequest);

    /// <summary>
    /// Gets all countries.
    /// </summary>
    /// <returns>A list of all country details.</returns>
    List<CountryResponse>? GetAllCountries();

    /// <summary>
    /// Gets a country by ID.
    /// </summary>
    /// <param name="countryId">The ID of the country to get.</param>
    /// <returns>The country details if found, null otherwise.</returns>
    CountryResponse? GetCountryByCountryId(Guid? countryId);
}