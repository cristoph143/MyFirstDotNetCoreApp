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

        [Route("book")]
        public IActionResult Book()
        {
            //Book id should be applied
            if (!Request.Query.ContainsKey("bookId"))
            {
                return BadRequest("Book id is not supplied");
            }

            //Book id can't be empty
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookId"])))
            {
                return BadRequest("Book id can't be null or empty");
            }

            //Book id should be between 1 to 1000
            int bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookId"]);
            switch (bookId)
            {
                case <= 0:
                    return BadRequest("Book id can't be less than or equal to zero");
                case > 1000:
                    return NotFound("Book id can't be greater than 1000");
            }

            //isLoggedIn should be true
            if (Convert.ToBoolean(Request.Query["isLoggedIn"])) 
            {
                // return File("/sample.pdf", "application/pdf");
                //return new RedirectToActionResult("Books", "Store", new { }); //302 - Found
                return new RedirectToActionResult("Books", "Store", new { }, permanent: true); //301 - Moved Permanently
            }
            // Response.StatusCode = 401;
            // return Content("User must be authenticated");
            return Unauthorized("User must be authenticated");


        }
    }
}