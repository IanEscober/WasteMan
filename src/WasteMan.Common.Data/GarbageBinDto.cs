using System.Collections.Generic;

namespace WasteMan.Common.Data
{
    public class GarbageBinDto
    {
        public string Name { get; set; }
        public string LidState { get; set; }
        public string LidStateTimeStamp { get; set; }
        public float Level { get; set; }
        public Coordinate Location { get; set; }
        public List<string> Ticks { get; set; }
        public List<float> Values { get; set; }  
    }
}
