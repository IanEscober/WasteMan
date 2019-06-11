using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using WasteMan.Web.Api.Bindings;

namespace WasteMan.Web.Api.Registrations
{
    public class BindingsRegistry
    {
        private readonly IServiceCollection _services;
        private readonly IConfiguration _configuration;
        private Dictionary<BaseBinding, bool> bindings;
        public BindingsRegistry(IServiceCollection services, IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
            bindings = new Dictionary<BaseBinding, bool>();
        }

        public BindingsRegistry Register(BaseBinding baseBinding)
        {
            bindings.Add(baseBinding, false);
            return this;
        }

        public BindingsRegistry RegisterWithConfiguration(BaseBinding baseBinding)
        {
            bindings.Add(baseBinding, true);
            return this;
        }

        public void Bind()
        {
            foreach (var binding in bindings)
            {
                if(binding.Value)
                {
                    binding.Key.Bind(_services, _configuration);
                }
                else
                {
                    binding.Key.Bind(_services);
                }
            }
        }
             
    }
}
