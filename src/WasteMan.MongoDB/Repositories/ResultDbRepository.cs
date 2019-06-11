using System;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using WasteMan.Common.Data;
using WasteMan.MongoDB.Data;
using WasteMan.MongoDB.Data.Enums;

namespace WasteMan.MongoDB.Repositories
{
    public class ResultDbRepository : IResultDbRepository
    {
        private readonly IMongoCollection<ResultDbDto> _mongoCollection;
        private readonly FilterDefinition<ResultDbDto> _filter;

        public ResultDbRepository(IMongoDatabase mongoDatabase)
        {
            Initialize(mongoDatabase);
            _mongoCollection = mongoDatabase.GetCollection<ResultDbDto>(nameof(Collections.Results));
            _filter = Builders<ResultDbDto>.Filter.Eq(nameof(ResultDbDto.Date), DateTime.Now.Date);
        }

        public async Task SetAsync(Result result)
        {
            var update = Builders<ResultDbDto>.Update.Set(nameof(ResultDbDto.Result), result);

            if (await ExistAsync())
            {
                _ = _mongoCollection.UpdateOneAsync(_filter, update);
            }

            else
            {
                _ = _mongoCollection.InsertOneAsync(new ResultDbDto { Date = DateTime.Now.Date, Result = result });
            }
        }

        public async Task<Result> GetAsync(DateTime date)
        {
            var queryResult = await _mongoCollection.FindAsync(filter => filter.Date == date.Date);
            return queryResult.FirstOrDefault()?.Result;
        }

        private async Task<bool> ExistAsync()
        {
            var count = await _mongoCollection.FindAsync(_filter);

            if (await count.AnyAsync())
            {
                return true;
            }

            return false;
        }

        private void Initialize(IMongoDatabase mongoDatabase)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("name", nameof(Collections.Results));
            var collections = mongoDatabase.ListCollections(new ListCollectionsOptions { Filter = filter });

            if (!collections.Any())
            {
                mongoDatabase.CreateCollection(nameof(Collections.Results));
            }
        }
    }
}
