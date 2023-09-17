// using MyFirstDotNetCoreApp.CustomConstraints;
// using MyFirstDotNetCoreApp.CustomMiddleware;
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
        // builder.Services.AddTransient<MyCustomMiddleware>();
        // //register custom service
        // builder.Services.AddRouting(options =>
        // {
        //     options.ConstraintMap.Add("months", typeof(MonthsCustomConstraints));
        // });
        var app = builder.Build();
        // Create an instance of the Past class
        // var past = new Past();
        // past.UseWhen(app);

        // enable routing
        app.UseStaticFiles();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.Map("/", async context =>
            {
                await context.Response.WriteAsync("Hello World");
            });
        });
        app.Run(async context => { await context.Response.WriteAsync($"Request received at {context.Request.Path}"); });
        app.Run();
    }
}