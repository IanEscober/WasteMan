using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WasteMan.MQTT.Wrappers;

namespace WasteMan.Web.Api.Startups
{
    public class MQTTStartupFilter : IStartupFilter
    {
        private readonly IServiceProvider _serviceProvider;

        public MQTTStartupFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var mqttContext = scope.ServiceProvider.GetRequiredService<IMQTTnetWrapper>();
                mqttContext.Start();
            }

            return next;
        }
    }
}
