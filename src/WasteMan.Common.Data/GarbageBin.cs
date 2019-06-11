using System;
using System.Collections.Generic;
using WasteMan.Common.Data.Enums;

namespace WasteMan.Common.Data
{
    public class GarbageBin
    {
        public string Name { get; set; }
        public Coordinate Location { get; set; }
        public LidStates LidSate { get; set; }
        public TimeSpan LidStateTimeStamp { get; set; }
        public float Level { get; set; }
        public Dictionary<TimeSpan,float> Status { get; set; }
    }
}
