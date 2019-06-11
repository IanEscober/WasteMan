
namespace WasteMan.SignalR.Helpers
{
    public interface IGarbageBinHelper
    {
        void PublishMessage(string topic, string payload);
    }
}
