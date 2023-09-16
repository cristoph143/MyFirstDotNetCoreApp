using MyFirstDotNetCoreApp.CustomMiddleware;
using MyFirstDotNetCoreApp.PastCodes; 
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
        // Create an instance of the Past class
        var past = new Past();
        past.UseWhen(app);
        app.Run();
    }

    
}