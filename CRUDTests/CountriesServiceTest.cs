﻿using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTests;

public class CountriesServiceTest
{
    private readonly ICountriesService _countriesService = new CountriesService();

    #region AddCountry
    //When CountryAddRequest is null, it should throw ArgumentNullException
    [Fact]
    public void AddCountry_NullCountry()
    {
        //Arrange
        CountryAddRequest? request = null;

        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //Act
            _countriesService.AddCountry(request);
        });
    }

    //When the CountryName is null, it should throw ArgumentException
    [Fact]
    public void AddCountry_CountryNameIsNull()
    {
        //Arrange
        CountryAddRequest request = new CountryAddRequest() { CountryName = null };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _countriesService.AddCountry(request);
        });
    }


    //When the CountryName is duplicate, it should throw ArgumentException
    [Fact]
    public void AddCountry_DuplicateCountryName()
    {
        //Arrange
        CountryAddRequest request1 = new CountryAddRequest { CountryName = "USA" };
        CountryAddRequest request2 = new CountryAddRequest { CountryName = "USA" };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _countriesService.AddCountry(request1);
            _countriesService.AddCountry(request2);
        });
    }


    //When you supply proper country name, it should insert (add) the country to the existing list of countries
    [Fact]
    public void AddCountry_ProperCountryDetails()
    {
        //Arrange
        CountryAddRequest request = new CountryAddRequest { CountryName = "Japan" };

        //Act
        CountryResponse? response = _countriesService.AddCountry(request);
        List<CountryResponse> countriesFromGetAllCountries = _countriesService.GetAllCountries();

        //Assert
        Assert.True(response.CountryId != Guid.Empty);
        Assert.Contains(response, countriesFromGetAllCountries);
    }

    #endregion

    #region GetAllCountries

    [Fact]
    //The list of countries should be empty by default (before adding any countries)
    public void GetAllCountries_EmptyList()
    {
        //Act
        List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();

        //Assert
        Assert.Empty(actualCountryResponseList);
    }

    [Fact]
    public void GetAllCountries_AddFewCountries()
    {
        //Arrange
        List<CountryAddRequest> countryRequestList = new List<CountryAddRequest>() { 
            new CountryAddRequest() { CountryName = "USA" },
            new CountryAddRequest() { CountryName = "UK" }
        };

        //Act
        List<CountryResponse?> countriesListFromAddCountry = new List<CountryResponse?>();

        foreach (CountryAddRequest countryRequest in countryRequestList)
        {
            countriesListFromAddCountry.Add(_countriesService.AddCountry(countryRequest));
        }

        List<CountryResponse> actualCountryResponseList = _countriesService.GetAllCountries();

        //read each element from countries_list_from_add_country
        foreach (CountryResponse? expectedCountry in countriesListFromAddCountry)
        {
            Assert.Contains(expectedCountry, actualCountryResponseList);
        }
    }
    #endregion

    #region GetCountryByCountryID

    [Fact]
    //If we supply null as CountryID, it should return null as CountryResponse
    public void GetCountryByCountryID_NullCountryID()
    {
        //Arrange
        Guid? countryId = null;

        //Act
        CountryResponse? countryResponseFromGetMethod = 
            _countriesService.GetCountryByCountryId(countryId);

        //Assert
        Assert.Null(countryResponseFromGetMethod);
    }


    [Fact]
    //If we supply a valid country id, it should return the matching country details as CountryResponse object
    public void GetCountryByCountryID_ValidCountryID()
    {
        //Arrange
        CountryAddRequest countryAddRequest = 
            new CountryAddRequest { CountryName = "China" };
        CountryResponse? countryResponseFromAdd = 
            _countriesService.AddCountry(countryAddRequest);

        //Act
        CountryResponse? countryResponseFromGet = 
            _countriesService.GetCountryByCountryId(countryResponseFromAdd.CountryId);

        //Assert
        Assert.Equal(countryResponseFromAdd, countryResponseFromGet);
    }
    #endregion

}