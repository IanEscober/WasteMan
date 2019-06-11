using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WasteMan.Common.Data;
using WasteMan.Algorithm.Core;
using WasteMan.Algorithm.Parsers;

namespace WasteMan.Algorithm.Processors
{
    public class ShortestPathFirstProcessor : IShortestPathFirst
    {
        public List<Adjacency> AdjacencyList { get; private set; }
        public string ShortestSequence { get; private set; }
        public float TotalDistance { get; private set; }

        private Graph<string, float> _graph;
        private IEnumerable<(string Vertex, float Weight)> _sequence;


        public ShortestPathFirstProcessor()
        {
            AdjacencyList = null;
            ShortestSequence = default(string);
            TotalDistance = default(float);

            _graph = null;
            _sequence = null;
        }

        public async Task ExecuteAsync(IEnumerable<Point> points, Point source)
        {
            _graph = await ApplyTransitiveClosureAsync(points);
            _sequence = await ApplySPFAsync(SelectNearestPoint(points, source));

            AdjacencyList = _graph.Parse();
            (ShortestSequence, TotalDistance) = FormatResult();
        }


        private Task<IEnumerable<(string Vertex, float Weight)>> ApplySPFAsync(string source) =>
            Task.Run(() => _graph.SPF()(source));

        private Task<Graph<string, float>> ApplyTransitiveClosureAsync(IEnumerable<Point> points) =>
            Task.Run(() => points.GenerateGraph());

        private string SelectNearestPoint(IEnumerable<Point> points, Point source) =>
            points
                .OrderBy(point => point.Location.DistanceTo(source.Location))
                    .First().Name;

        private (string, float) FormatResult()
        {
            var stringBuilder = new StringBuilder();
            var total = default(float);

            foreach (var (Vertex, Weight) in _sequence)
            {
                stringBuilder.Append(Vertex);
                stringBuilder.Append("➞");
                total += Weight;
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return (stringBuilder.ToString(), total);
        }
    }
}
