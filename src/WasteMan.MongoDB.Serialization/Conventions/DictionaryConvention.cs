using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Options;

namespace WasteMan.MongoDB.Serialization.Conventions
{
    public class DictionaryConvention : IMemberMapConvention
    {
        public string Name => "DictionaryConvention";

        public void Apply(BsonMemberMap memberMap)
        {
            memberMap.SetSerializer(ConfigureSerializer(memberMap.GetSerializer()));
        }

        private IBsonSerializer ConfigureSerializer(IBsonSerializer serializer)
        {
            var dictionaryRepresentationConfigurable = serializer as IDictionaryRepresentationConfigurable;
            if (dictionaryRepresentationConfigurable != null)
            {
                serializer = dictionaryRepresentationConfigurable.WithDictionaryRepresentation(DictionaryRepresentation.ArrayOfArrays);
            }

            return serializer;
        }
    }
}
