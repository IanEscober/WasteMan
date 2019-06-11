using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;

namespace WasteMan.MongoDB.Serialization.Conventions
{
    public class MappingConvention : IClassMapConvention
    {
        public string Name => "MappingConvention";

        public void Apply(BsonClassMap classMap)
        {
            classMap.SetIgnoreExtraElements(true);
        }
    }
}
