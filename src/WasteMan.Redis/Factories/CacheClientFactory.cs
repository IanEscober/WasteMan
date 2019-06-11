using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;

namespace WasteMan.Redis.Factories
{
    public class CacheClientFactory : ICacheClientFactory
    {
        private readonly ISerializer _serializer;
        private readonly RedisConfiguration _config;

        public CacheClientFactory(ISerializer serializer, RedisConfiguration config)
        {
            _serializer = serializer;
            _config = config;
        }

        public ICacheClient Create()
        {
            return new StackExchangeRedisCacheClient(_serializer, _config);
        }
    }
}
