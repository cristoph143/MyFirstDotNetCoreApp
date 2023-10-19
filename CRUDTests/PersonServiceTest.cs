using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enum;
using Services;

namespace CRUDTests;

public class PersonServiceTest
{
    private readonly IPersonService _personService;

    public PersonServiceTest()
    {
        _personService = new PersonService();
    }

    #region MyRegion

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
        Assert.True(personResponseFromAdd.PersonID != Guid.Empty);
        Assert.Contains(personResponseFromAdd, personsList);
    }

    #endregion
}