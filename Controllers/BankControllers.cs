using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using MyFirstDotNetCoreApp.Models;

namespace MyFirstDotNetCoreApp.Controllers;

public class BankControllers : Controller
{
    private static readonly List<BankAccount> BankAccounts = new()
    {
        new BankAccount
        {
            AccountNumber = 1001,
            AccountHolderName = "Example Name 1",
            CurrentBalance = 5000
        },
        new BankAccount
        {
            AccountNumber = 1002,
            AccountHolderName = "Example Name 2",
            CurrentBalance = 7000
        },
        new BankAccount
        {
            AccountNumber = 1003,
            AccountHolderName = "Example Name 3",
            CurrentBalance = 3000
        },
        new BankAccount
        {
            AccountNumber = 1004,
            AccountHolderName = "Example Name 4",
            CurrentBalance = 9000
        },
        new BankAccount
        {
            AccountNumber = 1005,
            AccountHolderName = "Example Name 5",
            CurrentBalance = 2000
        }
    };

    private static readonly Dictionary<string, string> Messages = new()
    {
        { "NotFound", "No bank accounts found" },
        { "BadRequest", "Account Number should be supplied" }
    };

    //When request is received at path "/"
    [Route("/")]
    public IActionResult Index() => Content("Welcome to the Best Bank");

    [Route("/account-details")]
    [Route("/account-details/{accountNumber?}")]
    public IActionResult AccountDetails(int? accountNumber)
    {
        if (accountNumber.HasValue)
        {
            var bankAccount = BankAccounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
            return bankAccount != null ? Json(bankAccount) : NotFound(Messages["NotFound"]);
        }

        var firstBankAccount = BankAccounts.FirstOrDefault();
        return firstBankAccount != null ? Json(firstBankAccount) : NotFound(Messages["NotFound"]);
    }

    //When request is received at path "/account-statement"
    [Route("/account-statement")]
    public IActionResult AccountStatement() =>
        //send a pdf file (at wwwroot folder) as response
        File("~/statement.pdf", "application/pdf");

    [Route("/get-current-balance/{accountNumber:int?}")]
    public IActionResult GetCurrentBalance()
    {
        // Get the 'accountNumber' value from the route parameters using RouteData
        if (!HttpContext.Request.RouteValues.TryGetValue("accountNumber", out var accountNumberObj) ||
            accountNumberObj is not string accountNumber) return NotFound(Messages["BadRequest"]);
        // Check if the 'accountNumber' parameter is provided
        if (string.IsNullOrEmpty(accountNumber)) return NotFound(Messages["BadRequest"]);

        // Convert the 'accountNumber' to an integer
        if (!int.TryParse(accountNumber, out var accountNumberInt))
            return BadRequest("Invalid Account Number format");
        var bankAccount = BankAccounts.FirstOrDefault(a => a.AccountNumber == accountNumberInt);

        if (bankAccount != null)
            return Content(bankAccount.CurrentBalance.ToString(CultureInfo.InvariantCulture));
            // Redirect to the AccountDetails action with the accountNumber parameter
            // return RedirectToAction("AccountDetails", new { accountNumber = accountNumberInt });
        {
            var validAccountNumbers = string.Join(", ", BankAccounts.Select(a => a.AccountNumber));
            return BadRequest($"Account Number should be one of the following: {validAccountNumbers}");
        }
    }
}