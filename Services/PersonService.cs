using System.ComponentModel.DataAnnotations;
using System.Globalization;
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
        _persons.Select(
            temp => temp.ToPersonResponse()
        ).ToList();

    public PersonResponse? GetPersonByPersonId(Guid? personId)
    {
        if (personId == null)
            return null;

        Person? person = _persons.FirstOrDefault(temp => temp.PersonId == personId);
        return person?.ToPersonResponse();
    }

    public List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString)
    {
        List<PersonResponse> allPersons = GetAllPersons();

        if (string.IsNullOrEmpty(searchBy) || string.IsNullOrEmpty(searchString))
        {
            return allPersons;
        }

        return searchBy switch
        {
            nameof(Person.PersonName) => allPersons.Where(temp =>
                string.IsNullOrEmpty(temp.PersonName) ||
                temp.PersonName.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList(),

            nameof(Person.Email) => allPersons.Where(temp =>
                string.IsNullOrEmpty(temp.Email) ||
                temp.Email.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList(),

            nameof(Person.DateOfBirth) => allPersons.Where(temp =>
                temp.DateOfBirth?.ToString("dd MMMM yyyy", CultureInfo.InvariantCulture)
                    .Contains(searchString, StringComparison.OrdinalIgnoreCase) == true).ToList(),

            nameof(Person.Gender) => allPersons.Where(temp =>
                string.IsNullOrEmpty(temp.Gender) ||
                temp.Gender.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList(),

            nameof(Person.CountryId) => allPersons.Where(temp =>
                string.IsNullOrEmpty(temp.Country) ||
                temp.Country.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList(),

            nameof(Person.Address) => allPersons.Where(temp =>
                string.IsNullOrEmpty(temp.Address) ||
                temp.Address.Contains(searchString, StringComparison.OrdinalIgnoreCase)).ToList(),

            _ => allPersons
        };
    }

    public List<PersonResponse> GetSortedPersons(List<PersonResponse> allPersons, string sortBy, SortOrderOptions sortOrder)
    {
        throw new NotImplementedException();
    }
}