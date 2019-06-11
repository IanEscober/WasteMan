using System.Linq;
using System.Collections.Generic;
using WasteMan.Common.Data;
using WasteMan.Algorithm.Singletons;

namespace WasteMan.Algorithm.Parsers
{
    internal static class CoordinateParser
    {
        public static List<Coordinate> Parse(this string[] points) =>
            points.Select(point => IntersectionPoints.Instance.Get(point).Location).ToList();
    }
}
