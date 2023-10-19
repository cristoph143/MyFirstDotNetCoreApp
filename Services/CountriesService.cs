﻿using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class CountriesService : ICountriesService
{
    //private field
    private readonly List<Country> _countries = new();
    public CountryResponse? AddCountry(CountryAddRequest? countryAddRequest)
    {
        //Validation: countryAddRequest parameter can't be null
        if (countryAddRequest == null)
            throw new ArgumentNullException(nameof(countryAddRequest));

        //Validation: CountryName can't be null
        if (countryAddRequest.CountryName == null)
            throw new ArgumentException(nameof(countryAddRequest.CountryName));

        //Validation: CountryName can't be duplicate
        if (_countries.Any(temp => temp.CountryName == countryAddRequest.CountryName))
            throw new ArgumentException("Given country name already exists");
        //Convert object from CountryAddRequest to Country type
        Country country = countryAddRequest.ToCountry();
        //generate CountryID
        country.CountryID = Guid.NewGuid();
        //Add country object into _countries
        _countries.Add(country);
        return country.ToCountryResponse();
    }

    public List<CountryResponse?> GetAllCountries()
    {
        return _countries.Select(
            country => country.ToCountryResponse()
        ).ToList();
    }

    public CountryResponse? GetCountryByCountryId(Guid? countryId)
    {
        if (countryId == null)
            return null;

        Country? countryResponseFromList = 
            _countries.FirstOrDefault(temp => temp.CountryID == countryId);

        return countryResponseFromList?.ToCountryResponse();
    }
}