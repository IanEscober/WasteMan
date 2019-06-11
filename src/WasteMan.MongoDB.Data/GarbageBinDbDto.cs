using System;
using System.Collections.Generic;
using WasteMan.Common.Data;

namespace WasteMan.MongoDB.Data
{
    public class GarbageBinDbDto
    {
        public DateTime Date { get; set; }
        public IEnumerable<GarbageBin> GarbageBins { get; set; }
    }
}
