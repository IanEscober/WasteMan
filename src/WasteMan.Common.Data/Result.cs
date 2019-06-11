using System.Collections.Generic;

namespace WasteMan.Common.Data
{
    public class Result
    {
        public string Source { get; set; }

        public List<Adjacency> AdjacencyList { get; set; }
        public string ShortestSequence { get; set; }
        public float SPFTotalDistance { get; set; }

        public List<Route> CollectionRoutes { get; set; }
        public string ShortestCollectionRoute { get; set; }
        public float MDFSTotalDistance { get; set; }
        public int PossiblePathsFound { get; set; }

        public List<Coordinate> Coordinates { get; set; }
    }
}
