using MyFirstDotNetCoreApp.CustomMiddleware;

namespace MyFirstDotNetCoreApp.PastCodes;

public class Past
{
    public void UseWhen(WebApplication app)
    {
        MiddleWare(app);
        app.UseWhen(context => context.Request.Query.ContainsKey("custom"),
            apps =>
            {
                apps.Use(async (context, next) =>
                {
                    await context.Response.WriteAsync("Hello from Middleware Branch");
                    await next(context);
                });
            });
        app.Run(async context => { await context.Response.WriteAsync("Hello from middleware at main chain\n"); });
    }

    private static void MiddleWare(WebApplication app)
    {
        app.MapGet("/hello", () => "Hello World 1!\n");
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello World 2!\n");
            await next(context);
        });

        //middleware 1
        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("From Middleware 1\n");
            await next(context);
        });

        //middleware 2
        //app.UseMiddleware<MyCustomMiddleware>();
        // app.UseMyCustomMiddleware();
        app.UseHelloCustomMiddle();

        //middleware 3
        app.Run(async context => { await context.Response.WriteAsync("From Middleware 3\n"); });

        app.Run(async context =>
        {
            //TODO: test useWhen for this calculator
            // string? firstNumber = context.Request.Query["firstNumber"];
            // string? secondNumber = context.Request.Query["secondNumber"];
            //
            // // checking if a valid non-empty string value exists
            // bool isFirstNumber = !string.IsNullOrEmpty(firstNumber);
            // bool isSecondNumber = !string.IsNullOrEmpty(secondNumber);
            // bool isContains = isFirstNumber && isSecondNumber;
            // app.UseWhen(context => context.Request.Query.ContainsKey("custom"),
            //     apps =>
            //     {
            //         apps.Use(async (context, next) =>
            //         {
            await PastCode(context);
            //         await next(context);
            //     });
            // });
            await context.Response.WriteAsync("Hello World Again!\n");
        });
    }

    private static async Task PastCode(HttpContext context)
    {
        // await queryString(context);
        var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        string path = context.Request.Path;
        var method = context.Request.Method;
        await Calculator.HttpCalculator(method, path, context);
        await QueryProcessor.ProcessQuery(context, body, path, method);
    }
}