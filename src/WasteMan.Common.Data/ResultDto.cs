using System.Collections.Generic;

namespace WasteMan.Common.Data
{
    public class ResultDto
    {
        public string Source { get; set; }

        public List<AdjacencyDto> AdjacencyList { get; set; }
        public string ShortestSequence { get; set; }
        public float SPFTotalDistance { get; set; }

        public List<RouteDto> CollectionRoutes { get; set; }
        public string ShortestCollectionRoute { get; set; }
        public float MDFSTotalDistance { get; set; }
        public int PossiblePathsFound { get; set; }

        public List<CoordinateDto> Coordinates { get; set; }
    }
}
