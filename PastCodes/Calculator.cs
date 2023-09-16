using System.Text;

namespace MyFirstDotNetCoreApp;

public static class Calculator
{
    /*
    HttpCalculator function: Handles HTTP requests for calculating operations.
    It checks for valid inputs and performs calculations if inputs are valid.
    */
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

    /*
    	CalculateResult function: Computes results based on valid inputs.
        Performs requested calculations and sends the response.
        -------------------------------------------------------------------------------------
        In the CalculateResult function, the errorMessages object is initialized as a new instance of the StringBuilder class. 
        Then, each time the IsContainKeys function is called and a query parameter is missing, 
        an error message is appended to the errorMessages object using the AppendLine method. 
        The AppendLine method adds a new line to the end of the string and appends the specified value.
        Since errorMessages is a StringBuilder, it maintains an internal buffer to hold the characters. 
        If there is space available in the buffer, it appends new data. Otherwise, it creates a new buffer, 
        copies the old data to the new buffer, and then appends the data.
        Once all three query parameters have been checked for existence, any error messages that were appended to errorMessages 
        are written to the response stream using the WriteAsync method.
        -------------------------------------------------------------------------------------
    */
    private static async Task CalculateResult(HttpContext context)
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

    // IsContainKeys function: Checks for the existence of a specific query parameter.
    // Returns true if the parameter exists; otherwise, sets a 400 status code and returns false.
    private static Task<bool> IsContainKeys(HttpContext context, string keys, StringBuilder errorMessages)
    {
        if (context.Request.Query.ContainsKey(keys)) return Task.FromResult(true);
        if (context.Response.HasStarted) return Task.FromResult(false);
        context.Response.StatusCode = 400;
        errorMessages.AppendLine($"Invalid input for {keys}");
        return Task.FromResult(false);
    }

    // ConvertToInt function: Converts a query parameter to an integer.
    // Returns the integer value if successful, or 0 on conversion failure.
    private static Task<int> ConvertToInt(HttpContext context, string keys, StringBuilder errorMessages)
    {
        var numberString = context.Request.Query[keys].FirstOrDefault();
        if (!string.IsNullOrEmpty(numberString)) return Task.FromResult(Convert.ToInt32(numberString));

        if (!context.Response.HasStarted) errorMessages.AppendLine($"Invalid input for {keys}");
        return Task.FromResult(0);
    }

    // GetCalculatedResult function: Computes the result of a requested operation.
    // Handles addition, subtraction, multiplication, division, or modulus based on the operation.
    private static async Task<long?> GetCalculatedResult(HttpContext context, string? operation, int firstNumber,
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

    // HandleInvalidOperation function: Manages invalid operations by setting a 400 status code
    // and sending an error message in the response.
    private static async Task<long?> HandleInvalidOperation(HttpContext context, string keys)
    {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync($"Invalid input for {keys}\n");
        await context.Response.CompleteAsync();
        return null;
    }
}