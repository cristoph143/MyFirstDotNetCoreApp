// using Microsoft.Extensions.Primitives;

namespace MyFirstDotNetCoreApp;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/hello", () => "Hello World!");
        app.Run(async context =>
        {
            // await queryString(context);
            // var reader = new StreamReader(context.Request.Body);
            // var body = await reader.ReadToEndAsync();
            string path = context.Request.Path;
            var method = context.Request.Method;
            await HttpCalculator(method, path, context);
            // Dictionary<string, StringValues> queryDict =
            //     Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
            // await queryDict(queryDict, context, body);
            // await context.Response.WriteAsync("Hello World1!");
            // await queryString(context, path, method);
        });
        app.Run();
    }

    private static async Task HttpCalculator(string method, string path, HttpContext context)
    {
        if (method == "GET" && path == "/")
        {
            var isFirst = await IsContainKeys(context, "firstNumber");
            var isSecond = await IsContainKeys(context, "secondNumber");
            var isOps = await IsContainKeys(context, "operation");

            if (!isFirst && !isSecond && !isOps) return;
            await CalculateResult(context);
        }
    }

    private static async Task CalculateResult(HttpContext context)
    {
        var firstNumber = await ConvertToInt(context, "firstNumber");
        var secondNumber = await ConvertToInt(context, "secondNumber");
        var operation = context.Request.Query["operation"].FirstOrDefault();       
        var result = await GetCalculatedResult(context, operation, firstNumber, secondNumber);
        await context.Response.WriteAsync(result?.ToString() ?? string.Empty);
        await context.Response.CompleteAsync(); // Send the response back to the client
    }

    private static async Task<bool> IsContainKeys(HttpContext context, string keys)
    {
        if (context.Request.Query.ContainsKey(keys)) return true;
        if (context.Response.HasStarted) return false;
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync($"Invalid input for {keys}\n");
        await context.Response.CompleteAsync();
        return false;
    }

    private static async Task<int> ConvertToInt(HttpContext context, string keys)
    {
        var numberString = context.Request.Query[keys].FirstOrDefault();        
        if (!string.IsNullOrEmpty(numberString)) return Convert.ToInt32(numberString);
        await HandleInvalidOperation(context, keys);
        return 0;
    }

    private static async Task<long?> GetCalculatedResult(HttpContext context, string? operation, int firstNumber, int secondNumber)
    {
        return operation switch
        {
            "add" => firstNumber + secondNumber,
            "subtract" => firstNumber - secondNumber,
            "multiply" => firstNumber * secondNumber,
            "divide" => secondNumber != 0 ? firstNumber / secondNumber : 0,
            "mod" => secondNumber != 0 ? firstNumber % secondNumber : 0,
            _ => await HandleInvalidOperation(context, "operation")
        };
    }

    private static async Task<long?> HandleInvalidOperation(HttpContext context, string keys)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync($"Invalid input for {keys}\n");
        await context.Response.CompleteAsync();
        return null;
    }

    // private static async Task queryDict(Dictionary<string, StringValues> queryDict, HttpContext context, string body)
    // {
    //     if (queryDict.ContainsKey("firstName"))
    //     {
    //         var firstName = queryDict["firstName"][0];
    //         await context.Response.WriteAsync($"<p>Hello World1! {body} ? {queryDict} {firstName}</p>");
    //     }
    // }
    //
    // private static async Task queryString(HttpContext context, string path, string method)
    // {
    //     await context.Response.WriteAsync("Hello World1!");
    //     await context.Response.WriteAsync("<h1>Hello World1!</h1>");
    //     await context.Response.WriteAsync($"<p>Hello World1! {path} ? {method}</p>");
    //     await context.Response.WriteAsync("Hello World2!");
    //     switch (method)
    //     {
    //         case "GET":
    //         {
    //             var containsAgent = context.Request.Headers.ContainsKey("User-Agent");
    //
    //             if (containsAgent)
    //             {
    //                 var userAgent = context.Request.Headers["User-Agent"];
    //                 await context.Response.WriteAsync($"<p>Hello World UserAgent! {userAgent}</p>");
    //             }
    //
    //             var containsKeys = context.Request.Query.ContainsKey("id");
    //             if (containsKeys)
    //             {
    //                 var id = context.Request.Query["id"].ToString();
    //                 await context.Response.WriteAsync($"<p>Hello World ID! {id}</p>");
    //             }
    //
    //             var containsAuthorizationKey = context.Request.Headers.ContainsKey("AuthorizationKey");
    //             if (containsAuthorizationKey)
    //             {
    //                 var auth = context.Request.Headers["AuthorizationKey"].ToString();
    //                 await context.Response.WriteAsync($"<p>Hello World containsAuthorizationKey! {auth}</p>");
    //             }
    //
    //             break;
    //         }
    //     }
    // }
}