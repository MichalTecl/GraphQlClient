using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Globalization;
using System.Collections.Generic;

namespace MTecl.GraphQlClient.ObjectMapping.Rendering.JsonConvertors
{
    public class DateTimeConverter : JsonConverter<DateTime>
    {
        private readonly GraphQlQueryBuilder _builder;

        public DateTimeConverter(GraphQlQueryBuilder builder)
        {
            _builder = builder;
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return _builder.DateTimeConversionMode.Read(ref reader, options);            
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            _builder.DateTimeConversionMode.Write(writer, value, options);
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
            private readonly string[] _alternativeFormats;

            public StringDateTimeConversion(string format, CultureInfo culture, string[] alternativeFormats)
            {
                _format = format;
                _cultureInfo = culture ?? CultureInfo.InvariantCulture;
                _alternativeFormats = alternativeFormats;
            }

            public DateTime Read(ref Utf8JsonReader reader, JsonSerializerOptions options)
            {
                var dtString = reader.GetString();

                foreach(var f in Formats)
                    if (DateTime.TryParseExact(dtString, f, _cultureInfo, DateTimeStyles.None, out var parsed))
                        return parsed;

                throw new ArgumentException($"Cannot parse '{dtString}' as DateTime ");
            }

            public void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString(_format, _cultureInfo));
            }

            private IEnumerable<string> Formats
            {
                get
                {
                    yield return _format;

                    if (_alternativeFormats != null)
                        foreach(var f in _alternativeFormats)
                            yield return f;
                }
            }
        }
        #endregion

        public static IDateTimeConversionMode StringConversionMode(string format, CultureInfo culture = null, string[] alternativeFormats = null)
        {
            return new StringDateTimeConversion(format, culture, alternativeFormats);
        }
    }
}
