using System;
using System.Collections.Generic;
using WasteMan.Algorithm.Core;

namespace WasteMan.Algorithm.Singletons
{
    internal sealed class TargetAreaGraph
    {
        private static readonly Lazy<TargetAreaGraph> lazy =
               new Lazy<TargetAreaGraph>(() => new TargetAreaGraph());

        public static TargetAreaGraph Instance => lazy.Value;

        private static readonly List<string> Vertices = new List<string>
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "I", "I*", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "R*", "S", "T", "U", "V", "W", "X", "Y"
        };
        private static readonly List<(string, string, float)> Edges = new List<(string, string, float)>
        {
            ("A", "B", 228f),("A", "F", 148f),
            ("B", "C", 270f),("B", "G", 152f),
            ("C", "H", 149f),("C", "D", 143f),
            ("D", "I", 147f),("D", "E", 139f),
            ("E", "J", 151f),
            ("F", "G", 223f),("F", "L", 211f),
            ("G", "H", 272f),("G", "M", 213f),
            ("H", "N", 212f),("H", "I", 143f),
            ("I", "R", 286f),("I", "I*", 76f),
            ("I*", "J", 71f),("I*", "R*", 287f),
            ("J", "S", 286f),("J", "K", 261f),
            ("K", "T", 285f),
            ("L", "M", 218f),("L", "O", 67f),
            ("M", "N", 271f),("M", "P", 69f),
            ("N", "Q", 71f),
            ("O", "P", 217f),("O", "U", 78f),
            ("P", "Q", 274f),("P", "V", 80f),
            ("Q", "R", 143f),("Q", "W", 76f),
            ("R", "R*", 74f),
            ("R*", "S", 76f),
            ("S", "T", 264f),("S", "X", 75f),
            ("T", "Y", 76f),
            ("U", "V", 215f),
            ("V", "W", 273f),
            ("W", "X", 287f),
            ("X", "Y", 261f)
        };
        private static readonly Graph<string, float> Graph = new Graph<string, float>(Vertices, Edges);

        public Graph<string, float> Get() => Graph;
    }
}
