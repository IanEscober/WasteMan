using System;
using WasteMan.Common.Data;

namespace WasteMan.MQTT.Wrappers
{
    public interface IMQTTnetWrapper
    {
        void OnConnect(Action act);
        void OnDisconnect(Action act);
        void OnReceive(Action<GarbageBin> act);
        void Start();
        void Publish(string topic, string payload);
    }
}