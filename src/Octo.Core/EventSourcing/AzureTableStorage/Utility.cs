using System;
using System.IO;
using System.Text;

namespace Octo.Core.EventSourcing.AzureTableStorage
{
    public static class Utility
    {
        public static object DeJsonize(string data, Type type)
        {
            return new JsonSerializer().Deserialize(new StringReader(data), type);
        }

        public static object DeJsonize(string data, string assemblyQualifiedTypeName)
        {
            Type parsedType = Type.GetType(assemblyQualifiedTypeName, true, true);
            return DeJsonize(data, parsedType);
        }

        public static string Jsonize(object data, Type type)
        {
            StringBuilder result = new StringBuilder();
            new JsonSerializer().Serialize(new StringWriter(result), data);
            return result.ToString();
        }

        public static string Jsonize(object data, string assemblyQualifiedTypeName)
        {
            Type parsedType = Type.GetType(assemblyQualifiedTypeName, true, true);
            return Jsonize(data, parsedType);
        }
    }
}