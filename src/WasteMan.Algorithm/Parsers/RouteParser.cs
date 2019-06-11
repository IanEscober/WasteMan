using System.Text;
using System.Linq;
using System.Collections.Generic;
using WasteMan.Common.Data;

namespace WasteMan.Algorithm.Parsers
{
    internal static class RouteParser
    {
        public static Route Parse(this IEnumerable<(IEnumerable<string> Route, float Distance)> routes)
        {
            var formattedRoute = new List<(string, float)>();
            var stringBuilder = new StringBuilder();

            foreach (var route in routes)
            {
                foreach (var item in route.Route)
                {
                    stringBuilder.Append(item);
                    stringBuilder.Append("➞");
                }
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
                formattedRoute.Add((stringBuilder.ToString(), route.Distance));
                stringBuilder.Clear();
            }

            return new Route
            {
                Source = routes.First().Route.First(),
                Destination = routes.First().Route.Last(),
                Routes = formattedRoute
            };
        }
    }
}
