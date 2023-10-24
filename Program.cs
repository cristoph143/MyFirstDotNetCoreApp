using Autofac;
using Autofac.Extensions.DependencyInjection;
using MyFirstDotNetCoreApp.Models;
using ServiceContracts;
using Services;
using StocksApp.Services;

namespace MyFirstDotNetCoreApp;

internal abstract class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Services.AddControllersWithViews();
        builder.Services.Configure<WeatherApiOptions>(builder.Configuration.GetSection("weatherApi"));
        builder.Configuration.AddJsonFile("MyOwnConfig.json", true, true);
        builder.Services.AddScoped<ICitiesService, CitiesService>();
        builder.Services.AddScoped<IStocksService, StocksService>(); // Fixed the error here
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope(); //AddScoped
        });
//add services into IoC container
        builder.Services.AddSingleton<ICountriesService, CountriesService>();
        builder.Services.AddSingleton<IPersonService, PersonService>();
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<FinnhubService>();
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            var configuration = builder.Configuration;
            var finnhubToken = configuration["FinnhubToken"];
            containerBuilder.RegisterType<FinnhubService>()
                .WithParameter(new TypedParameter(typeof(string), finnhubToken))
                .As<IFinnhubService>();
        });

        builder.Services.Configure<TradingOptions>(
            builder.Configuration.GetSection(nameof(TradingOptions))); //add IOptions<TradingOptions> as a service
        var app = builder.Build();
        if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
        app.UseStaticFiles();
        app.UseRouting();
        app.MapGet("/app-setting", async context =>
        {
            await context.Response.WriteAsync(app.Configuration["mykEY"] + "\n");
            await context.Response.WriteAsync(app.Configuration.GetValue<string>("MyKey") + "\n");
            await context.Response.WriteAsync(app.Configuration.GetValue("x", 10) + "\n");
        });
        app.MapControllers();

        app.Run();
    }
}