namespace MyFirstDotNetCoreApp.CustomMiddleware;

// You may need to install the
// Microsoft.AspNetCore.Http.Abstractions package
// into your project
public class HelloCustomMiddleware
{
    private readonly RequestDelegate _next;

    public HelloCustomMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        string? firstName = context.Request.Query["firstName"];
        string? lastName = context.Request.Query["lastName"];

        // checking if a valid non-empty string value exists
        var isFirstName = !string.IsNullOrEmpty(firstName);
        var isLastName = !string.IsNullOrEmpty(lastName);
        var isContains = isFirstName && isLastName;
        var fullName = isContains ? $"{firstName} {lastName}" : null;

        await context.Response.WriteAsync(fullName != null ? "Hello World " + fullName : "");
        await _next(context);
    }
}

// Extension method used to add the middleware
//       to the HTTP request pipeline
public static class HelloCustomMiddleExtensions
{
    public static IApplicationBuilder UseHelloCustomMiddle(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<HelloCustomMiddleware>();
    }
}