﻿using Entities;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enum;
using Services;
using Xunit.Abstractions;

namespace CRUDTests;

public class PersonServiceTest
{
    private readonly IPersonService _personService = new PersonService(false);
    private readonly ICountriesService _countriesService = new CountriesService(false);
    private readonly ITestOutputHelper _testOutputHelper;

    public PersonServiceTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    #region AddPerson

    [Fact]
    public void AddPerson_NullPerson()
    {
        //Arrange
        PersonAddRequest? personAddRequest = null;

        //Act
        Assert.Throws<ArgumentNullException>(() =>
        {
            _personService.AddPerson(personAddRequest);
        });
    }

    //When we supply null value as PersonName, it should throw ArgumentException
    [Fact]
    public void AddPerson_PersonNameIsNull()
    {
        //Arrange
        PersonAddRequest personAddRequest =
            new PersonAddRequest() { PersonName = null };

        //Act
        Assert.Throws<ArgumentException>(() =>
        {
            _personService.AddPerson(personAddRequest);
        });
    }

    //When we supply proper person details, it should insert the person into the persons list; and it should return an object of PersonResponse, which includes with the newly generated person id
    [Fact]
    public void AddPerson_ProperPersonDetails()
    {
        //Arrange
        PersonAddRequest personAddRequest =
            new PersonAddRequest
            {
                PersonName = "PersonModel name...",
                Email = "person@example.com",
                Address = "sample address",
                CountryID = Guid.NewGuid(),
                Gender = GenderOptions.Male,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                ReceiveNewsLetters = true
            };

        //Act
        PersonResponse personResponseFromAdd = _personService.AddPerson(personAddRequest);

        List<PersonResponse> personsList = _personService.GetAllPersons();

        //Assert
        Assert.True(personResponseFromAdd.PersonId != Guid.Empty);
        Assert.Contains(personResponseFromAdd, personsList);
    }

    #endregion

    #region GetPersonByPersonID

    //If we supply null as PersonID, it should return null as PersonResponse
    [Fact]
    public void GetPersonByPersonID_NullPersonID()
    {
        //Arrange
        Guid? personId = null;

        //Act
        PersonResponse? personResponseFromGet =
            _personService.GetPersonByPersonId(personId);

        //Assert
        Assert.Null(personResponseFromGet);
    }


    //If we supply a valid person id, it should return the valid person details as PersonResponse object
    [Fact]
    public void GetPersonByPersonID_WithPersonID()
    {
        //Arrange
        CountryAddRequest countryRequest =
            new CountryAddRequest { CountryName = "Canada" };
        CountryResponse? countryResponse =
            _countriesService.AddCountry(countryRequest);
        PersonAddRequest personRequest =
            new PersonAddRequest
            {
                PersonName = "person name...",
                Email = "email@sample.com",
                Address = "address",
                CountryID = countryResponse?.CountryId,
                DateOfBirth = DateTime.Parse("2000-01-01"),
                Gender = GenderOptions.Male,
                ReceiveNewsLetters = false
            };
        PersonResponse personResponseFromAdd =
            _personService.AddPerson(personRequest);
        PersonResponse? personResponseFromGet =
            _personService.GetPersonByPersonId(personResponseFromAdd.PersonId);
        //Assert
        Assert.Equal(personResponseFromAdd, personResponseFromGet);
    }

    #endregion

    #region GetAllPersons

    //The GetAllPersons() should return an empty list by default
    [Fact]
    public void GetAllPersons_EmptyList()
    {
        //Act
        List<PersonResponse> personsFromGet = _personService.GetAllPersons();

        //Assert
        Assert.Empty(personsFromGet);
    }


