using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using WasteMan.Common.Data;
using WasteMan.Common.Helpers;
using WasteMan.Redis.Repositories;
using WasteMan.MongoDB.Repositories;
using WasteMan.Algorithm;

namespace WasteMan.Web.Api.Processors
{
    public class ResultProcessor : IResultProcessor
    {
        private readonly IGarbageBinCacheRepository _garbageBinCacheRepository;
        private readonly IGarbageBinDbRepository _garbageBinDbRepository;
        private readonly IResultDbRepository _resultDbRepository;
        private readonly IAlgorithm _algorithm;
        public ResultProcessor(IGarbageBinCacheRepository garbageBinCacheRepository,
                               IGarbageBinDbRepository garbageBinDbRepository,
                               IResultDbRepository resultDbRepository,
                               IAlgorithm algorithm)
        {
            _garbageBinCacheRepository = garbageBinCacheRepository;
            _garbageBinDbRepository = garbageBinDbRepository;
            _resultDbRepository = resultDbRepository;
            _algorithm = algorithm;
        }

        public async Task<ResultDto> Get(string source)
        {
            var bins = await ProcessGarbageBins();

            if (bins is null)
            {
                return null;
            }

            else
            {
                var result = await ProcessResult(bins, source);

                return Mapper.Map<ResultDto>(result);
            }
        }

        public async Task<ResultDto> Get(DateTime date)
        {
            var result = await _resultDbRepository.GetAsync(date.Date);

            return Mapper.Map<ResultDto>(result);
        }

        private async Task<IEnumerable<GarbageBin>> ProcessGarbageBins()
        {
            var bins = await _garbageBinCacheRepository.GetAsync();

            if(bins is null)
            {
                return null;
            }

            SetToDB(bins);

            var filteredBins = bins.Filter();

            return filteredBins.Any() ? filteredBins : null;
        }

        private async Task<Result> ProcessResult(IEnumerable<GarbageBin> bins, string source)
        {
            var result = await _algorithm.ExecuteAsync(bins, source);

            SetToDB(result);

            return result;
        }

        private void SetToDB<T>(T input)
        {
            switch (input)
            {
                case IEnumerable<GarbageBin> bins:
                    _ = _garbageBinDbRepository.SetAsync(bins);
                    break;
                case Result result:
                    _ = _resultDbRepository.SetAsync(result);
                    break;
                default:
                    throw new TypeLoadException("Type is not supported");
            }
        }
    }
}
