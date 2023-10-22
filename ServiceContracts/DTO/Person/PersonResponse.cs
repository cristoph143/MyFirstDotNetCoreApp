using Entities;
using ServiceContracts.Enum;

namespace ServiceContracts.DTO;

public class PersonResponse
{
    public Guid PersonId { get; set; }
    public string? PersonName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public Guid? CountryId { get; set; }
    public string? Country { get; set; }
    public string? Address { get; set; }
    public bool ReceiveNewsLetters { get; set; }
    public double? Age { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;

        if (obj.GetType() != typeof(PersonResponse)) return false;

        PersonResponse person = (PersonResponse)obj;
        return
            PersonId == person.PersonId &&
            PersonName == person.PersonName &&
            Email == person.Email &&
            DateOfBirth == person.DateOfBirth &&
            Gender == person.Gender &&
            CountryId == person.CountryId &&
            Address == person.Address &&
            ReceiveNewsLetters == person.ReceiveNewsLetters;
    }
    public override int GetHashCode() => base.GetHashCode();

    public override string ToString()
    {
        return
            $"PersonModel ID: {PersonId}, " +
            $"PersonModel Name: {PersonName}, " +
            $"Email: {Email}, " +
            $"Date of Birth: {DateOfBirth?.ToString("dd MMM yyyy")}, " +
            $"Gender: {Gender}, Country ID: {CountryId}, " +
            $"Country: {Country}, " +
            $"Address: {Address}, " +
            $"Receive News Letters: {ReceiveNewsLetters}";
    }

    public PersonUpdateRequest ToPersonUpdateRequest()
    {
        return new PersonUpdateRequest()
        {
            PersonId = PersonId,
            PersonName = PersonName,
            Email = Email,
            DateOfBirth = DateOfBirth,
            Gender = (GenderOptions)System.Enum.Parse(typeof(GenderOptions),
                Gender,
                true),
            Address = Address,
            CountryId = CountryId,
            ReceiveNewsLetters = ReceiveNewsLetters
        };
    }
}

public static class PersonExtensions
{
    /// <summary>
    /// An extension method to convert an object of PersonModel class into PersonResponse class
    /// </summary>
    /// <param name="person">The PersonModel object to convert</param>
    /// /// <returns>Returns the converted PersonResponse object</returns>
    public static PersonResponse ToPersonResponse(this Person person) =>
        //person => convert => PersonResponse
        new()
        {
            PersonId = person.PersonId,
            PersonName = person.PersonName,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth,
            ReceiveNewsLetters = person.ReceiveNewsLetters,
            Address = person.Address,
            CountryId = person.CountryId,
            Gender = person.Gender,
            Age = person.DateOfBirth != null ?
                Math.Round((
                    DateTime.Now - person.DateOfBirth.Value
                ).TotalDays / 365.25) : null
        };
}