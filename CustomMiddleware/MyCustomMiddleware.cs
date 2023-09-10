namespace MyFirstDotNetCoreApp.CustomMiddleware;

public class MyCustomMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        await context.Response.WriteAsync("MyCustomMiddleware - Starts\n");
        await next(context);
        await  context.Response.WriteAsync("MyCustomMiddleware - Ends\n");
    }
}