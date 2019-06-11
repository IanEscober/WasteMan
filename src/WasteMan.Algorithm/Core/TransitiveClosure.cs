using System.Linq;
using System.Collections.Generic;
using WasteMan.Common.Data;

namespace WasteMan.Algorithm.Core
{
    internal static class TransitiveClosure
    {
        public static Graph<string, float> GenerateGraph(this IEnumerable<Point> points)
        {
            var vertices = points.Select(point => point.Name);
            var edges = points.GenerateEdges();

            return new Graph<string, float>(vertices, edges);
        }

        private static IEnumerable<(string, string, float)> GenerateEdges(this IEnumerable<Point> points)
        {
            var edges = new List<(string, string, float)>();
            for (int i = 0; i < points.Count(); i++)
            {
                for (int j = i + 1; j < points.Count(); j++)
                {
                    var index = points.ElementAt(i);
                    var curr = points.ElementAt(j);
                    edges.Add((index.Name, curr.Name, index.Location.DistanceTo(curr.Location)));
                }
            }

            return edges;
        }
    }
}
