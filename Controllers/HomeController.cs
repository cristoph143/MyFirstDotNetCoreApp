using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyFirstDotNetCoreApp.Controllers
{
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
        [Route("/")]
        public IActionResult Index()
        {
        return View();
        }

        [Route("about-company")]
        public IActionResult About()
        {
        return View();
        }

        [Route("contact-support")]
        public IActionResult Contact()
        {
        return View();
        }
    }
}