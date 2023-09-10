internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.MapGet("/", () => "Hello World!");
        app.Run(async context =>
        {
            context.Response.StatusCode = 400;
            context.Response.StatusCode = 1 == 1 ? 200 : 400;
            await context.Response.WriteAsync("Hello World1!");
            await context.Response.WriteAsync("Hello World2!");
        });
        app.Run();
    }
}