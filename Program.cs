using MyFirstDotNetCoreApp.CustomMiddleware;

// using MyFirstDotNetCoreApp.PastCodes;

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
        builder.Services.AddTransient<MyCustomMiddleware>();
        var app = builder.Build();
        // enable routing
        app.UseRouting();
        // creating end points
        app.UseEndpoints(
            endpoints =>
            {
                // add endpoints
            });
        app.Run(async context => { await context.Response.WriteAsync("No response"); });
        app.Run();
    }
}