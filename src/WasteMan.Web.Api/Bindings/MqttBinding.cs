using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using WasteMan.MQTT.Wrappers;
using WasteMan.MQTT.Configuration;
using WasteMan.MQTT.Configuration.Builder;

namespace WasteMan.Web.Api.Bindings
{
    public class MqttBinding : BaseBinding
    {
        public override void Bind(IServiceCollection services, IConfiguration configuration = null)
        {
            services.AddSingleton(configuration.GetSection("MQTTnet").Get<MQTTnetConfiguration>());
            services.AddSingleton(new MqttFactory().CreateMqttClient());
            services.AddSingleton<IMQTTnetConfigurationBuilder, MQTTnetConfigurationBuilder>();
            services.AddSingleton(provider =>
                provider
                    .GetService<IMQTTnetConfigurationBuilder>()
                    .Build());
            services.AddSingleton<IMQTTnetWrapper, MQTTnetWrapper>();
        }
    }
}
