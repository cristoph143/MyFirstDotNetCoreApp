using System.Text;

namespace MyFirstDotNetCoreApp;

public static class Calculator
{
    public static async Task HttpCalculator(string method, string path, HttpContext context)
    {
        if (method == "GET" && path == "/")
        {
            var errorMessages = new StringBuilder();

            var isFirst = await IsContainKeys(context, "firstNumber", errorMessages);
            var isSecond = await IsContainKeys(context, "secondNumber", errorMessages);
            var isOps = await IsContainKeys(context, "operation", errorMessages);

            if (!isFirst || !isSecond || !isOps)
            {
                await context.Response.WriteAsync(errorMessages.ToString());
                await context.Response.CompleteAsync();
                return;
            }

            await CalculateResult(context);
        }
    }

    public static async Task CalculateResult(HttpContext context)
    {
        var errorMessages = new StringBuilder();

        var isFirst = await IsContainKeys(context, "firstNumber", errorMessages);
        var isSecond = await IsContainKeys(context, "secondNumber", errorMessages);
        var isOps = await IsContainKeys(context, "operation", errorMessages);

        if (!isFirst || !isSecond || !isOps)
        {
            await context.Response.WriteAsync(errorMessages.ToString());
            await context.Response.CompleteAsync();
            return;
        }

        var firstNumber = await ConvertToInt(context, "firstNumber", errorMessages);
        var secondNumber = await ConvertToInt(context, "secondNumber", errorMessages);
        var operation = context.Request.Query["operation"].FirstOrDefault();
        var result = await GetCalculatedResult(context, operation, firstNumber, secondNumber);

        await context.Response.WriteAsync(result?.ToString() ?? string.Empty);
        await context.Response.CompleteAsync();
    }

    public static Task<bool> IsContainKeys(HttpContext context, string keys, StringBuilder errorMessages)
    {
        if (context.Request.Query.ContainsKey(keys)) return Task.FromResult(true);
        if (context.Response.HasStarted) return Task.FromResult(false);
        context.Response.StatusCode = 400;
        errorMessages.AppendLine($"Invalid input for {keys}");
        return Task.FromResult(false);
    }

    public static Task<int> ConvertToInt(HttpContext context, string keys, StringBuilder errorMessages)
    {
        var numberString = context.Request.Query[keys].FirstOrDefault();
        if (!string.IsNullOrEmpty(numberString)) return Task.FromResult(Convert.ToInt32(numberString));

        if (!context.Response.HasStarted) errorMessages.AppendLine($"Invalid input for {keys}");
        return Task.FromResult(0);
    }

    public static async Task<long?> GetCalculatedResult(HttpContext context, string? operation, int firstNumber,
        int secondNumber)
    {
        return operation switch
        {
            "add" => firstNumber + secondNumber,
            "subtract" => firstNumber - secondNumber,
            "multiply" => firstNumber * secondNumber,
            "divide" => secondNumber != 0 ? firstNumber / secondNumber : 0,
            "mod" => secondNumber != 0 ? firstNumber % secondNumber : 0,
            _ => await HandleInvalidOperation(context, "operation")
        };
    }

    public static async Task<long?> HandleInvalidOperation(HttpContext context, string keys)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync($"Invalid input for {keys}\n");
        await context.Response.CompleteAsync();
        return null;
    }
}