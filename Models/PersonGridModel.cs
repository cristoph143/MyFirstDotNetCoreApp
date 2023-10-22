namespace MyFirstDotNetCoreApp.Models;

public class PersonGridModel
{
    public string GridTitle { get; set; } = string.Empty;
    public List<PersonModel> Persons { get; set; } = new();
}