    //First, we will add few persons; and then when we call GetAllPersons(), it should return the same persons that were added
    [Fact]
    public void GetAllPersons_AddFewPersons()
    {
        //Arrange
        CountryAddRequest countryRequest1 = new CountryAddRequest
        {
            CountryName = "USA"
        };
        CountryAddRequest countryRequest2 = new CountryAddRequest
        {
            CountryName = "India"
        };

        CountryResponse? countryResponse1 = _countriesService.AddCountry(countryRequest1);
        CountryResponse? countryResponse2 = _countriesService.AddCountry(countryRequest2);

        PersonAddRequest personRequest1 = new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "smith@example.com",
            Gender = GenderOptions.Male,
            Address = "address of smith",
            CountryID = countryResponse1?.CountryId,
            DateOfBirth = DateTime.Parse("2002-05-06"),
            ReceiveNewsLetters = true
        };
        PersonAddRequest personRequest2 = new PersonAddRequest
        {
            PersonName = "Mary",
            Email = "mary@example.com",
            Gender = GenderOptions.Female,
            Address = "address of mary",
            CountryID = countryResponse2?.CountryId,
            DateOfBirth = DateTime.Parse("2000-02-02"),
            ReceiveNewsLetters = false
        };
        PersonAddRequest personRequest3 = new PersonAddRequest
        {
            PersonName = "Rahman",
            Email = "rahman@example.com",
            Gender = GenderOptions.Male,
            Address = "address of rahman",
            CountryID = countryResponse2?.CountryId,
            DateOfBirth = DateTime.Parse("1999-03-03"),
            ReceiveNewsLetters = true
        };

        List<PersonAddRequest> personRequests = new List<PersonAddRequest>
        {
            personRequest1,
            personRequest2,
            personRequest3
        };

        List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();

        foreach (PersonAddRequest personRequest in personRequests)
        {
            PersonResponse personResponse = _personService.AddPerson(personRequest);
            personResponseListFromAdd.Add(personResponse);
        }

        //print person_response_list_from_add
        _testOutputHelper.WriteLine("Expected:");
        foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
        {
            _testOutputHelper.WriteLine(personResponseFromAdd.ToString());
        }

        //Act
        List<PersonResponse> personsListFromGet = _personService.GetAllPersons();

