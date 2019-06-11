using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Protocol;
using WasteMan.Common.Data;
using WasteMan.Common.Helpers;
using WasteMan.Redis.Repositories;
using WasteMan.MQTT.Configuration;
using WasteMan.MQTT.Parsers;

namespace WasteMan.MQTT.Wrappers
{
    public class MQTTnetWrapper : IMQTTnetWrapper
    {
        private readonly IMqttClient _mqttClient;
        private readonly IMqttClientOptions _mqttClientOptions;
        private readonly IGarbageBinCacheRepository _garbageBinCacheRepository;
        private readonly List<string> topics;
        
        public MQTTnetWrapper(MQTTnetConfiguration mQTTnetConfiguration, IMqttClient mqttClient, IMqttClientOptions mqttClientOptions, IGarbageBinCacheRepository garbageBinCacheRepository)
        {
            _mqttClient = mqttClient;
            _mqttClientOptions = mqttClientOptions;
            _garbageBinCacheRepository = garbageBinCacheRepository;
            topics = mQTTnetConfiguration.Topics;
        }

        public void OnConnect(Action act) => 
            _mqttClient.Connected += (s, e) =>
                act();

        public void OnDisconnect(Action act) =>
            _mqttClient.Disconnected += (s, e) =>
                act();

        public void OnReceive(Action<GarbageBin> act) =>
            _mqttClient.ApplicationMessageReceived += async (s, e) =>
                act(await DoLinking(e.ToGarbageBin()));
                
        public void Publish(string topic, string payload) =>
            _mqttClient.PublishAsync(topic, payload, MqttQualityOfServiceLevel.ExactlyOnce);

        public void Start()
        {
            Subscribe();
            Receive();
            ReConnect();
            Connect();
        }

        private void Connect() => 
            _mqttClient.ConnectAsync(_mqttClientOptions);

        private void ReConnect() => 
            _mqttClient.Disconnected += (s, e) =>
                _mqttClient.ConnectAsync(_mqttClientOptions);

        private void Subscribe() =>
            _mqttClient.Connected += (s, e) =>
                topics.ForEach(topic => _mqttClient.SubscribeAsync(topic));

        private void Receive() =>
            _mqttClient.ApplicationMessageReceived += (s, e) =>
                _garbageBinCacheRepository.SetAsync(e.ToGarbageBin());
                

        private async Task<GarbageBin> DoLinking(GarbageBin bin)
        {
            var prevBin = await _garbageBinCacheRepository.GetAsync(bin.Name);
            if (prevBin is null)
            {
                return bin;
            }
            return bin.Link(prevBin);
        }
    }
}
