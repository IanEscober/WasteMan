using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using WasteMan.MongoDB.Serialization.Conventions;
using WasteMan.MongoDB.Serialization.Serializers;

namespace WasteMan.Web.Api.Registrations
{
    public static class MongoDBRegistry
    {
        public static void Register()
        {
            DictionaryConventionRegistry.Register(new DictionaryConvention());
            MappingConventionRegistry.Register(new MappingConvention());
            BsonSerializer.RegisterSerializationProvider(new ValueTupleSerializationProvider());
            BsonSerializer.RegisterSerializer(DateTimeSerializer.LocalInstance);
        }
    }
}
