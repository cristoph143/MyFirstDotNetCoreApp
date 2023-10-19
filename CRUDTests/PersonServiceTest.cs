using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enum;
using Services;
using Xunit.Abstractions;

namespace CRUDTests;

public class PersonServiceTest
{
    private readonly IPersonService _personService = new PersonService();
    private readonly ICountriesService _countriesService = new CountriesService();
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
                PersonName = "Person name...",
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
}