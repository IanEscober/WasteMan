using MQTTnet.Client;

namespace WasteMan.MQTT.Configuration.Builder
{
    public interface IMQTTnetConfigurationBuilder
    {
        IMqttClientOptions Build();
    }
}