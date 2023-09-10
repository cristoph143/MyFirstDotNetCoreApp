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
            // var reader = new StreamReader(context.Request.Body);
            // var body = await reader.ReadToEndAsync();
            string path = context.Request.Path;
            var method = context.Request.Method;

            if (method == "GET" && path == "/")
            {
                int firstNumber = 0, secondNumber = 0;

                if (context.Request.Query.ContainsKey("firstNumber"))
                {
                    var firstNumberString = context.Request.Query["firstNumber"][0];
                    if (string.IsNullOrEmpty(firstNumberString))
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
                        return;
                    }

                    firstNumber = Convert.ToInt32(firstNumberString);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input for 'firstNumber'\n");
                    return;
                }

                if (context.Request.Query.ContainsKey("secondNumber"))
                {
                    var secondNumberString = context.Request.Query["secondNumber"][0];
                    if (string.IsNullOrEmpty(secondNumberString))
                    {
                        context.Response.StatusCode = 400;
                        await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
                        return;
                    }

                    secondNumber = Convert.ToInt32(secondNumberString);
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input for 'secondNumber'\n");
                    return;
                }

                if (context.Request.Query.ContainsKey("operation"))
                {
                    var operation = Convert.ToString(context.Request.Query["operation"][0]);

                    long? result;
                    switch (operation)
                    {
                        case "add":
                            result = firstNumber + secondNumber;
                            break;
                        case "subtract":
                            result = firstNumber - secondNumber;
                            break;
                        case "multiply":
                            result = firstNumber * secondNumber;
                            break;
                        case "divide":
                            result = secondNumber != 0 ? firstNumber / secondNumber : 0;
                            break;
                        case "mod":
                            result = secondNumber != 0 ? firstNumber % secondNumber : 0;
                            break;
                        default:
                            context.Response.StatusCode = 400;
                            await context.Response.WriteAsync("Invalid input for 'operation'\n");
                            return;
                    }

                    await context.Response.WriteAsync(result.Value.ToString());
                    await context.Response.CompleteAsync(); // Send the response back to the client
                }
                else
                {
                    context.Response.StatusCode = 400;
                    await context.Response.WriteAsync("Invalid input for 'operation'\n");
                    await context.Response.CompleteAsync(); // Send the response back to the client
                }
            }
            // Dictionary<string, StringValues> queryDict =
            //     Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
            // await queyDict(queryDict, context, body);
            // await context.Response.WriteAsync("Hello World1!");
            // await queryString(context, path, method);
        });
        app.Run();
    }

    private static async Task queyDict(Dictionary<string, StringValues> queryDict, HttpContext context, string body)
    {
        if (queryDict.ContainsKey("firstName"))
        {
            var firstName = queryDict["firstName"][0];
            await context.Response.WriteAsync($"<p>Hello World1! {body} ? {queryDict} {firstName}</p>");
        }
    }

    private static async Task queryString(HttpContext context, string path, string method)
    {
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