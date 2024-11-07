using MTecl.GraphQlClient.ObjectMapping.Rendering.JsonConvertors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace MTecl.GraphQlClient.ObjectMapping.Rendering
{
    internal static class GqlObjectSerializer 
    {
        internal const string LiteralStringPrefix = "###__LITERAL_TO_UNWRAP_IN_GQL_NOTATION__##:";
               
        private static readonly EnumValueConverter _gqlNotationEnumConverter = new EnumValueConverter(true);

        public static string Serialize(object o, GraphQlQueryBuilder builder)
        {
            var jsonSerializerOptions = builder.JsonSerializerOptions;

            // We have to clone options to pass special convertor for enums. But it's only happening during compilation (when input object is passed as a part of query), so it's ok
            jsonSerializerOptions = new JsonSerializerOptions(builder.JsonSerializerOptions);
            var existingEnumConverter = jsonSerializerOptions.Converters.FirstOrDefault(c => c.GetType() == typeof(EnumValueConverter));
            if (existingEnumConverter != null)
            {
                jsonSerializerOptions.Converters.Remove(existingEnumConverter);
            }

            jsonSerializerOptions.Converters.Add(_gqlNotationEnumConverter);

            var jDocument = JsonSerializer.SerializeToDocument(o, jsonSerializerOptions);

            return SerializeNode(jDocument.RootElement);
        }

        private static string SerializeNode(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                return SerializeObject(element);
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                return SerializeArray(element);
            }
            else if (element.ValueKind == JsonValueKind.String)
            {
                var originalStr = element.GetString();
                if (originalStr?.StartsWith(LiteralStringPrefix) == true)
                    return originalStr.Substring(LiteralStringPrefix.Length);

                return $"\"{originalStr}\"";
            }
            else if (element.ValueKind == JsonValueKind.Number)
            {
                return element.GetRawText();
            }
            else if (element.ValueKind == JsonValueKind.True)
            {
                return "true";
            }
            else if (element.ValueKind == JsonValueKind.False)
            {
                return "false";
            }
            else if (element.ValueKind == JsonValueKind.Null)
            {
                return "null";
            }
            else
            {
                throw new NotSupportedException($"Unsupported JsonValueKind: {element.ValueKind}");
            }
        }

        private static string SerializeObject(JsonElement element)
        {
            var properties = new List<string>();
            foreach (var property in element.EnumerateObject())
            {
                string name = property.Name;
                string value = SerializeNode(property.Value);
                properties.Add($"{name}: {value}");
            }
            return $"{{ {string.Join(", ", properties)} }}";
        }

        private static string SerializeArray(JsonElement element)
        {
            var items = new List<string>();
            foreach (var item in element.EnumerateArray())
            {
                items.Add(SerializeNode(item));
            }
            return $"[ {string.Join(", ", items)} ]";
        }
    }
}
