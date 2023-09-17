using MyFirstDotNetCoreApp.CustomMiddleware;
using MyFirstDotNetCoreApp.PastCodes;

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
        // Create an instance of the Past class
        // var past = new Past();
        // past.UseWhen(app);

        // enable routing
        app.UseRouting();

        // creating endpoints
        app.UseEndpoints(endpoints =>
        {
            endpoints.Map("files/{filename}.{extension}", async context =>
            {
                string? filename = context.Request.RouteValues["filename"] as string;
                string? extension = context.Request.RouteValues["extension"] as string;

                await context.Response.WriteAsync($"Request received at {context.Request.Path} - {filename} - {extension}");
            });
            endpoints.Map("employee/profile/{employeeName}", async context =>            {
                string? employeeName = context.Request.RouteValues["EmployeeName"] as string;

                await context.Response.WriteAsync($"Request received at {context.Request.Path} - {employeeName}");
            });
        });
        app.Run(async context => { await context.Response.WriteAsync($"Request received at {context.Request.Path}"); });
        app.Run();
    }

    
}