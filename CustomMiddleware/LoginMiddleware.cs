using System.Text;

namespace MyFirstDotNetCoreApp.CustomMiddleware;

public class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if (!(context.Request.Path == "/" && context.Request.Method == "POST"))
        {
            await _next(context);
            return;
        }

        // Read request body as stream
        var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        var email = GetQueryValue(body, "email");
        var password = GetQueryValue(body, "password");

        var errorMessage = BuildErrorMessage(email, password);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(errorMessage);
            return;
        }

        if (await IsRequired(context, email, password)) return;
        await IsValidLogin(context, email, password);
    }

    private static async Task<bool> IsRequired(HttpContext context, string email, string password)
    {
        var isEmail = string.IsNullOrEmpty(email);
        var isPassword = string.IsNullOrEmpty(password);

        if (!isEmail || !isPassword) return false;
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Both 'email' and 'password' are required\n");
        return true;
    }

    private static async Task IsValidLogin(HttpContext context, string email, string password)
    {
        // Valid email and password as per the requirement specification
        const string validEmail = "admin@example.com";
        const string validPassword = "admin1234";
        var isEmailEqual = email == validEmail;
        var isPasswordEqual = password == validPassword;
        var isValidLogin = isEmailEqual && isPasswordEqual;

        if (isValidLogin)
        {
            await context.Response.WriteAsync("Successful login\n");
        }
        else
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("Invalid login\n");
        }
    }

    private static string? GetQueryValue(string body, string key)
    {
        var queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
        return !queryDict.TryGetValue(key, out var value) ? null : Convert.ToString(value[0]);
    }

    private static string BuildErrorMessage(string email, string password)
    {
        var sb = new StringBuilder();
        if (string.IsNullOrEmpty(email)) sb.Append("Invalid input for 'email'\n");
        if (string.IsNullOrEmpty(password)) sb.Append("Invalid input for 'password'\n");
        return sb.ToString();
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class LoginMiddlewareExtensions
{
    public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoginMiddleware>();
    }
}