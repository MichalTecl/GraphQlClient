using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Globalization;

namespace MTecl.GraphQlClient.ObjectMapping.Rendering.JsonConvertors
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {        
        public IDateTimeConversionMode Mode { get; set; }
             
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (Mode == null) 
            {
                return (DateTime)JsonSerializer.Deserialize(ref reader, typeToConvert, options);
            }

            return Mode.Read(ref reader, options);
            
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            if (Mode == null)
            {
                JsonSerializer.Serialize(writer, value, typeof(DateTime), options);
                return;
            }

            Mode.Write(writer, value, options);
        }

        public interface IDateTimeConversionMode
        {
            DateTime Read(ref Utf8JsonReader reader, JsonSerializerOptions options);
            void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options);
        }

        #region Default Modes
        private sealed class StringDateTimeConversion : IDateTimeConversionMode
        {
            private readonly string _format;
            private readonly CultureInfo _cultureInfo;

            public StringDateTimeConversion(string format, CultureInfo culture)
            {
                _format = format;
                _cultureInfo = culture ?? CultureInfo.InvariantCulture;

            }

            public DateTime Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
            {
                return DateTime.ParseExact(reader.GetString(), _format, _cultureInfo);
            }

            public void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(_format, _cultureInfo));
            }
        }
        #endregion

        public static IDateTimeConversionMode StringConversionMode(string format, CultureInfo culture = null)
        {
            return new StringDateTimeConversion(format, culture);
        }
    }
}
