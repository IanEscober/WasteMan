using System;
using System.Linq;
using System.Collections.Generic;

namespace WasteMan.Algorithm.Core
{
    internal static class ShortestPathFirst
    {
        public static Func<V, IEnumerable<(V Vertex, W Weight)>> SPF<V, W>(this Graph<V, W> graph) =>
            (start) => SPFUtility(graph, start);

        private static IEnumerable<(V, W)> SPFUtility<V, W>(Graph<V, W> graph, V start)
        {
            var sequence = new HashSet<(V index, W weight)> { (start, default(W)) };

            while (sequence.Count < graph.AdjacencyList.Count)
            {
                var unvisited = graph.AdjacencyList[start]
                    .Where(vertex => !sequence
                        .Select(item => item.index)
                            .Contains(vertex.Neighbor));

                var min = unvisited
                    .Where(vertex => vertex.Weight
                        .Equals(unvisited
                            .Min(item => item.Weight)))
                                .FirstOrDefault();

                sequence.Add(min);
                start = min.Neighbor;
            }

            return sequence;
        }
    }
}
