using System;

namespace WasteMan.Algorithm.Parsers
{
    internal static class ResultParser
    {
        public static string[] Parse(this string input) =>
            input.Split(new string[] { "➞" }, StringSplitOptions.None);
    }
}
