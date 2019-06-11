using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WasteMan.Web.Api.Bindings
{
    public abstract class BaseBinding
    {
        public abstract void Bind(IServiceCollection services, IConfiguration configuration = null);
    }
}
