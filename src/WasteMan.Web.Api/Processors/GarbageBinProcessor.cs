using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WasteMan.Common.Data;
using WasteMan.Redis.Repositories;
using WasteMan.MongoDB.Repositories;

namespace WasteMan.Web.Api.Processors
{
    public class GarbageBinProcessor : IGarbageBinProcessor
    {
        private readonly IGarbageBinCacheRepository _garbageBinCacheRepository;
        private readonly IGarbageBinDbRepository _garbageBinDbRepository;
        public GarbageBinProcessor(IGarbageBinCacheRepository garbageBinCacheRepository, IGarbageBinDbRepository garbageBinDbRepository)
        {
            _garbageBinCacheRepository = garbageBinCacheRepository;
            _garbageBinDbRepository = garbageBinDbRepository;
        }

        public async Task<IEnumerable<GarbageBinDto>> Get() =>
            await FormatGarbageBins(
                await _garbageBinCacheRepository.GetAsync());


        public async Task<IEnumerable<GarbageBinDto>> Get(DateTime date) =>
            await FormatGarbageBins(
                await _garbageBinDbRepository.GetAsync(date.Date));

        private Task<IEnumerable<GarbageBinDto>> FormatGarbageBins(IEnumerable<GarbageBin> bins) =>
            Task.Run(() => bins
                ?.OrderBy(bin => bin.Name)
                ?.Select(item => Mapper.Map<GarbageBinDto>(item)));


    }
}
