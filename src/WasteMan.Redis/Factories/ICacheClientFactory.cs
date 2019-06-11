using StackExchange.Redis.Extensions.Core;

namespace WasteMan.Redis.Factories
{
    public interface ICacheClientFactory
    {
        ICacheClient Create();
    }
}