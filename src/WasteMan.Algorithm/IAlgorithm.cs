using System.Collections.Generic;
using System.Threading.Tasks;
using WasteMan.Common.Data;

namespace WasteMan.Algorithm
{
    public interface IAlgorithm
    {
        Task<Result> ExecuteAsync(IEnumerable<GarbageBin> bins, string source);
    }
}