using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WasteMan.Algorithm;
using WasteMan.Algorithm.Processors;

namespace WasteMan.Web.Api.Bindings
{
    public class AlgorithmBinding : BaseBinding
    {
        public override void Bind(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddTransient<IShortestPathFirst, ShortestPathFirstProcessor>();
            services.AddTransient<IModifiedDepthFirstSearch, ModifiedDepthFirstSearchProcessor>();
            services.AddTransient<IAlgorithm, Algorithm.Algorithm>();
        }
    }
}
