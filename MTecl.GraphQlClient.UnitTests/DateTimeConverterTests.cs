using FluentAssertions;
using MTecl.GraphQlClient.ObjectMapping.Rendering.JsonConvertors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static MTecl.GraphQlClient.ObjectMapping.Rendering.JsonConvertors.DateTimeConverter;

namespace MTecl.GraphQlClient.UnitTests
{
    public class DateTimeConverterTests
    {
        [Fact]
        public void ConvertesThereAndBack()
        {
            var m1 = new Model { Dt = DateTime.Now };

            var options = GetOptions(DateTimeConverter.StringConversionMode("yyyyMMdd HHmmss"));

            var json = JsonSerializer.Serialize(m1, options);

            var deserialized = JsonSerializer.Deserialize<Model>(json, options);

            deserialized.Dt.Should().BeCloseTo(m1.Dt, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void UsesAlternativeFormats()
        {
            var m1 = new Model { Dt = DateTime.Now };
                        
            var json = JsonSerializer.Serialize(m1, GetOptions(DateTimeConverter.StringConversionMode("yyyyMMdd")));

            var deserialized = JsonSerializer.Deserialize<Model>(json, GetOptions(
                DateTimeConverter.StringConversionMode("yyyyMMdd  HHmmss", alternativeFormats: ["HHmmss", "yyyyMMdd"])
                ));

            deserialized.Dt.Date.Should().Be(m1.Dt.Date);
        }

        class Model
        {
            public DateTime Dt { get; set; }
        }

        private static JsonSerializerOptions GetOptions(IDateTimeConversionMode mode)
        {
            var builder = GraphQlQueryBuilder.Create();

            builder.DateTimeConversionMode = mode;

            return builder.JsonSerializerOptions;
        }
    }
}
