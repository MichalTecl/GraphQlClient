using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MTecl.GraphQlClient.ObjectMapping.Rendering
{
    public class RenderOptions
    {
        public static readonly RenderOptions Default = new RenderOptions()
        {
            JsonSerializerOptions = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = true,
                AllowTrailingCommas = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                Converters = { new CustomDateTimeConverter("yyyy-MM-dd HH:mm:ss") }
            },

            
        };

        public RenderOptions()
        {
            InputObjectSerializer = new GqlObjectSerializer(this);
        }

        public JsonSerializerOptions JsonSerializerOptions { get; set; }

        public bool ConvertFieldNamesToCamelCase { get; set; } = true;

        public IInputObjectSerializer InputObjectSerializer { get; set; }
    }
}
