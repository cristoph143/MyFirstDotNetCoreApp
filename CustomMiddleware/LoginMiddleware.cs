using System.Text;

namespace MyFirstDotNetCoreApp.CustomMiddleware;

public class LoginMiddleware
{
    private readonly RequestDelegate _next;

    public LoginMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    // Middleware entry point that handles the login functionality.
    public async Task Invoke(HttpContext context)
    {
        // If the request path is "/" and the method is "POST", it reads the request body,
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

        // validates the email and password, and performs the login logic.
        var errorMessage = BuildErrorMessage(email, password);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync(errorMessage);
            return;
        }
        // If any validation errors occur, it returns the appropriate error response.
        if (await IsRequired(context, email, password)) return;
        // If the login is successful, it returns a success response.
        await IsValidLogin(context, email, password);
    }

    // Checks if both the email and password are present.
    // If either the email or password is missing, it sets the response status code to 400
    // and returns true to indicate that the validation failed.
    // It also writes an error message to the response if both email and password are missing.
    // Otherwise, it returns false to indicate that the validation passed.
    private static async Task<bool> IsRequired(HttpContext context, string email, string password)
    {
        var isEmail = string.IsNullOrEmpty(email);
        var isPassword = string.IsNullOrEmpty(password);

        if (!isEmail || !isPassword) return false;
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Both 'email' and 'password' are required\n");
        return true;
    }

    // Validates the login credentials.
    // Compares the email and password with the valid credentials.
    // If the credentials are valid, it writes a success message to the response.
    // Otherwise, it sets the response status code to 400 and writes an error message.
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

    // Retrieves the value of a query parameter from the request body.
    // Parses the query dictionary from the body and tries to get the value for the specified key.
    // If the key is found, it returns the value as a string.
    // If the key is not found, it returns null.
    private static string? GetQueryValue(string body, string key)
    {
        var queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
        return !queryDict.TryGetValue(key, out var value) ? null : Convert.ToString(value[0]);
    }

    // Builds an error message based on the validation results.
    // If the email is invalid or missing, it appends an error message for 'email'.
    // If the password is invalid or missing, it appends an error message for 'password'.
    // Returns the constructed error message as a string.
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
    // Extension method used to add the LoginMiddleware to the HTTP request pipeline.
    // Returns the updated IApplicationBuilder instance.
    public static IApplicationBuilder UseLoginMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LoginMiddleware>();
    }
}