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

    /// <summary>
    /// Returns all person objects that matches with the given search field and search string
    /// </summary>
    /// <param name="searchBy">Search field to search</param>
    /// <param name="searchString">Search string to search</param>
    /// <returns>Returns all matching persons based on the given search field and search string</returns>
    List<PersonResponse> GetFilteredPersons(string searchBy, string? searchString);
}