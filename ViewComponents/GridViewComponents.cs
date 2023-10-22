using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.ViewComponents;

public class GridViewComponent : ViewComponent
{
    // public async Task<IViewComponentResult> InvokeAsync()
    // {
    //     PersonGridModel model = new PersonGridModel()
    //     {
    //         GridTitle = "Persons List",
    //         Persons = new List<PersonModel>() {
    //         new PersonModel() { PersonName = "John", JobTitle = "Manager" },
    //         new PersonModel() { PersonName = "Jones", JobTitle = "Asst. Manager" },
    //         new PersonModel() { PersonName = "William", JobTitle = "Clerk" },
    //         }
    //     };
    //     // ViewData["Grid"] = model;
    //     return View("Sample", model); //invoked a partial view Views/Shared/Components/Grid/Default.cshtml
    // }
    public async Task<IViewComponentResult> InvokeAsync(PersonGridModel grid)
    {
        return View("Sample", grid); //invokes a partial view Views/Shared/Components/Grid/Sample.cshtml
    }
}