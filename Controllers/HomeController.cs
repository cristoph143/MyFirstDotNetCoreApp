using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        private readonly string _pathFile;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _pathFile = Path.Combine(webHostEnvironment.WebRootPath, "Sample.pdf");
        }

        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
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
            return Json(person);
        }

        [Route("contact-us/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact()
        {
            return "Hello from Contact";
        }

        [Route("file-download")]
        public VirtualFileResult FileDownload()
        {
            return File("/sample.pdf", "application/pdf");
        }

        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {
            return PhysicalFile(_pathFile, "application/pdf");
        }

        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(_pathFile);
            return File(bytes, "application/pdf");
        }
    }
}