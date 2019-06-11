using Microsoft.AspNetCore.SignalR;
using WasteMan.SignalR.Helpers;

namespace WasteMan.SignalR.Hubs
{
    public class GarbageBinHub : Hub
    {
        private readonly IGarbageBinHelper _garbageBinHelper;
        public GarbageBinHub(IGarbageBinHelper garbageBinHelper)
        {
            _garbageBinHelper = garbageBinHelper;
        }

        public void Publisher(string topic, string payload) =>
            _garbageBinHelper.PublishMessage(topic, payload);
    }
}
