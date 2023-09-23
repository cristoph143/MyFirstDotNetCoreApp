using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.Controllers;
 [Controller]
public  class HomeController : Controller
{
    [Route("home")]
    [Route("/")]
    public ContentResult Index()
    {
        //return new ContentResult() { Content = "Hello from Index", ContentType = "text/plain" };

        // return Content("Hello from Index", "text/plain");

        return Content("<h1>Welcome</h1> <h2>Hello from Index</h2>", "text/html");
    }

    [Route("about")]
    public string About()
    {
        return "Hello from About";
    }

    [Route("person")]
    public JsonResult Person()
    {
        Person person = new Person() { Id = Guid.NewGuid(), FirstName = "James", LastName = "Smith", Age = 25 };
        //return new JsonResult(person);
        return Json(person);
        //return "{ \"key\": \"value\" }";
    }

    [Route("contact-us/{mobile:regex(^\\d{{10}}$)}")]
    public string Contact()
    {
        return "Hello from Contact";
    }
}