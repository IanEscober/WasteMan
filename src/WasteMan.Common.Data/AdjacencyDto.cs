using System.Collections.Generic;

namespace WasteMan.Common.Data
{
    public class AdjacencyDto
    {
        public string Vertex { get; set; }
        public List<string> Neighbors { get; set; }
        public List<float> Weights { get; set; }
    }
}
