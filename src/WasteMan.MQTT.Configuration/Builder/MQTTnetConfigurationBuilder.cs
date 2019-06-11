using MQTTnet.Client;

namespace WasteMan.MQTT.Configuration.Builder
{
    public class MQTTnetConfigurationBuilder : IMQTTnetConfigurationBuilder
    {
        private readonly MQTTnetConfiguration _configuration;
        public MQTTnetConfigurationBuilder(MQTTnetConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IMqttClientOptions Build()
        {
            return new MqttClientOptionsBuilder()
                .WithCleanSession(_configuration.CleanSession)
                .WithClientId(_configuration.ClientID)
                .WithCredentials(_configuration.Credentials.Username, _configuration.Credentials.Password)
                .WithTcpServer(_configuration.Servers.Server, _configuration.Servers.Port)
                .Build();
        }
    }
}
