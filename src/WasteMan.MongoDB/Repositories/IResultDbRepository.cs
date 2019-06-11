using System;
using System.Threading.Tasks;
using WasteMan.Common.Data;

namespace WasteMan.MongoDB.Repositories
{
    public interface IResultDbRepository
    {
        Task<Result> GetAsync(DateTime date);
        Task SetAsync(Result result);
    }
}