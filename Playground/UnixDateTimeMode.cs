using MTecl.GraphQlClient.ObjectMapping.Rendering.JsonConvertors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Playground
{
    internal class UnixDateTimeMode : DateTimeConverter.IDateTimeConversionMode
    {
        public DateTime Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
         => DateTimeOffset.FromUnixTimeSeconds(reader.GetInt64()).UtcDateTime;

        public void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
          => writer.WriteNumberValue(new DateTimeOffset(value).ToUnixTimeSeconds());
    }
}
