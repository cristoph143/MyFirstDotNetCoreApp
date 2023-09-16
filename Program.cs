using MyFirstDotNetCoreApp.CustomMiddleware;

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

        app.MapGet("/hello", () => "Hello World 1!\n");
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello World 2!\n");
            await next(context);
        });

        //middleware 1
        app.Use(async (context, next) => {
            await context.Response.WriteAsync("From Middleware 1\n");
            await next(context);
        });

        //middleware 2
        //app.UseMiddleware<MyCustomMiddleware>();
        // app.UseMyCustomMiddleware();
        app.UseHelloCustomMiddle();

        //middleware 3
        app.Run(async context => {
            await context.Response.WriteAsync("From Middleware 3\n");
        });

        app.Run(async context =>
        {
            // await PastCode(context);
            await context.Response.WriteAsync("Hello World Again!\n");
        });
        app.Run();
    }

    // private static async Task PastCode(HttpContext context)
    // {
    //     // await queryString(context);
    //     var reader = new StreamReader(context.Request.Body);
    //     var body = await reader.ReadToEndAsync();
    //     string path = context.Request.Path;
    //     var method = context.Request.Method;
    //     await Calculator.HttpCalculator(method, path, context);
    //     await QueryProcessor.ProcessQuery(context, body, path, method);
    // }
}