        //print persons_list_from_get
        _testOutputHelper.WriteLine("Actual:");
        foreach (PersonResponse personResponseFromGet in personsListFromGet)
        {
            _testOutputHelper.WriteLine(personResponseFromGet.ToString());
        }
        //Assert
        foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
        {
            Assert.Contains(personResponseFromAdd, personsListFromGet);
        }
    }
    #endregion

    #region GetFilteredPersons

    //If the search text is empty and search by is "PersonName", it should return all persons
    [Fact]
    public void GetFilteredPersons_EmptySearchText()
    {
        //Arrange
        CountryAddRequest countryRequest1 = new CountryAddRequest
        {
            CountryName = "USA"
        };
        CountryAddRequest countryRequest2 = new CountryAddRequest
        {
            CountryName = "India"
        };

        CountryResponse? countryResponse1 = _countriesService.AddCountry(countryRequest1);
        CountryResponse? countryResponse2 = _countriesService.AddCountry(countryRequest2);

        PersonAddRequest personRequest1 = new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "smith@example.com",
            Gender = GenderOptions.Male,
            Address = "address of smith",
            CountryID = countryResponse1?.CountryId,
            DateOfBirth = DateTime.Parse("2002-05-06"),
            ReceiveNewsLetters = true
        };

        PersonAddRequest personRequest2 = new PersonAddRequest
        {
            PersonName = "Mary",
            Email = "mary@example.com",
            Gender = GenderOptions.Female,
            Address = "address of mary",
            CountryID = countryResponse2?.CountryId,
            DateOfBirth = DateTime.Parse("2000-02-02"),
            ReceiveNewsLetters = false
        };

        PersonAddRequest personRequest3 = new PersonAddRequest
        {
            PersonName = "Rahman",
            Email = "rahman@example.com",
            Gender = GenderOptions.Male,
            Address = "address of rahman",
            CountryID = countryResponse2?.CountryId,
            DateOfBirth = DateTime.Parse("1999-03-03"),
            ReceiveNewsLetters = true
        };

        List<PersonAddRequest> personRequests = new List<PersonAddRequest>
        {
            personRequest1,
            personRequest2,
            personRequest3
        };

        List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();

        foreach (PersonAddRequest personRequest in personRequests)
        {
            PersonResponse personResponse = _personService.AddPerson(personRequest);
            personResponseListFromAdd.Add(personResponse);
        }

        //print person_response_list_from_add
        _testOutputHelper.WriteLine("Expected:");
        foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
        {
            _testOutputHelper.WriteLine(personResponseFromAdd.ToString());
        }

        //Act
        List<PersonResponse> personsListFromSearch = _personService.GetFilteredPersons(nameof(Person.PersonName), "");

        //print persons_list_from_get
        _testOutputHelper.WriteLine("Actual:");
        foreach (PersonResponse personResponseFromGet in personsListFromSearch)
        {
            _testOutputHelper.WriteLine(personResponseFromGet.ToString());
        }

        //Assert
        foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
        {
            Assert.Contains(personResponseFromAdd, personsListFromSearch);
        }
    }


    //First we will add few persons; and then we will search based on person name with some search string. It should return the matching persons
    [Fact]
    public void GetFilteredPersons_SearchByPersonName()
    {
        //Arrange
        CountryAddRequest countryRequest1 = new CountryAddRequest
        {
            CountryName = "USA"
        };
        CountryAddRequest countryRequest2 = new CountryAddRequest
        {
            CountryName = "India"
        };

        CountryResponse? countryResponse1 = _countriesService.AddCountry(countryRequest1);
        CountryResponse? countryResponse2 = _countriesService.AddCountry(countryRequest2);
        PersonAddRequest personRequest1 = new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "smith@example.com",
            Gender = GenderOptions.Male,
            Address = "address of smith",
            CountryID = countryResponse1?.CountryId,
            DateOfBirth = DateTime.Parse("2002-05-06"),
            ReceiveNewsLetters = true
        };
        PersonAddRequest personRequest2 = new PersonAddRequest
        {
            PersonName = "Mary",
            Email = "mary@example.com",
            Gender = GenderOptions.Female,
            Address = "address of mary",
            CountryID = countryResponse2?.CountryId,
            DateOfBirth = DateTime.Parse("2000-02-02"),
            ReceiveNewsLetters = false
        };
        PersonAddRequest personRequest3 = new PersonAddRequest
        {
            PersonName = "Rahman",
            Email = "rahman@example.com",
            Gender = GenderOptions.Male,
            Address = "address of rahman",
            CountryID = countryResponse2?.CountryId,
            DateOfBirth = DateTime.Parse("1999-03-03"),
            ReceiveNewsLetters = true
        };
        List<PersonAddRequest> personRequests = new List<PersonAddRequest>
        {
            personRequest1,
            personRequest2,
            personRequest3
        };
        List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();

        foreach (PersonAddRequest personRequest in personRequests)
        {
            PersonResponse personResponse = _personService.AddPerson(personRequest);
            personResponseListFromAdd.Add(personResponse);
        }

        //print person_response_list_from_add
        _testOutputHelper.WriteLine("Expected:");
        foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
            _testOutputHelper.WriteLine(personResponseFromAdd.ToString());

        //Act
        List<PersonResponse> personsListFromSearch =
            _personService.GetFilteredPersons(nameof(Person.PersonName), "ma");

        //print persons_list_from_get
        _testOutputHelper.WriteLine("Actual:");
        foreach (PersonResponse personResponseFromGet in personsListFromSearch)
            _testOutputHelper.WriteLine(personResponseFromGet.ToString());

        //Assert
        foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
        {
            if (personResponseFromAdd.PersonName == null) continue;
            if (!personResponseFromAdd.PersonName.Contains("ma", StringComparison.OrdinalIgnoreCase)) continue;
            Assert.Contains(personResponseFromAdd, personsListFromSearch);
        }
    }

    #endregion

    #region GetSortedPersons

    //When we sort based on PersonName in DESC, it should return persons list in descending on PersonName
    [Fact]
    public void GetSortedPersons()
    {
        //Arrange
        CountryAddRequest countryRequest1 = new CountryAddRequest { CountryName = "USA" };
        CountryAddRequest countryRequest2 = new CountryAddRequest { CountryName = "India" };

        CountryResponse? countryResponse1 = _countriesService.AddCountry(countryRequest1);
        CountryResponse? countryResponse2 = _countriesService.AddCountry(countryRequest2);

        PersonAddRequest personRequest1 = new PersonAddRequest
        {
            PersonName = "Smith",
            Email = "smith@example.com",
            Gender = GenderOptions.Male,
            Address = "address of smith",
            CountryID = countryResponse1?.CountryId,
            DateOfBirth = DateTime.Parse("2002-05-06"),
            ReceiveNewsLetters = true
        };
        PersonAddRequest personRequest2 = new PersonAddRequest
        {
            PersonName = "Mary",
            Email = "mary@example.com",
            Gender = GenderOptions.Female,
            Address = "address of mary",
            CountryID = countryResponse2?.CountryId,
            DateOfBirth = DateTime.Parse("2000-02-02"),
            ReceiveNewsLetters = false
        };
        PersonAddRequest personRequest3 = new PersonAddRequest
        {
            PersonName = "Rahman",
            Email = "rahman@example.com",
            Gender = GenderOptions.Male,
            Address = "address of rahman",
            CountryID = countryResponse2?.CountryId,
            DateOfBirth = DateTime.Parse("1999-03-03"),
            ReceiveNewsLetters = true
        };
        List<PersonAddRequest> personRequests = new List<PersonAddRequest>
      {
          personRequest1,
          personRequest2,
          personRequest3
      };
        List<PersonResponse> personResponseListFromAdd = new List<PersonResponse>();
        foreach (PersonAddRequest personRequest in personRequests)
        {
            PersonResponse personResponse = _personService.AddPerson(personRequest);
            personResponseListFromAdd.Add(personResponse);
        }

        //print person_response_list_from_add
        _testOutputHelper.WriteLine("Expected:");
        foreach (PersonResponse personResponseFromAdd in personResponseListFromAdd)
        {
            _testOutputHelper.WriteLine(personResponseFromAdd.ToString());
        }
        List<PersonResponse> allPersons = _personService.GetAllPersons();

        //Act
        List<PersonResponse> personsListFromSort = _personService.GetSortedPersons(allPersons,
            nameof(Person.PersonName),
            SortOrderOptions.DESC);

        //print persons_list_from_get
        _testOutputHelper.WriteLine("Actual:");
        foreach (PersonResponse personResponseFromGet in personsListFromSort)
        {
            _testOutputHelper.WriteLine(personResponseFromGet.ToString());
        }
        personResponseListFromAdd = personResponseListFromAdd.OrderByDescending(temp => temp.PersonName).ToList();

        //Assert
        for (int i = 0; i < personResponseListFromAdd.Count; i++)
        {
            Assert.Equal(personResponseListFromAdd[i], personsListFromSort[i]);
        }
    }
    #endregion

    #region UpdatePerson

    //When we supply null as PersonUpdateRequest, it should throw ArgumentNullException
    [Fact]
    public void UpdatePerson_NullPerson()
    {
        //Arrange
        PersonUpdateRequest? personUpdateRequest = null;

        //Assert
        Assert.Throws<ArgumentNullException>(() =>
        {
            //Act
            _personService.UpdatePerson(personUpdateRequest);
        });
    }


    //When we supply invalid person id, it should throw ArgumentException
    [Fact]
    public void UpdatePerson_InvalidPersonID()
    {
        //Arrange
        PersonUpdateRequest personUpdateRequest = new PersonUpdateRequest
        {
            PersonId = Guid.NewGuid()
        };

        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _personService.UpdatePerson(personUpdateRequest);
        });
    }

    //When PersonName is null, it should throw ArgumentException
    [Fact]
    public void UpdatePerson_PersonNameIsNull()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest
        {
            CountryName = "UK"
        };
        CountryResponse? countryResponseFromAdd = _countriesService.AddCountry(countryAddRequest);

        PersonAddRequest personAddRequest = new PersonAddRequest
        {
            PersonName = "John",
            CountryID = countryResponseFromAdd?.CountryId,
            Email = "john@example.com",
            Address = "address...",
            Gender = GenderOptions.Male
        };

        PersonResponse personResponseFromAdd = _personService.AddPerson(personAddRequest);

        PersonUpdateRequest personUpdateRequest = personResponseFromAdd.ToPersonUpdateRequest();
        personUpdateRequest.PersonName = null;


        //Assert
        Assert.Throws<ArgumentException>(() =>
        {
            //Act
            _personService.UpdatePerson(personUpdateRequest);
        });

    }

    //First, add a new person and try to update the person name and email
    [Fact]
    public void UpdatePerson_PersonFullDetailsUpdation()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest
        {
            CountryName = "UK"
        };
        CountryResponse? countryResponseFromAdd = _countriesService.AddCountry(countryAddRequest);

        PersonAddRequest personAddRequest = new PersonAddRequest
        {
            PersonName = "John",
            CountryID = countryResponseFromAdd?.CountryId,
            Address = "Abc road",
            DateOfBirth = DateTime.Parse("2000-01-01"),
            Email = "abc@example.com",
            Gender = GenderOptions.Male,
            ReceiveNewsLetters = true
        };

        PersonResponse personResponseFromAdd = _personService.AddPerson(personAddRequest);

        PersonUpdateRequest personUpdateRequest = personResponseFromAdd.ToPersonUpdateRequest();
        personUpdateRequest.PersonName = "William";
        personUpdateRequest.Email = "william@example.com";

        //Act
        PersonResponse personResponseFromUpdate = _personService.UpdatePerson(personUpdateRequest);

        PersonResponse? personResponseFromGet = _personService.GetPersonByPersonId(personResponseFromUpdate.PersonId);
        //Assert
        Assert.Equal(personResponseFromGet, personResponseFromUpdate);
    }

    #endregion
    #region DeletePerson

    //If you supply an valid PersonID, it should return true
    [Fact]
    public void DeletePerson_ValidPersonID()
    {
        //Arrange
        CountryAddRequest countryAddRequest = new CountryAddRequest
        {
            CountryName = "USA"
        };
        CountryResponse? countryResponseFromAdd = _countriesService.AddCountry(countryAddRequest);

        PersonAddRequest personAddRequest = new PersonAddRequest
        {
            PersonName = "Jones",
            Address = "address",
            CountryID = countryResponseFromAdd?.CountryId,
            DateOfBirth = Convert.ToDateTime("2010-01-01"),
            Email = "jones@example.com",
            Gender = GenderOptions.Male,
            ReceiveNewsLetters = true
        };

        PersonResponse personResponseFromAdd = _personService.AddPerson(personAddRequest);


        //Act
        bool isDeleted = _personService.DeletePerson(personResponseFromAdd.PersonId);

        //Assert
        Assert.True(isDeleted);
    }


    //If you supply an invalid PersonID, it should return false
    [Fact]
    public void DeletePerson_InvalidPersonID()
    {
        //Act
        bool isDeleted = _personService.DeletePerson(Guid.NewGuid());

        //Assert
        Assert.False(isDeleted);
    }

    #endregion
}