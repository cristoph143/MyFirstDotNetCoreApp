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
        app.Use(async (context, next) =>
        {
            Microsoft.AspNetCore.Http.Endpoint? endPoint = context.GetEndpoint();
            if (endPoint != null)
            {
                await context.Response.WriteAsync($"Endpoint: {endPoint.DisplayName}\n");
            }
            await next(context);
        });
        // enable routing
        app.UseRouting();
        app.Use(async (context, next) =>
        {
            Endpoint? endPoint = context.GetEndpoint();
            if (endPoint != null)
            {
                await context.Response.WriteAsync($"Endpoint: {endPoint.DisplayName}\n");
            }
            await  next(context);
        });

        // creating end points
        app.UseEndpoints( endpoints =>
        {
            // add endpoints
            endpoints.Map("map1", async (context) => await context.Response.WriteAsync("Hello World! Map 1"));
            endpoints.Map("map2", async (context) => await context.Response.WriteAsync("Hello World! Map 2"));
            endpoints.MapPost("map1Post", async (context) => await context.Response.WriteAsync("Hello World! Map 1"));
            endpoints.MapPost("map2Post", async (context) => await context.Response.WriteAsync("Hello World! Map 2"));
        });
        app.Run(async context => { await context.Response.WriteAsync($"Request received at {context.Request.Path}"); });
        app.Run();
    }
}