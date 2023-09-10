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
            await Calculator.HttpCalculator(method, path, context);

            // await HttpCalculator(method, path, context);
            // Dictionary<string, StringValues> queryDict =
            //     Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
            // await queryDict(queryDict, context, body);
            // await context.Response.WriteAsync("Hello World1!");
            // await queryString(context, path, method);
        });
        app.Run();
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