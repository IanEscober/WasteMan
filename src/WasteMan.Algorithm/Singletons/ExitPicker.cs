using System;
using System.Linq;
using System.Collections.Generic;

namespace WasteMan.Algorithm.Singletons
{
    internal sealed class ExitPicker
    {
        private static readonly Lazy<ExitPicker> lazy =
               new Lazy<ExitPicker>(() => new ExitPicker());

        public static ExitPicker Instance => lazy.Value;

        private static readonly Dictionary<string, List<string>> Map = new Dictionary<string, List<string>>
        {
            { "G", new List<string> { "B", "F" } },
            { "M", new List<string> { "L", "V" } },
            { "I", new List<string> { "D", "C" } },
            { "S", new List<string> { "X", "T" } }
        };

        public string Get(string last, string secondLast) => Map
            .SingleOrDefault(point => point.Key == last)
                .Value
                    ?.First(item => item != secondLast);
    }
}
