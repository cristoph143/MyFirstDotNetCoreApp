// using MyFirstDotNetCoreApp.CustomConstraints;
// using MyFirstDotNetCoreApp.CustomMiddleware;

using MyFirstDotNetCoreApp.PastCodes;

// using Microsoft.Extensions.FileProviders;
namespace MyFirstDotNetCoreApp;

internal abstract class Program
{
    /*
    // Main function: Entry point of the application.
    // It sets up a web application, defines a request handling logic,
    // and starts the application.
    */
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.UseRouting();
        Countries.CountriesEndpoints(app);
        app.Run(async context => { await context.Response.WriteAsync($"Request received at {context.Request.Path}"); });
        app.Run();
    }
}