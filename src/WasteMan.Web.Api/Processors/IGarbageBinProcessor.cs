using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WasteMan.Common.Data;

namespace WasteMan.Web.Api.Processors
{
    public interface IGarbageBinProcessor
    {
        Task<IEnumerable<GarbageBinDto>> Get();
        Task<IEnumerable<GarbageBinDto>> Get(DateTime date);
    }
}