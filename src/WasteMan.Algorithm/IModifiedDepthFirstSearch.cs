using System.Collections.Generic;
using System.Threading.Tasks;
using WasteMan.Common.Data;

namespace WasteMan.Algorithm
{
    public interface IModifiedDepthFirstSearch
    {
        List<Route> CollectionRoutes { get; }
        int PossibleRoutesFound { get; }
        string ShortestCollectionRoute { get; }
        float TotalDistance { get; }

        Task ExecuteAsync(IEnumerable<string> points);
    }
}