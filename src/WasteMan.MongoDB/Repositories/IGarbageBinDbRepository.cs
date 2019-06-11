using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using WasteMan.Common.Data;

namespace WasteMan.MongoDB.Repositories
{
    public interface IGarbageBinDbRepository
    {
        Task<IEnumerable<GarbageBin>> GetAsync(DateTime date);
        Task SetAsync(IEnumerable<GarbageBin> garbageBins);
    }
}