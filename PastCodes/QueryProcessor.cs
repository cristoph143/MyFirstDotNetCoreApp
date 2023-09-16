using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace MyFirstDotNetCoreApp
{
    public static class QueryProcessor
    {
        public static async Task ProcessQuery(HttpContext context, string body, string path, string method)
        {
            var queryDict = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(body);
            await ProcessQueryDict(queryDict, context, body);
            await context.Response.WriteAsync("Hello World1!");
            await ProcessQueryString(context, path, method);
        }

        private static async Task ProcessQueryDict(Dictionary<string, StringValues> queryDict, HttpContext context, string body)
        {
            if (queryDict.ContainsKey("firstName"))
            {
                var firstName = queryDict["firstName"][0];
                await context.Response.WriteAsync($"<p>Hello World1! {body} ? {queryDict} {firstName}</p>");
            }
        }

        private static async Task ProcessQueryString(HttpContext context, string path, string method)
        {
            await context.Response.WriteAsync("Hello World1!");
            await context.Response.WriteAsync("<h1>Hello World1!</h1>");
            await context.Response.WriteAsync($"<p>Hello World1! {path} ? {method}</p>");
            await context.Response.WriteAsync("Hello World2!");

            switch (method)
            {
                case "GET":
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