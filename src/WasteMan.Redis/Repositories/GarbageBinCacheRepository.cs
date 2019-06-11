using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Collections.Generic;
using AutoMapper;
using WasteMan.Redis.Factories;
using WasteMan.Common.Data;
using WasteMan.Common.Helpers;
using WasteMan.Redis.Data;
using WasteMan.Redis.Data.Enums;

namespace WasteMan.Redis.Repositories
{
    public class GarbageBinCacheRepository : IGarbageBinCacheRepository
    {
        private readonly ICacheClientFactory _cacheClientFactory;

        public GarbageBinCacheRepository(ICacheClientFactory cacheClientFactory)
        {
            _cacheClientFactory = cacheClientFactory;
            Initialize();
        }

        private void Initialize()
        {
            using (var cache = _cacheClientFactory.Create())
            {
                if (cache.Exists(nameof(HashKey.GarbageBins)))
                {
                    cache.Remove(nameof(HashKey.GarbageBins));
                }
            }
        }

        private Task<bool> ExistAsync(string name)
        {
            using (var cache = _cacheClientFactory.Create())
            {
                return cache.HashExistsAsync(nameof(HashKey.GarbageBins), name);
            }
        }

        public async Task<GarbageBin> GetAsync(string name)
        {
            using (var cache = _cacheClientFactory.Create())
            {
                var binDto = await cache.HashGetAsync<GarbageBinCacheDto>(nameof(HashKey.GarbageBins), name);

                if (binDto is null)
                {
                    return null;
                }

                return MapBin(name, binDto);
            }
        }

        public async Task<IEnumerable<GarbageBin>> GetAsync()
        {
            using (var cache = _cacheClientFactory.Create())
            {
                var binsDto = await cache.HashGetAllAsync<GarbageBinCacheDto>(nameof(HashKey.GarbageBins));

                if (binsDto.Count == 0)
                {
                    return null;
                }

                return await ConvertBinsAsync(binsDto);
            }
        }

        public async Task SetAsync(GarbageBin bin)
        {
            using (var cache = _cacheClientFactory.Create())
            {
                if (await ExistAsync(bin.Name))
                {
                    var prevBin = await GetAsync(bin.Name);
                    bin.Link(prevBin);
                }

                _ = cache.HashSetAsync(nameof(HashKey.GarbageBins), bin.Name, Mapper.Map<GarbageBin, GarbageBinCacheDto>(bin));
            }
        }

        private async Task<IEnumerable<GarbageBin>> ConvertBinsAsync(Dictionary<string, GarbageBinCacheDto> binsDto)
        {
            var bins = new ConcurrentBag<GarbageBin>();
            var mappingTasks = new List<Task>();

            foreach (var item in binsDto)
            {
                mappingTasks.Add(Task.Run(() => 
                    bins.Add(MapBin(item.Key, item.Value))));
            }

            await Task.WhenAll(mappingTasks);

            return bins;
        }

        private GarbageBin MapBin(string name, GarbageBinCacheDto binDto)
        {
            return Mapper.Map<GarbageBinCacheDto, GarbageBin>(binDto, opts =>
                 opts.AfterMap((src, dest) =>
                 {
                     dest.Name = name;
                     if(src.Latitude is null && src.Longitude is null)
                     {
                         dest.Location = null;
                     }
                 }));
        }
    }
}
