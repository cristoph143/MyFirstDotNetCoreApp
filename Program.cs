using MyFirstDotNetCoreApp.CustomConstraints;
using MyFirstDotNetCoreApp.CustomMiddleware;

// using MyFirstDotNetCoreApp.PastCodes;

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
        //register custom service
        builder.Services.AddRouting(options =>
        {
            options.ConstraintMap.Add("months", typeof(MonthsCustomConstraints));
        });
        var app = builder.Build();
        // Create an instance of the Past class
        // var past = new Past();
        // past.UseWhen(app);

        // enable routing
        app.UseRouting();



        // creating endpoints
        app.UseEndpoints(endpoints =>
        {
            //Eg: files/sample.txt
            endpoints.Map("files/{filename}.{extension}", async context =>
            {
                var filename = context.Request.RouteValues["filename"] as string;
                var extension = context.Request.RouteValues["extension"] as string;

                await context.Response.WriteAsync(
                    $"Request received at {context.Request.Path} - {filename} - {extension}");
            });
            //Eg: employee/profile/john
            endpoints.Map("employee/profile/{EmployeeName:length(4,7):alpha=harsha}", async context =>
            {
                var employeeName = context.Request.RouteValues["EmployeeName"] as string;

                await context.Response.WriteAsync($"Request received at {context.Request.Path} - {employeeName}");
            });
            // products/details/1
            endpoints.Map("products/details/{id:int:range(1,1000)?}", async context =>
            {
                var isId = context.Request.RouteValues.ContainsKey("id");
                if (!isId)
                {
                    await context.Response.WriteAsync($"Request received at {context.Request.Path} - No ID");
                    return;
                }

                var id = Convert.ToInt32(context.Request.RouteValues["id"]);
                await context.Response.WriteAsync($"Request received at {context.Request.Path} - ID: {id}");
            });
            //daily-digest-report/{reportDate}
            endpoints.Map("daily-digest-report/{reportdate:datetime}", async context =>
            {
                var reportDate = Convert.ToDateTime(context.Request.RouteValues["reportDate"]);
                await context.Response.WriteAsync($"In daily-digest-report - {reportDate.ToShortDateString()}");
            });
            //Eg: cities/{cityId}
            endpoints.Map("cities/{cityId:guid}", async context =>
            {
                var cityId = Guid.Parse(Convert.ToString(context.Request.RouteValues["cityId"])!);
                await context.Response.WriteAsync($"City information - {cityId}");
            });
            //sales-report/2030/apr
            endpoints.Map("/sales-report/{year:int:min(1900)}/{month:months}", async context =>
            {
                var year = Convert.ToInt32(context.Request.RouteValues["year"]);
                var month = Convert.ToString(context.Request.RouteValues["month"]);

                if (month != "apr" && month != "jul" && month != "oct" && month != "jan")
                {
                    await context.Response.WriteAsync($"{month} is not allowed for sales report");
                    return;
                }

                await context.Response.WriteAsync($"sales report - {year} - {month}");
            });
        });
        app.Run(async context => { await context.Response.WriteAsync($"Request received at {context.Request.Path}"); });
        app.Run();
    }
}