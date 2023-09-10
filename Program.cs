internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");
        app.Run(async context =>
        {
            string path = context.Request.Path;
            string method = context.Request.Method;
            await queryString(method, context);
            await context.Response.WriteAsync("Hello World1!");
            await context.Response.WriteAsync("<h1>Hello World1!</h1>");
            await context.Response.WriteAsync($"<p>Hello World1! {path} ? {method}</p>");
            await context.Response.WriteAsync("Hello World2!");
        });
        app.Run();
    }

    private static async Task queryString(string method, HttpContext context)
    {
        switch (method)
        {
            case "GET":
            {
                var containsKeys = context.Request.Query.ContainsKey("id");
                var containsAgent = context.Request.Headers.ContainsKey("User-Agent");
                if (containsAgent)
                {
                    var userAgent = context.Request.Headers["User-Agent"];
                    await context.Response.WriteAsync($"<p>Hello World UserAgent! {userAgent}</p>");
                }

                if (containsKeys)
                {
                    var id = context.Request.Query["id"].ToString();
                    await context.Response.WriteAsync($"<p>Hello World ID! {id}</p>");
                }

                break;
            }
        }
    }
}