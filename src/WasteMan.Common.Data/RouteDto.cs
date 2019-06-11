using System.Collections.Generic;

namespace WasteMan.Common.Data
{
    public class RouteDto
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public List<string> Routes { get; set; }
        public List<float> Distances { get; set; }
    }
}
