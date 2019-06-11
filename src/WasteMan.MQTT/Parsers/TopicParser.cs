using MQTTnet;
using WasteMan.MQTT.Data.Enums;

namespace WasteMan.MQTT.Parsers
{
    internal static class TopicParser
    {
        public static (string Name, string Sensor) ToTopic(this MqttApplicationMessage message)
        {
            var topic = message.Topic.Split('/');
            return (topic[(int)Topic.Name], topic[(int)Topic.Sensor]);
        }
    }
}
