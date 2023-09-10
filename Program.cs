using Microsoft.Extensions.Primitives;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");
        app.Run(async context =>
        {
            // await queryString(context);
            StreamReader reader = new StreamReader(context.Request.Body);
            string body = await reader.ReadToEndAsync();
            Dictionary<string, StringValues> queryDict =
                Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
            if (queryDict.ContainsKey("firstName"))
            {
                string firstName = queryDict["firstName"][0];
                await context.Response.WriteAsync($"<p>Hello World1! {body} ? {queryDict} {firstName}</p>");
            }
            await context.Response.WriteAsync("Hello World1!");
        });
        app.Run();
    }

    private static async Task queryString(HttpContext context)
    {
        string path = context.Request.Path;
        string method = context.Request.Method;
        await context.Response.WriteAsync("Hello World1!");
        await context.Response.WriteAsync("<h1>Hello World1!</h1>");
        await context.Response.WriteAsync($"<p>Hello World1! {path} ? {method}</p>");
        await context.Response.WriteAsync("Hello World2!");
        switch (method)
        {
            case "GET":
            {
                var containsAgent = context.Request.Headers.ContainsKey("User-Agent");
                
                if (containsAgent)
                {
                    var userAgent = context.Request.Headers["User-Agent"];
                    await context.Response.WriteAsync($"<p>Hello World UserAgent! {userAgent}</p>");
                }
                var containsKeys = context.Request.Query.ContainsKey("id");
                if (containsKeys)
                {
                    var id = context.Request.Query["id"].ToString();
                    await context.Response.WriteAsync($"<p>Hello World ID! {id}</p>");
                }
                var containsAuthorizationKey = context.Request.Headers.ContainsKey("AuthorizationKey");
                if (containsAuthorizationKey)
                {
                    var auth = context.Request.Headers["AuthorizationKey"].ToString();
                    await context.Response.WriteAsync($"<p>Hello World containsAuthorizationKey! {auth}</p>");
                }

                break;
            }
        }
    }
}