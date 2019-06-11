using System.Collections.Generic;
using System.Threading.Tasks;
using WasteMan.Common.Data;

namespace WasteMan.Redis.Repositories
{
    public interface IGarbageBinCacheRepository
    {
        Task<GarbageBin> GetAsync(string name);
        Task<IEnumerable<GarbageBin>> GetAsync();
        Task SetAsync(GarbageBin bin);
    }
}