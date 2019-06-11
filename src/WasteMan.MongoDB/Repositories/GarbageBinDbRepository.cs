using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using WasteMan.Common.Data;
using WasteMan.MongoDB.Data;
using WasteMan.MongoDB.Data.Enums;

namespace WasteMan.MongoDB.Repositories
{
    public class GarbageBinDbRepository : IGarbageBinDbRepository
    {
        private readonly IMongoCollection<GarbageBinDbDto> _mongoCollection;
        private readonly FilterDefinition<GarbageBinDbDto> _filter;

        public GarbageBinDbRepository(IMongoDatabase mongoDatabase)
        {
            Initialize(mongoDatabase);
            _mongoCollection = mongoDatabase.GetCollection<GarbageBinDbDto>(nameof(Collections.GarbageBins));
            _filter = Builders<GarbageBinDbDto>.Filter.Eq(nameof(GarbageBinDbDto.Date), DateTime.Now.Date);
        }

        public async Task SetAsync(IEnumerable<GarbageBin> garbageBins)
        {
            var update = Builders<GarbageBinDbDto>.Update.Set(nameof(GarbageBinDbDto.GarbageBins), garbageBins);

            if(await ExistAsync())
            {
                _ = _mongoCollection.UpdateOneAsync(_filter, update);
            }

            else
            {
                _ = _mongoCollection.InsertOneAsync(new GarbageBinDbDto { Date = DateTime.Now.Date, GarbageBins = garbageBins });
            }
        }

        public async Task<IEnumerable<GarbageBin>> GetAsync(DateTime date)
        {
            var queryResult = await _mongoCollection.FindAsync(filter => filter.Date == date.Date);
            return queryResult.FirstOrDefault()?.GarbageBins;
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
            var filter = Builders<BsonDocument>.Filter.Eq("name", nameof(Collections.GarbageBins));
            var collections = mongoDatabase.ListCollections(new ListCollectionsOptions { Filter = filter });

            if(!collections.Any())
            {
                mongoDatabase.CreateCollection(nameof(Collections.GarbageBins));
            }
        }
    }
}
