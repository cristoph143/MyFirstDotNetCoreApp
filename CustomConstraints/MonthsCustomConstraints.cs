using System.Text.RegularExpressions;

namespace MyFirstDotNetCoreApp.CustomConstraints;

public partial class MonthsCustomConstraints : IRouteConstraint
{
    [GeneratedRegex("^(apr|jul|oct|jan)$")]
    private static partial Regex MyRegex();

    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values,
        RouteDirection routeDirection)
    {
        // check whether values exist
        if (!values.ContainsKey(routeKey)) return false;

        var regex = MyRegex();

        var monthValue = Convert.ToString(values[routeKey]);

        return regex.IsMatch(monthValue ?? string.Empty);
    }
}