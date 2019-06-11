using System.Collections.Generic;

namespace WasteMan.Common.Data
{
    public class Route
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public IEnumerable<(string Route, float Distance)> Routes { get; set; }
    }
}
