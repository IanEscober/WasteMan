using System.Collections.Generic;
using WasteMan.MQTT.Configuration.Elements;

namespace WasteMan.MQTT.Configuration
{
    public class MQTTnetConfiguration
    {
        public string ClientID { get; set; }
        public bool CleanSession { get; set; }
        public CredentialElement Credentials { get; set; }
        public ServerElement Servers { get; set; }
        public List<string> Topics { get; set; }
    }
}
