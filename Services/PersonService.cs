using System.ComponentModel.DataAnnotations;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class PersonService : IPersonService
{
    //private field
    private readonly List<Person> _persons = new();
    private readonly ICountriesService _countriesService = new CountriesService();


    private PersonResponse ConvertPersonToPersonResponse(Person person)
    {
        PersonResponse personResponse = person.ToPersonResponse();
        personResponse.Country =
            _countriesService.GetCountryByCountryId(
                person.CountryId)?.CountryName;
        return personResponse;
    }

    public PersonResponse AddPerson(PersonAddRequest? personAddRequest)
    {
        //check if PersonAddRequest is not null
        if (personAddRequest == null)
            throw new ArgumentNullException(nameof(personAddRequest));

        // Validate PersonName
        ValidationHelper.ModelValidation(personAddRequest);
        //convert personAddRequest into Person type
        Person person = personAddRequest.ToPerson();

        //generate PersonID
        person.PersonId = Guid.NewGuid();
        //add person object to persons list
        _persons.Add(person);
        //convert the Person object into PersonResponse type
        return ConvertPersonToPersonResponse(person);
    }

    public List<PersonResponse> GetAllPersons() =>
                throw new NotImplementedException();

    public PersonResponse? GetPersonByPersonId(Guid? personId)
    {
        if (personId == null)
            return null;

        Person? person = _persons.FirstOrDefault(temp => temp.PersonId == personId);
        return person?.ToPersonResponse();
    }
}