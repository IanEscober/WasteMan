using System;
using System.Linq;
using System.Collections.Generic;

namespace WasteMan.Algorithm.Singletons
{
    internal sealed class GarbageBinToPoint
    {
        private static readonly Lazy<GarbageBinToPoint> lazy =
                new Lazy<GarbageBinToPoint>(() => new GarbageBinToPoint());

        public static GarbageBinToPoint Instance => lazy.Value;

        private static readonly Dictionary<string, string> Map = new Dictionary<string, string>
        {
            { "1", "G" },
            { "2", "C" },
            { "3", "I" },
            { "4", "K" },
            { "5", "M" },
            { "6", "S" },
            { "7", "U" },
            { "8", "X" }
        };

        public string Convert(string binName) => Map.Where(keys => keys.Key == binName).First().Value;
    }
}
