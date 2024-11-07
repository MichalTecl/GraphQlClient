using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MTecl.GraphQlClient.ObjectMapping.Rendering.JsonConvertors
{

    /// <summary>
    /// Forces JsonSerializer to serialize enum member names instead of numeric values.
    /// </summary>
    public class EnumValueConverter : JsonConverterFactory
    {
        private readonly bool _gqlNotation;

        /// <summary>
        /// Creates new instance of EnumValueConverter
        /// </summary>
        /// <param name="gqlNotation">True = values are serialized WITHOUT quotation marks; False = values are serialized as strings</param>
        public EnumValueConverter(bool gqlNotation)
        {
            _gqlNotation = gqlNotation;
        }

        public override bool CanConvert(Type typeToConvert)
        {
            return (Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert).IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return new TheConverter(_gqlNotation);
        }

        private sealed class TheConverter : JsonConverter<object>
        {
            private readonly bool _gqlNotation = false;

            public TheConverter(bool gqlNotation)
            {
                _gqlNotation = gqlNotation;
            }

            public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                Type enumType = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;

                if (!enumType.IsEnum)
                {
                    return JsonSerializer.Deserialize(ref reader, typeToConvert, options);
                }

                if (reader.TokenType == JsonTokenType.Null && Nullable.GetUnderlyingType(typeToConvert) != null)
                {
                    return null;
                }

                try
                {
                    if (reader.TokenType == JsonTokenType.String)
                    {
                        var enumString = reader.GetString();
                        if (enumString == null)
                        {
                            return JsonSerializer.Deserialize(ref reader, typeToConvert, options);
                        }

                        enumString = enumString.ToUpperInvariant().Trim();

                        foreach (var name in Enum.GetNames(enumType))
                        {
                            if (name.ToUpperInvariant() == enumString)
                            {
                                return Enum.Parse(enumType, name);
                            }
                        }
                    }

                    if (reader.TokenType == JsonTokenType.Number)
                    {
                        var intValue = reader.GetInt32();
                        if (Enum.IsDefined(enumType, intValue))
                        {
                            return Enum.ToObject(enumType, intValue);
                        }
                    }
                }
                catch
                {
                    ;
                }

                return JsonSerializer.Deserialize(ref reader, typeToConvert, options);
            }


            public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
            {
                if (value?.GetType().IsEnum != true)
                {
                    JsonSerializer.Serialize(writer, value, value?.GetType() ?? typeof(object), options);
                    return;
                }

                var strValue = value.ToString();

                if (_gqlNotation)
                    writer.WriteStringValue(string.Concat(GqlObjectSerializer.LiteralStringPrefix, strValue));
                else
                    writer.WriteStringValue(strValue);
            }
        }
    }
    
}
