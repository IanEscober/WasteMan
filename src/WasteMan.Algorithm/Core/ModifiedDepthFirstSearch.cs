using System;
using System.Linq;
using System.Collections.Generic;

namespace WasteMan.Algorithm.Core
{
    internal static class ModifiedDepthFirstSearch
    {
        public static Func<V, V, IEnumerable<IEnumerable<(V Vertex, W Weight)>>> MDFS<V, W>(this Graph<V, W> graph) =>
            (V s, V d) => MDFSUtility(graph, new List<HashSet<(V, W)>>(), new HashSet<V>(), s, d);

        private static IEnumerable<IEnumerable<(V, W)>> MDFSUtility<V, W>(Graph<V, W> graph, ICollection<HashSet<(V, W)>> path, HashSet<V> visited, V start, V destination)
        {
            visited.Add(start);

            if (start.Equals(destination))
            {
                path = TransformRoute(graph, path, visited);
            }
            else
            {
                MDFSRecursion(graph, path, visited, start, destination);
            }

            visited.Remove(start);
            return path;
        }

        private static void MDFSRecursion<V, W>(Graph<V, W> graph, ICollection<HashSet<(V, W)>> path, HashSet<V> visited, V start, V destination)
        {
            foreach (var (Neighbor, Weight) in graph.AdjacencyList[start])
            {
                if (!visited.Contains(Neighbor))
                {
                    _ = MDFSUtility(graph, path, visited, Neighbor, destination);
                }
            }
        }

        private static ICollection<HashSet<(V, W)>> TransformRoute<V, W>(Graph<V, W> graph, ICollection<HashSet<(V, W)>> path, HashSet<V> visited)
        {
            var generatedRoute = GenerateRoute(graph, visited);
            path.Add(generatedRoute);
            var trimmedRoute = TrimRoute(path, visited);
            return trimmedRoute;
        }

        private static ICollection<HashSet<(V, W)>> TrimRoute<V, W>(ICollection<HashSet<(V index, W weight)>> path, HashSet<V> visited)
        {
            var trimmedRoute = path.Where(vertex =>
            {
                var index = vertex.First().index;
                var indexVisited = visited.First();
                return index.Equals(indexVisited);
            });
            return trimmedRoute as ICollection<HashSet<(V, W)>>;
        }

        private static HashSet<(V, W)> GenerateRoute<V, W>(Graph<V, W> graph, HashSet<V> visited)
        {
            var tempPath = new HashSet<(V, W)> { (visited.First(), default(W)) };

            visited.Aggregate((curr, next) =>
            {
                tempPath.Add(graph.AdjacencyList[curr]
                    .Where(vertex => vertex.Neighbor
                        .Equals(next))
                            .FirstOrDefault());
                return next;
            });

            return tempPath;
        }
    }
}
