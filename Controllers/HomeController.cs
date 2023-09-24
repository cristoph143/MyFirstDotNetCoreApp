using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        private readonly string _pathFile;

        public HomeController(IWebHostEnvironment webHostEnvironment) => _pathFile = Path.Combine(webHostEnvironment.WebRootPath, "Sample.pdf");

        [Route("home")]
        [Route("/")]
        public ContentResult Index() => Content("<h1>Welcome</h1> <h2>Hello from Index</h2>", "text/html");

        [Route("about")]
        public string About() => "Hello from About";

        [Route("register")]
        //[Bind(nameof(Person.PersonName), nameof(Person.Email), nameof(Person.Password), nameof(Person.ConfirmPassword))]
        public IActionResult Index(Person person)
        {
            if (ModelState.IsValid) return Content($"{person}");
            string errors = string.Join("\n", ModelState.Values.SelectMany(value => value.Errors).Select(err => err.ErrorMessage));
            return BadRequest(errors);
        }

        [Route("contact-us/{mobile:regex(^\\d{{10}}$)}")]
        public string Contact() => "Hello from Contact";

        [Route("file-download")]
        public VirtualFileResult FileDownload() => File("/sample.pdf", "application/pdf");

        [Route("file-download2")]
        public PhysicalFileResult FileDownload2() => PhysicalFile(_pathFile, "application/pdf");

        [Route("file-download3")]
        public FileContentResult FileDownload3()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(_pathFile);
            return File(bytes, "application/pdf");
        }

        [Route("bookstore/{bookId?}/{isLoggedIn?}")]
        public IActionResult Book([DisallowNull][FromRoute] short? bookId,  [FromRoute]string isLoggedIn, Book book)
        {
            IsNull(bookId);

            //Book id should be applied
            if (!Request.Query.ContainsKey("bookId")) return BadRequest("Book id is not supplied");
            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookId"])))
                return BadRequest("Book id can't be null or empty");
            bookId = Convert.ToInt16(ControllerContext.HttpContext.Request.Query["bookId"]);
            return bookId switch
            {
                <= 0 => BadRequest("Book id can't be less than or equal to zero"),
                > 1000 => NotFound("Book id can't be greater than 1000"),
                //isLoggedIn should be true
                _ => Convert.ToBoolean(Request.Query["isLoggedIn"])
                    ?
                    Content($"Book id: {bookId}, Book: {book}", "text/plain")
                    // new RedirectResult($"/store/books/{bookId}", true)
                    : Unauthorized("User must be authenticated")
            };
        }

        private static void IsNull(short? bookId)
        {
            switch (bookId)
            {
                case null:
                    throw new ArgumentNullException(nameof(bookId));
                case <= 0:
                    throw new ArgumentOutOfRangeException(nameof(bookId));
            }
        }
    }
}