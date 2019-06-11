using MongoDB.Bson.Serialization.Conventions;

namespace WasteMan.MongoDB.Serialization.Conventions
{
    public static class MappingConventionRegistry
    {
        public static void Register(MappingConvention mapping)
        {
            ConventionRegistry.Register("MappingConvention", new ConventionPack { mapping }, _ => true);
        }
    }
}
