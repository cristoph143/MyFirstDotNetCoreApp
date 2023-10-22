using System.Globalization;
using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services;

public class PersonService : IPersonService
{
    //private field
    private readonly List<Person> _persons;
    private readonly ICountriesService _countriesService;

    //constructor
    public PersonService(bool initialize = true)
    {
        _persons = new List<Person>();
        _countriesService = new CountriesService();
        if (initialize)
        {
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("8082ED0C-396D-4162-AD1D-29A13F929824"),
                PersonName = "Aguste",
                Email = "aleddy0@booking.com",
                DateOfBirth = DateTime.Parse("1993-01-02"),
                Gender = "Male",
                Address = "0858 Novick Terrace",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("000C76EB-62E9-4465-96D1-2C41FDB64C3B")
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("06D15BAD-52F4-498E-B478-ACAD847ABFAA"),
                PersonName = "Jasmina",
                Email = "jsyddie1@miibeian.gov.cn",
                DateOfBirth = DateTime.Parse("1991-06-24"),
                Gender = "Female",
                Address = "0742 Fieldstone Lane",
                ReceiveNewsLetters = true,
                CountryId = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F")
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("D3EA677A-0F5B-41EA-8FEF-EA2FC41900FD"),
                PersonName = "Kendall",
                Email = "khaquard2@arstechnica.com",
                DateOfBirth = DateTime.Parse("1993-08-13"),
                Gender = "Male",
                Address = "7050 Pawling Alley",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("32DA506B-3EBA-48A4-BD86-5F93A2E19E3F")
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("89452EDB-BF8C-4283-9BA4-8259FD4A7A76"),
                PersonName = "Kilian",
                Email = "kaizikowitz3@joomla.org",
                DateOfBirth = DateTime.Parse("1991-06-17"),
                Gender = "Male",
                Address = "233 Buhler Junction",
                ReceiveNewsLetters = true,
                CountryId = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E")
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("F5BD5979-1DC1-432C-B1F1-DB5BCCB0E56D"),
                PersonName = "Dulcinea",
                Email = "dbus4@pbs.org",
                DateOfBirth = DateTime.Parse("1996-09-02"),
                Gender = "Female",
                Address = "56 Sundown Point",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("DF7C89CE-3341-4246-84AE-E01AB7BA476E")
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("A795E22D-FAED-42F0-B134-F3B89B8683E5"),
                PersonName = "Corabelle",
                Email = "cadams5@t-online.de",
                DateOfBirth = DateTime.Parse("1993-10-23"),
                Gender = "Female",
                Address = "4489 Hazelcrest Place",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("15889048-AF93-412C-B8F3-22103E943A6D")
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("3C12D8E8-3C1C-4F57-B6A4-C8CAAC893D7A"),
                PersonName = "Faydra",
                Email = "fbischof6@boston.com",
                DateOfBirth = DateTime.Parse("1996-02-14"),
                Gender = "Female",
                Address = "2010 Farragut Pass",
                ReceiveNewsLetters = true,
                CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB")
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("7B75097B-BFF2-459F-8EA8-63742BBD7AFB"),
                PersonName = "Oby",
                Email = "oclutheram7@foxnews.com",
                DateOfBirth = DateTime.Parse("1992-05-31"),
                Gender = "Male",
                Address = "2 Fallview Plaza",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB")
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("6717C42D-16EC-4F15-80D8-4C7413E250CB"),
                PersonName = "Seumas",
                Email = "ssimonitto8@biglobe.ne.jp",
                DateOfBirth = DateTime.Parse("1999-02-02"),
                Gender = "Male",
                Address = "76779 Norway Maple Crossing",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB")
            });
            _persons.Add(new Person()
            {
                PersonId = Guid.Parse("6E789C86-C8A6-4F18-821C-2ABDB2E95982"),
                PersonName = "Freemon",
                Email = "faugustin9@vimeo.com",
                DateOfBirth = DateTime.Parse("1996-04-27"),
                Gender = "Male",
                Address = "8754 Becker Street",
                ReceiveNewsLetters = false,
                CountryId = Guid.Parse("80DF255C-EFE7-49E5-A7F9-C35D7C701CAB")
            });
        }
    }
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
        //convert personAddRequest into PersonModel type
        Person person = personAddRequest.ToPerson();

        //generate PersonID
        person.PersonId = Guid.NewGuid();
        //add person object to persons list
        _persons.Add(person);
        //convert the PersonModel object into PersonResponse type
        return ConvertPersonToPersonResponse(person);
    }

    public List<PersonResponse> GetAllPersons() =>
        _persons.Select(
            ConvertPersonToPersonResponse
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
        if (string.IsNullOrEmpty(sortBy))
            return allPersons;

        List<PersonResponse> sortedPersons = sortOrder switch
        {
            SortOrderOptions.ASC => sortBy switch
            {
                nameof(PersonResponse.PersonName) => allPersons.OrderBy(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
                nameof(PersonResponse.Email) => allPersons.OrderBy(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                nameof(PersonResponse.DateOfBirth) => allPersons.OrderBy(temp => temp.DateOfBirth).ToList(),
                nameof(PersonResponse.Age) => allPersons.OrderBy(temp => temp.Age).ToList(),
                nameof(PersonResponse.Gender) => allPersons.OrderBy(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
                nameof(PersonResponse.Country) => allPersons.OrderBy(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
                nameof(PersonResponse.Address) => allPersons.OrderBy(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
                nameof(PersonResponse.ReceiveNewsLetters) => allPersons.OrderBy(temp => temp.ReceiveNewsLetters).ToList(),
                _ => allPersons
            },
            SortOrderOptions.DESC => sortBy switch
            {
                nameof(PersonResponse.PersonName) => allPersons.OrderByDescending(temp => temp.PersonName, StringComparer.OrdinalIgnoreCase).ToList(),
                nameof(PersonResponse.Email) => allPersons.OrderByDescending(temp => temp.Email, StringComparer.OrdinalIgnoreCase).ToList(),
                nameof(PersonResponse.DateOfBirth) => allPersons.OrderByDescending(temp => temp.DateOfBirth).ToList(),
                nameof(PersonResponse.Age) => allPersons.OrderByDescending(temp => temp.Age).ToList(),
                nameof(PersonResponse.Gender) => allPersons.OrderByDescending(temp => temp.Gender, StringComparer.OrdinalIgnoreCase).ToList(),
                nameof(PersonResponse.Country) => allPersons.OrderByDescending(temp => temp.Country, StringComparer.OrdinalIgnoreCase).ToList(),
                nameof(PersonResponse.Address) => allPersons.OrderByDescending(temp => temp.Address, StringComparer.OrdinalIgnoreCase).ToList(),
                nameof(PersonResponse.ReceiveNewsLetters) => allPersons.OrderByDescending(temp => temp.ReceiveNewsLetters).ToList(),
                _ => allPersons
            },
            _ => allPersons
        };

        return sortedPersons;
    }

    public PersonResponse UpdatePerson(PersonUpdateRequest? personUpdateRequest)
    {
        if (personUpdateRequest == null)
            throw new ArgumentNullException(nameof(Person));

        //validation
        ValidationHelper.ModelValidation(personUpdateRequest);

        //get matching person object to update
        Person? matchingPerson = _persons.FirstOrDefault(
            temp => temp.PersonId == personUpdateRequest.PersonId);
        if (matchingPerson == null)
            throw new ArgumentException("Given person id doesn't exist");

        //update all details
        matchingPerson.PersonName = personUpdateRequest.PersonName;
        matchingPerson.Email = personUpdateRequest.Email;
        matchingPerson.DateOfBirth = personUpdateRequest.DateOfBirth;
        matchingPerson.Gender = personUpdateRequest.Gender.ToString();
        matchingPerson.CountryId = personUpdateRequest.CountryId;
        matchingPerson.Address = personUpdateRequest.Address;
        matchingPerson.ReceiveNewsLetters = personUpdateRequest.ReceiveNewsLetters;

        return matchingPerson.ToPersonResponse();
    }

    public bool DeletePerson(Guid? personId)
    {
        if (personId == null)
            throw new ArgumentNullException(nameof(personId));

        Person? person = _persons.FirstOrDefault(temp => temp.PersonId == personId);
        if (person == null)
            return false;

        _persons.RemoveAll(temp => temp.PersonId == personId);
        return true;
    }
}