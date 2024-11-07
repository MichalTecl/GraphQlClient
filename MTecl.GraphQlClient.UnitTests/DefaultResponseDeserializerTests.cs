using FluentAssertions;
using MTecl.GraphQlClient.Exceptions;
using MTecl.GraphQlClient.ObjectMapping.ResponseProcessing;

namespace MTecl.GraphQlClient.UnitTests
{
    public class DefaultResponseDeserializerTests
    {
        private static readonly GraphQlQueryBuilder _builder = GraphQlQueryBuilder.Create();

        private const string _countriesValidJson = "{\r\n  \"data\": {\r\n    \"countries\": [\r\n      {\r\n        \"capital\": \"Vienna\",\r\n        \"currency\": \"EUR\"\r\n      },\r\n      {\r\n        \"capital\": \"Berlin\",\r\n        \"currency\": \"EUR\"\r\n      }\r\n    ]\r\n  }\r\n}";
        private const string _errorsJson = "{\r\n  \"errors\": [\r\n    {\r\n      \"message\": \"Cannot query field \\\"blabla\\\" on type \\\"Query\\\".\",\r\n      \"locations\": [\r\n        {\r\n          \"line\": 2,\r\n          \"column\": 3\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}";

        private readonly DefaultResponseDeserializer _sut = new DefaultResponseDeserializer();

        [Fact]
        public void DeserializesData()
        {
            var deserialized = _sut.DeserializeResponse<List<Country>>(_countriesValidJson, _builder);

            deserialized.Should().HaveCount(2);
        }

        [Fact]
        public void ThrowsIfResponseIsErrors()
        {
            Action act = () => _sut.DeserializeResponse<List<Country>>(_errorsJson, _builder);
            
            var ex = act.Should().ThrowExactly<ServerErrorResponseException>();
            ex.Subject.Single().Message.Should().Contain("Cannot query field \"blabla\" on type \"Query\"");
        }

        [Fact]
        public void TestEnumParsing()
        {
            var countryJson = $"{{ \"data\": {{ \"c\": {{ \"countryType\":\"{nameof(CountryType.TypeB)}\" }}}}}}";

            var deser = _sut.DeserializeResponse<Country>(countryJson, _builder);

            deser.CountryType.Should().NotBeNull();
            deser.CountryType.Should().Be(CountryType.TypeB);
        }

        class Country 
        {
            public string capital { get; set; }
            public string currency { get; set; }

            public CountryType? CountryType { get; set; }
        }

        public enum CountryType 
        {
            TypeA,TypeB
        }
    }
}
