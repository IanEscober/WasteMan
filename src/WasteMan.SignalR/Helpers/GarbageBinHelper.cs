using Microsoft.AspNetCore.SignalR;
using AutoMapper;
using WasteMan.Common.Data;
using WasteMan.MQTT.Wrappers;
using WasteMan.SignalR.Hubs;

namespace WasteMan.SignalR.Helpers
{
    public class GarbageBinHelper : IGarbageBinHelper
    {   
        private readonly IHubContext<GarbageBinHub> _hubContext;
        private readonly IMQTTnetWrapper _mQTTnetWrapper;

        public GarbageBinHelper(IHubContext<GarbageBinHub> hubContext, IMQTTnetWrapper mQTTnetWrapper)
        {
            _hubContext = hubContext;
            _mQTTnetWrapper = mQTTnetWrapper;
            _mQTTnetWrapper.OnConnect(OnConnectBroker);
            _mQTTnetWrapper.OnDisconnect(OnDisconnectBoker);
            _mQTTnetWrapper.OnReceive(OnReceiveMessage);
            //_mQTTnetWrapper.Start();
        }

        private void OnConnectBroker() =>
            _hubContext.Clients.All.SendAsync(nameof(OnConnectBroker),"Connected To Broker");
        

        private void OnDisconnectBoker() =>
            _hubContext.Clients.All.SendAsync(nameof(OnDisconnectBoker), "Disconnected From Broker");
        

        private void OnReceiveMessage(GarbageBin bin) =>
            _hubContext.Clients.All.SendAsync(nameof(OnReceiveMessage), Mapper.Map<GarbageBinDto>(bin));

        public void PublishMessage(string topic, string payload) =>
            _mQTTnetWrapper.Publish(topic, payload);
    }
}
