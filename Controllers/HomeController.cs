using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MyFirstDotNetCoreApp.Controllers;

[Route("[controller]")]
public class HomeController(
    IOptions<SocialMediaLinksOptions> socialMediaLinksOptions
    ) : Controller
{
    private readonly SocialMediaLinksOptions _socialMediaLinksOptions = socialMediaLinksOptions.Value;

    [Route("/")]
    public IActionResult Index()
    {
        ViewBag.SocialMediaLinks = _socialMediaLinksOptions;
        return View();
    }
}