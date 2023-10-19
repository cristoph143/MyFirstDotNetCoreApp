using Entities;

namespace ServiceContracts.DTO;

public class PersonResponse
{
    public Guid PersonID { get; set; }
    public string? PersonName { get; set; }
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public Guid? CountryID { get; set; }
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
            PersonID == person.PersonID &&
            PersonName == person.PersonName &&
            Email == person.Email &&
            DateOfBirth == person.DateOfBirth &&
            Gender == person.Gender &&
            CountryID == person.CountryID &&
            Address == person.Address &&
            ReceiveNewsLetters == person.ReceiveNewsLetters;
    }
}

public static class PersonExtensions
{
    /// <summary>
    /// An extension method to convert an object of Person class into PersonResponse class
    /// </summary>
    /// <param name="person">The Person object to convert</param>
    /// /// <returns>Returns the converted PersonResponse object</returns>
    public static PersonResponse ToPersonResponse(this Person person) =>
        //person => convert => PersonResponse
        new()
        {
            PersonID = person.PersonId,
            PersonName = person.PersonName,
            Email = person.Email,
            DateOfBirth = person.DateOfBirth,
            ReceiveNewsLetters = person.ReceiveNewsLetters,
            Address = person.Address,
            CountryID = person.CountryId,
            Gender = person.Gender,
            Age = person.DateOfBirth != null ?
                Math.Round((
                    DateTime.Now - person.DateOfBirth.Value
                ).TotalDays / 365.25) : null
        };
}