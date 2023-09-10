using MyFirstDotNetCoreApp.CustomMiddleware;

namespace MyFirstDotNetCoreApp;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddTransient<MyCustomMiddleware>();
        var app = builder.Build();

        app.MapGet("/hello", () => "Hello World!\n");
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello World!\n");
            await next(context);
        });
        app.UseMiddleware<MyCustomMiddleware>();
        app.UseMiddleware<MyCustomMiddleware>();

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
    //     // await Calculator.HttpCalculator(method, path, context);
    //     await QueryProcessor.ProcessQuery(context, body, path, method);
    // }
}