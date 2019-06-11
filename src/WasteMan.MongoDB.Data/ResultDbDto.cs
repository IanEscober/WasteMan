using System;
using WasteMan.Common.Data;

namespace WasteMan.MongoDB.Data
{
    public class ResultDbDto
    {
        public DateTime Date { get; set; }
        public Result Result { get; set; }
    }
}
