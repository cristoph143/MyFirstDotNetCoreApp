namespace MyFirstDotNetCoreApp.PastCodes;

public abstract class Countries
{
    //data
    private static readonly Dictionary<int, string> CountryData = new()
    {
        { 1, "United States" },
        { 2, "Canada" },
        { 3, "United Kingdom" },
        { 4, "India" },
        { 5, "Japan" }
    };

    public static void CountriesEndpoints(WebApplication app)
    {
        //endpoints
        app.UseEndpoints(endpoints =>
        {
            //when request path is "/countries"
            endpoints.MapGet("/countries", async context =>
            {
                foreach (var country in CountryData)
                    //write country details to response
                    await context.Response.WriteAsync($"{country.Key}, {country.Value}\n");
            });

            //When request path is "countries/{countryID}"
            endpoints.MapGet("/countries/{countryID:int:range(1,100)}", async context =>
            {
                //check if "countryID" was not submitted in the request
                bool isContainKey = context.Request.RouteValues.ContainsKey("countryID") ;
                if (!isContainKey)
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("The CountryID should be between 1 and 100");
                    return;
                }
                //read countryID from RouteValues (route parameters)
                int  countryID = Convert.ToInt32(context.Request.RouteValues["countryID"]);

                //if the countryID exists in the countries dictionary
                if (!CountryData.TryGetValue(countryID, out var country))
                {   // if countryID not exists in countries dictionary
                    context.Response.StatusCode = 404;
                    await context.Response.WriteAsync($"[No country]");
                    return;
                }
                //write country name to response
                await context.Response.WriteAsync($"{country}");
            });

            //When request path is "countries/{countryID}"
            endpoints.MapGet("/countries/{countryID:min(101)}", async context =>
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("The CountryID should be between 1 and 100 - min");
            });
        });
    }
}