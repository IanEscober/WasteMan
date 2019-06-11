using System.Collections.Generic;

namespace WasteMan.Algorithm.Core
{
    internal class Graph<V, W>
    {
        public IDictionary<V, HashSet<(V Neighbor, W Weight)>> AdjacencyList { get; }

        public Graph()
        {
            AdjacencyList = new Dictionary<V, HashSet<(V, W)>>();
        }

        public Graph(IDictionary<V, HashSet<(V Neighbor, W Weight)>> adjacencyList)
        {
            AdjacencyList = adjacencyList;
        }

        public Graph(IEnumerable<V> vertices, IEnumerable<(V, V, W)> edges)
        {
            AdjacencyList = new Dictionary<V, HashSet<(V, W)>>();
            foreach (var vertex in vertices)
            {
                AddVertex(vertex);
            }
            foreach (var edge in edges)
            {
                AddEdge(edge);
            }
        }

        private void AddVertex(V vertex)
        {
            AdjacencyList[vertex] = new HashSet<(V, W)>();
        }

        private void AddEdge((V index, V neighbor, W weight) edge)
        {
            if (AdjacencyList.ContainsKey(edge.index) && AdjacencyList.ContainsKey(edge.neighbor))
            {
                AdjacencyList[edge.index].Add((edge.neighbor, edge.weight));
                AdjacencyList[edge.neighbor].Add((edge.index, edge.weight));
            }
        }
    }
}
