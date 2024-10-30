using FluentAssertions;
using MTecl.GraphQlClient.Exceptions;
using MTecl.GraphQlClient.ObjectMapping.ResponseProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.UnitTests
{
    public class DefaultResponseDeserializerTests
    {
        private const string _countriesValidJson = "{\r\n  \"data\": {\r\n    \"countries\": [\r\n      {\r\n        \"capital\": \"Vienna\",\r\n        \"currency\": \"EUR\"\r\n      },\r\n      {\r\n        \"capital\": \"Berlin\",\r\n        \"currency\": \"EUR\"\r\n      }\r\n    ]\r\n  }\r\n}";
        private const string _errorsJson = "{\r\n  \"errors\": [\r\n    {\r\n      \"message\": \"Cannot query field \\\"blabla\\\" on type \\\"Query\\\".\",\r\n      \"locations\": [\r\n        {\r\n          \"line\": 2,\r\n          \"column\": 3\r\n        }\r\n      ]\r\n    }\r\n  ]\r\n}";

        private readonly DefaultResponseDeserializer _sut = new DefaultResponseDeserializer();

        [Fact]
        public void DeserializesData()
        {
            var deserialized = _sut.DeserializeResponse<List<Country>>(_countriesValidJson, new JsonSerializerOptions());

            deserialized.Should().HaveCount(2);
        }

        [Fact]
        public void ThrowsIfResponseIsErrors()
        {
            Action act = () => _sut.DeserializeResponse<List<Country>>(_errorsJson, new JsonSerializerOptions());
            
            var ex = act.Should().ThrowExactly<ServerErrorResponseException>();
            ex.Subject.Single().Message.Should().Contain("Cannot query field \"blabla\" on type \"Query\"");
        }

        class Country 
        {
            public string capital { get; set; }
            public string currency { get; set; }
        }
        
    }
}
