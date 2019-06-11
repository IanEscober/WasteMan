using System;
using System.Threading.Tasks;
using WasteMan.Common.Data;

namespace WasteMan.Web.Api.Processors
{
    public interface IResultProcessor
    {
        Task<ResultDto> Get(DateTime date);
        Task<ResultDto> Get(string source);
    }
}