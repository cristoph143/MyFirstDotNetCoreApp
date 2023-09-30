using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.Controllers;

public class OrdersController : Controller
{
    [Route("/order")]

    public IActionResult Index(
        [Bind(nameof(Order.OrderDate), nameof(Order.InvoicePrice), nameof(Order.Products))]
        Order order)
    {
        //if there are any validation errors (as per data annotations)
        if (!ModelState.IsValid)
        {
            var messages = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return BadRequest(messages);
        }

        var random = new Random();
        var randomOrderNumber = random.Next(1, 99999);

        return Json(new { orderNumber = randomOrderNumber });
    }
}