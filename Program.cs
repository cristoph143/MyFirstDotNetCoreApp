namespace MyFirstDotNetCoreApp;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/hello", () => "Hello World!");
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello World!");
            await next(context);
        });
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello World 1!");
            await next(context);
        });
        app.Run(async context =>
        {
            // await PastCode(context);
            await context.Response.WriteAsync("Hello World Again!");
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