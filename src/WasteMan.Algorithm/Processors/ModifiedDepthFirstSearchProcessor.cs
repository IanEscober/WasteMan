using System;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WasteMan.Common.Data;
using WasteMan.Algorithm.Core;
using WasteMan.Algorithm.Parsers;
using WasteMan.Algorithm.Singletons;

namespace WasteMan.Algorithm.Processors
{
    public class ModifiedDepthFirstSearchProcessor : IModifiedDepthFirstSearch
    {
        public List<Route> CollectionRoutes { get; private set; }
        public string ShortestCollectionRoute { get; private set; }
        public float TotalDistance { get; private set; }
        public int PossibleRoutesFound { get; private set; }

        private readonly Func<Graph<string, float>> _graph;
        private IEnumerable<IEnumerable<(string Vertex, float Weight)>> _routes;
        private IEnumerable<(IEnumerable<string> Route, float Distance)> _possibleRoutes;
        private ICollection<(IEnumerable<string> Route, float Distance)> _shortestCollectionRoutes;
        private const int TAKE_COUNT = 5;

        public ModifiedDepthFirstSearchProcessor()
        {
            CollectionRoutes = new List<Route>();
            ShortestCollectionRoute = default(string);
            TotalDistance = default(float);
            PossibleRoutesFound = default(int);

            _graph = TargetAreaGraph.Instance.Get;
            _routes = null;
            _possibleRoutes = null;
            _shortestCollectionRoutes = new List<(IEnumerable<string>, float)>();
        }

        public async Task ExecuteAsync(IEnumerable<string> points)
        {
            for (int i = 0; i < points.Count() - 1; i++) //Ewww... for loop
            {
                await ProcessAsync(points.ElementAt(i), points.ElementAt(i + 1));
            }

            await ExitPickerAsync(points.First());

            (ShortestCollectionRoute, TotalDistance) = FormatResult();
        }

        private async Task ProcessAsync(string source, string destination)
        {
            _routes = await ApplyMDFSAsync(source, destination);
            PossibleRoutesFound += _routes.Count();

            var shortestRoute = await DetermineShortestRouteAsync();

            CollectionRoutes.Add(_possibleRoutes.Parse());
            _shortestCollectionRoutes.Add(shortestRoute);
        }

        private async Task<(IEnumerable<string>, float)> DetermineShortestRouteAsync()
        {
            var formattedRoutes = await FormatRoutesAsync();
            var shortestRoute = default((IEnumerable<string>, float)?);
            var offset = 0;
            do
            {
                _possibleRoutes = formattedRoutes;
                _possibleRoutes = TakePossibleRoutes(offset++);
                shortestRoute = SelectShortestRoute();
            } while (!shortestRoute.HasValue);
            return shortestRoute.Value;
        }

        private async Task ExitPickerAsync(string startingPoint)
        {
            var last = default(string);
            var secondLast = default(string);
            var exit = default(string);

            if (_shortestCollectionRoutes.Any())
            {
                last = _shortestCollectionRoutes.Last().Route.Last();
                secondLast = _shortestCollectionRoutes.Last().Route.Reverse().Skip(1).First();
                exit = ExitPicker.Instance.Get(last, secondLast);  
            }
            else //Started at last point
            {
                last = startingPoint;
                exit = ExitPicker.Instance.Get(last, secondLast);
            }

            if (!string.IsNullOrEmpty(exit))
            {
                await ProcessAsync(last, exit);
            }
        }

        private Task<IEnumerable<IEnumerable<(string, float)>>> ApplyMDFSAsync(string source, string destination) =>
            Task.Run(() =>
                _graph()
                    .MDFS()(source, destination));

        private Task<IOrderedEnumerable<(IEnumerable<string>, float)>> FormatRoutesAsync() =>
            Task.Run(() =>
                _routes.Select(route => FormatRoute(route))
                    .OrderBy(by => by.Distance));

        private (IEnumerable<string> Route, float Distance) FormatRoute(IEnumerable<(string Vertex, float Weight)> route)
        {
            var localRoute = route.Select(point => point.Vertex);
            var distance = route.Sum(point => point.Weight);
            return (localRoute, distance);
        }

        private IEnumerable<(IEnumerable<string>, float)> TakePossibleRoutes(int offset) =>
            _possibleRoutes
                .Take(TAKE_COUNT + offset);

        private (IEnumerable<string>, float)? SelectShortestRoute()
        {
            if (_shortestCollectionRoutes.Any())
            {
                var tryRoutes = _possibleRoutes.Where(possibleRoute => CheckReversal(possibleRoute));

                if(!tryRoutes.Any())
                {
                    return null;
                }
                return tryRoutes.First();
            }
            return _possibleRoutes.First();
        }

        private bool CheckReversal((IEnumerable<string> Route, float Distance) possibleRoute)
        {
            var localPossibleRoute = possibleRoute.Route;
            var secondFirstPoint = localPossibleRoute.Skip(1).First();
            var localShortestRoute = _shortestCollectionRoutes.Last().Route;
            var secondLastPoint = localShortestRoute.Reverse().Skip(1).First();
            return secondFirstPoint != secondLastPoint;
        }

        private (string, float) FormatResult()
        {
            var stringBuilder = new StringBuilder();
            var total = default(float);
            var prev = default(string);

            foreach (var route in _shortestCollectionRoutes)
            {
                foreach (var item in route.Route)
                {
                    if (item != prev)
                    {
                        stringBuilder.Append(item);
                        stringBuilder.Append("➞");
                    }
                    prev = item;
                }
                total += route.Distance;
            }

            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return (stringBuilder.ToString(), total);
        }
    }
}
