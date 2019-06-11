using System.Collections.Generic;
using WasteMan.Common.Data;
using WasteMan.Algorithm.Core;

namespace WasteMan.Algorithm.Parsers
{
    internal static class AdjacencyParser
    {
        public static List<Adjacency> Parse(this Graph<string, float> graph)
        {
            var adjacencyList = new List<Adjacency>();
            foreach (var item in graph.AdjacencyList)
            {
                adjacencyList.Add(new Adjacency { Vertex = item.Key, Neighbors = item.Value });
            }

            return adjacencyList;
        }
    }
}
