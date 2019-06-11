using System.Collections.Generic;

namespace WasteMan.Common.Data
{
    public class Adjacency
    {
        public string Vertex { get; set; }
        public HashSet<(string Neighbor, float Weight)> Neighbors { get; set; }
    }
}
