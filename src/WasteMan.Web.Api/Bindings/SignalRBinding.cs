using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WasteMan.SignalR.Helpers;

namespace WasteMan.Web.Api.Bindings
{
    public class SignalRBinding : BaseBinding
    {
        public override void Bind(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddSingleton<IGarbageBinHelper, GarbageBinHelper>();
        }
    }
}
