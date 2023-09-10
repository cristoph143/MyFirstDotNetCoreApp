internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");
        app.Run(async context =>
        {
            context.Response.Headers["MyKey"] = "myValue";
            context.Response.Headers.Add("MyKey2", "myValue2");
            context.Response.Headers["Content-Type"] = "text/html";
            context.Response.StatusCode = 400;
            context.Response.StatusCode = 1 == 1 ? 200 : 400;
            await context.Response.WriteAsync("Hello World1!");
            await context.Response.WriteAsync("<h1>Hello World1!</h1>");
            await context.Response.WriteAsync("Hello World2!");
        });
        app.Run();
    }
}