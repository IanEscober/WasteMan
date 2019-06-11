using System.Collections.Generic;
using System.Threading.Tasks;
using WasteMan.Common.Data;

namespace WasteMan.Algorithm
{
    public interface IShortestPathFirst
    {
        List<Adjacency> AdjacencyList { get; }
        string ShortestSequence { get; }
        float TotalDistance { get; }

        Task ExecuteAsync(IEnumerable<Point> bins, Point source);
    }
}