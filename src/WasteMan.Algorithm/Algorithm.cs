using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WasteMan.Common.Data;
using WasteMan.Algorithm.Parsers;
using WasteMan.Algorithm.Singletons;

namespace WasteMan.Algorithm
{
    public class Algorithm : IAlgorithm
    {
        private readonly IShortestPathFirst _spf;
        private readonly IModifiedDepthFirstSearch _mdfs;

        public Algorithm(IShortestPathFirst spf, IModifiedDepthFirstSearch mdfs)
        {
            _spf = spf;
            _mdfs = mdfs;
        }

        public async Task<Result> ExecuteAsync(IEnumerable<GarbageBin> bins, string source)
        {
            #region HardCoding Garbage Bins Coordinates - Presentation Purposes
            bins = bins.Select(bin =>
            {
                var point = GarbageBinToPoint.Instance.Convert(bin.Name);
                var location = IntersectionPoints.Instance.Get(point).Location;
                bin.Location = location;
                return bin;
            });
            #endregion

            var sourcePoint = IntersectionPoints.Instance.Get(source);

            await _spf.ExecuteAsync(MapBinsToPoints(bins), sourcePoint);

            var points = ExtractPoints(sourcePoint);

            await _mdfs.ExecuteAsync(points);

            return FormatResult(source);
        }

        private IEnumerable<Point> MapBinsToPoints(IEnumerable<GarbageBin> bins) =>
            bins
                .Select(bin => new Point { Name = bin.Name, Location = bin.Location })
                    .OrderBy(bin => bin.Name);

        private IEnumerable<string> ExtractPoints(Point sourcePoint)
        {
            var sequence = _spf.ShortestSequence.Parse();
            var points = sequence.Select(bin => GarbageBinToPoint.Instance.Convert(bin));

            if (points.First() != sourcePoint.Name)
            {
                points = points.Prepend(sourcePoint.Name);
            }

            return points;
        }

        private Result FormatResult(string source) =>
            new Result
            {
                Source = source,

                AdjacencyList = _spf.AdjacencyList,
                ShortestSequence = _spf.ShortestSequence,
                SPFTotalDistance = _spf.TotalDistance,

                CollectionRoutes = _mdfs.CollectionRoutes,
                ShortestCollectionRoute = _mdfs.ShortestCollectionRoute,
                MDFSTotalDistance = _mdfs.TotalDistance,
                PossiblePathsFound = _mdfs.PossibleRoutesFound,

                Coordinates = _mdfs.ShortestCollectionRoute.Parse().Parse()
            };
    }
}
