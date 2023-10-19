using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enum;
using Services;
namespace CRUDTests;

public class PersonServiceTest
{
    private readonly IPersonService _personService = new PersonService();
    private readonly ICountriesService _countriesService = new CountriesService();

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
        PersonAddRequest? personAddRequest =
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
        PersonAddRequest? personAddRequest =
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
        Guid? personID = null;

        //Act
        PersonResponse? personResponseFromGet = 
            _personService.GetPersonByPersonId(personID);

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
        CountryResponse countryResponse = 
            _countriesService.AddCountry(countryRequest);
        PersonAddRequest personRequest = 
            new PersonAddRequest
            {
                PersonName = "person name...", 
                Email = "email@sample.com", 
                Address = "address", 
                CountryID = countryResponse.CountryId, 
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
}