using System;
using System.Collections.Generic;
using System.Text;
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
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            }
        };

        public JsonSerializerOptions JsonSerializerOptions { get; set; }

        public bool ConvertFieldNamesToCamelCase { get; set; } = true;
    }
}
