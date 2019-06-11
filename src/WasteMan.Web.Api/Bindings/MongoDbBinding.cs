using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using WasteMan.MongoDB.Repositories;

namespace WasteMan.Web.Api.Bindings
{
    public class MongoDbBinding : BaseBinding
    {
        public override void Bind(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddSingleton(new MongoUrl(configuration.GetSection("MongoDB:ConnectionString").Value));
            services.AddSingleton<IMongoClient, MongoClient>();
            services.AddSingleton(provider =>
                provider
                    .GetService<IMongoClient>()
                    .GetDatabase(configuration
                    .GetSection("MongoDB:Database").Value));
            services.AddSingleton<IGarbageBinDbRepository, GarbageBinDbRepository>();
            services.AddSingleton<IResultDbRepository, ResultDbRepository>();
        }
    }
}
