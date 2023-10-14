using Autofac;
using Autofac.Extensions.DependencyInjection;
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
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
        builder.Services.AddControllersWithViews();
        // builder.Services.Add(new ServiceDescriptor(
        //     typeof(ICitiesService),
        //     typeof(CitiesService),
        //     // ServiceLifetime.Transient
        //     // ServiceLifetime.Singleton
        //     ServiceLifetime.Scoped
        // ));
        //builder.Services.AddTransient<ICitiesService, CitiesService>();
        builder.Services.AddScoped<ICitiesService, CitiesService>();
        //builder.Services.AddSingleton<ICitiesService, CitiesService>();        var app = builder.Build();
        builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
        {
            //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerDependency(); //AddTransient
            containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope(); //AddScoped
            //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().SingleInstance(); //AddSingleton
        });

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        app.UseStaticFiles();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.Map("/app-setting", async context =>
            {
                await context.Response.WriteAsync(app.Configuration["mykEY"] + "\n");
                await context.Response.WriteAsync(app.Configuration.GetValue<string>("MyKey") + "\n");
                await context.Response.WriteAsync(app.Configuration.GetValue<int>("x", 10) + "\n");
            });
        });
        app.MapControllers();

        app.Run();
    }
}