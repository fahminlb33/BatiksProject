using System;
using System.IO;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProtoBuf;

namespace BatiksProject.Infrastructure
{
    public class FloatArrayToStringConverter : ValueConverter<float[], string>
    {
        private static Expression<Func<float[], string>> convertToProviderExpression = x => FloatArrayToString(x);
        private static Expression<Func<string, float[]>> convertFromProviderExpression = x => StringToFloatArray(x);

        public FloatArrayToStringConverter(ConverterMappingHints mappingHints = null)
            : base(convertToProviderExpression, convertFromProviderExpression, mappingHints)
        {
        }

        private static string FloatArrayToString(float[] arr)
        {
            using var ms = new MemoryStream();
            var data = new ProtoFloatContract{Data = arr};
            Serializer.Serialize(ms, data);

            return Convert.ToBase64String(ms.GetBuffer(), 0, (int) ms.Length);
        }

        private static float[] StringToFloatArray(string str)
        {
            var bytes = Convert.FromBase64String(str);
            using var ms = new MemoryStream(bytes);

            return Serializer.Deserialize<ProtoFloatContract>(ms).Data;
        }

        [ProtoContract]
        public class ProtoFloatContract
        {
            [ProtoMember(1)]
            public float[] Data { get; set; }
        }
    }
}
