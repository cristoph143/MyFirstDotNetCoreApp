using ServiceContracts;
using Services;


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
        builder.Services.AddControllersWithViews();
        builder.Services.Add(new ServiceDescriptor(
            typeof(ICitiesService),
            typeof(CitiesService),
            // ServiceLifetime.Transient
            ServiceLifetime.Singleton
        ));
        var app = builder.Build();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapControllers();

        app.Run();
    }
}