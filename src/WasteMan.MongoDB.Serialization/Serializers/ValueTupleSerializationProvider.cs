using System;
using System.Reflection;
using MongoDB.Bson.Serialization;

namespace WasteMan.MongoDB.Serialization.Serializers
{
    public class ValueTupleSerializationProvider : IBsonSerializationProvider
    {
        public IBsonSerializer GetSerializer(Type type)
        {
            if (type.IsGenericType && !type.ContainsGenericParameters)
            {
                if (type.GetGenericTypeDefinition() == typeof(ValueTuple<,>))
                {
                    var valueTupleSerializer = typeof(ValueTupleSerializer<,>).MakeGenericType(type.GenericTypeArguments);
                    return GetValueTupleSerializer(valueTupleSerializer);
                }
            }

            return null;
        }

        private static IBsonSerializer GetValueTupleSerializer(Type valueTupleSerializer)
        {
            var valueTupleSerializerInfo = valueTupleSerializer.GetTypeInfo();

            var valueTupleSerializerConstructor = valueTupleSerializerInfo.GetConstructor(new[] { typeof(IBsonSerializerRegistry) });
            if (valueTupleSerializerConstructor != null)
            {
                return valueTupleSerializerConstructor.Invoke(new object[] { BsonSerializer.SerializerRegistry }) as IBsonSerializer;
            }

            valueTupleSerializerConstructor = valueTupleSerializerInfo.GetConstructor(new Type[0]);
            if (valueTupleSerializerConstructor != null)
            {
                return valueTupleSerializerConstructor.Invoke(new object[0]) as IBsonSerializer;
            }

            throw new MissingMethodException("No constructor found for ValueTuple serializer");
        }
    }
}
