﻿using ServiceContracts.DTO;

namespace ServiceContracts;

public interface IPersonService
{
    /// <summary>
    /// Addds a new person into the list of persons
    /// </summary>
    /// <param name="personAddRequest">Person to add</param>
    /// <returns>Returns the same person details, along with newly generated PersonID</returns>
    PersonResponse AddPerson(PersonAddRequest? personAddRequest);


    /// <summary>
    /// Returns all persons
    /// </summary>
    /// <returns>Returns a list of objects of PersonResponse type</returns>
    List<PersonResponse> GetAllPersons();


    /// <summary>
    /// Returns the person object based on the given person id
    /// </summary>
    /// <param name="personID">Person id to search</param>
    /// <returns>Returns matching person object</returns>
    PersonResponse? GetPersonByPersonId(Guid? personID);
}