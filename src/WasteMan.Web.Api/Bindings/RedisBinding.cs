using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Protobuf;
using WasteMan.Redis.Factories;
using WasteMan.Redis.Repositories;

namespace WasteMan.Web.Api.Bindings
{
    public class RedisBinding : BaseBinding
    {
        public override void Bind(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddSingleton<ISerializer, ProtobufSerializer>();
            services.AddSingleton(configuration.GetSection("Redis").Get<RedisConfiguration>());
            services.AddSingleton<ICacheClientFactory, CacheClientFactory>();
            services.AddSingleton<IGarbageBinCacheRepository, GarbageBinCacheRepository>();
        }
    }
}
