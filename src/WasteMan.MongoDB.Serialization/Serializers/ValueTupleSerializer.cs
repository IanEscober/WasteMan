using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace WasteMan.MongoDB.Serialization.Serializers
{
    public class ValueTupleSerializer<T1, T2> : SerializerBase<ValueTuple<T1, T2>>
    {
        private readonly Lazy<IBsonSerializer<T1>> _lazyItem1Serializer;
        private readonly Lazy<IBsonSerializer<T2>> _lazyItem2Serializer;

        public ValueTupleSerializer() : this(BsonSerializer.SerializerRegistry)
        {
        }

        public ValueTupleSerializer(IBsonSerializerRegistry serializerRegistry)
        {
            if (serializerRegistry == null)
            {
                throw new ArgumentNullException("serializerRegistry");
            }

            _lazyItem1Serializer = new Lazy<IBsonSerializer<T1>>(() => serializerRegistry.GetSerializer<T1>());
            _lazyItem2Serializer = new Lazy<IBsonSerializer<T2>>(() => serializerRegistry.GetSerializer<T2>());
        }

        public override (T1, T2) Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            context.Reader.ReadStartArray();
            var item1 = _lazyItem1Serializer.Value.Deserialize(context);
            var item2 = _lazyItem2Serializer.Value.Deserialize(context);
            context.Reader.ReadEndArray();

            return (item1, item2);
        }

        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, (T1, T2) value)
        {
            context.Writer.WriteStartArray();
            _lazyItem1Serializer.Value.Serialize(context, args, value.Item1);
            _lazyItem2Serializer.Value.Serialize(context, args, value.Item2);
            context.Writer.WriteEndArray();
        }
    }
}
