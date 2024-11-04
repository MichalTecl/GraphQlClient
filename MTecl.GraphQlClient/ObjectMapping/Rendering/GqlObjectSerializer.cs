using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MTecl.GraphQlClient.ObjectMapping.Rendering
{
    internal class GqlObjectSerializer : IInputObjectSerializer
    {
        private readonly RenderOptions _renderOptions;

        public GqlObjectSerializer(RenderOptions renderOptions)
        {
            _renderOptions = renderOptions;
        }

        public string Serialize(object o)
        {
            var jDocument = JsonSerializer.SerializeToDocument(o, _renderOptions.JsonSerializerOptions);

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
                return $"\"{element.GetString()}\"";
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
