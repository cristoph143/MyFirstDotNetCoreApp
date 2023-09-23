using Microsoft.AspNetCore.Mvc;

namespace MyFirstDotNetCoreApp.Controllers;

public  class HomeController : Controller
{
       [Route("sayHello")]
      public string method1()
      {
          return "return from method1";
      }
}