using MongoDB.Bson.Serialization.Conventions;

namespace WasteMan.MongoDB.Serialization.Conventions
{
    public static class DictionaryConventionRegistry
    {
        public static void Register(DictionaryConvention convention)
        {
            ConventionRegistry.Register("DictionaryConvention", new ConventionPack() { convention }, _ => true);
        }
    }
}
