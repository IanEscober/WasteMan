using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WasteMan.Web.Api.Startups;

namespace WasteMan.Web.Api.Bindings
{
    public class StartupBinding : BaseBinding
    {
        public override void Bind(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddTransient<IStartupFilter, MQTTStartupFilter>();
        }
    }
}
