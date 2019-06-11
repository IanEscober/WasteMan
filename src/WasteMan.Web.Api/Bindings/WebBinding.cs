using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WasteMan.Web.Api.Processors;

namespace WasteMan.Web.Api.Bindings
{
    public class WebBinding : BaseBinding
    {
        public override void Bind(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddTransient<IGarbageBinProcessor, GarbageBinProcessor>();
            services.AddTransient<IResultProcessor, ResultProcessor>();
        }
    }
}
