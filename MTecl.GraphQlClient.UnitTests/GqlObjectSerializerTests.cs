using MTecl.GraphQlClient.ObjectMapping.Rendering;

namespace MTecl.GraphQlClient.UnitTests
{
    public class GqlObjectSerializerTests
    {
        private readonly GqlObjectSerializer _sut = new GqlObjectSerializer();

        [Fact]
        public void Serialize_ShouldConvertSimpleObjectToGraphQLInput()
        {
            // Arrange
            var obj = new
            {
                name = "John",
                age = 30,
                isMember = true
            };

            // Act
            string result = _sut.Serialize(obj);

            // Assert
            Assert.Equal("{ name: \"John\", age: 30, isMember: true }", result);
        }

        [Fact]
        public void Serialize_ShouldConvertNestedObjectToGraphQLInput()
        {
            // Arrange
            var obj = new
            {
                name = "John",
                address = new
                {
                    street = "123 Main St",
                    city = "New York"
                }
            };

            // Act
            string result = _sut.Serialize(obj);

            // Assert
            Assert.Equal("{ name: \"John\", address: { street: \"123 Main St\", city: \"New York\" } }", result);
        }

        [Fact]
        public void Serialize_ShouldConvertArrayToGraphQLInput()
        {
            // Arrange
            var obj = new
            {
                items = new[] { "apple", "banana", "cherry" }
            };

            // Act
            string result = _sut.Serialize(obj);

            // Assert
            Assert.Equal("{ items: [ \"apple\", \"banana\", \"cherry\" ] }", result);
        }

        [Fact]
        public void Serialize_ShouldHandleNullValues()
        {
            // Arrange
            var obj = new
            {
                name = (string)null,
                age = 30
            };

            // Act
            string result = _sut.Serialize(obj);

            // Assert
            Assert.Equal("{ name: null, age: 30 }", result);
        }

        [Fact]
        public void Serialize_ShouldHandleEmptyObject()
        {
            // Arrange
            var obj = new { };

            // Act
            string result = _sut.Serialize(obj);

            // Assert
            Assert.Equal("{  }", result);
        }

        [Fact]
        public void Serialize_ShouldConvertDictionaryToGraphQLInput()
        {
            // Arrange
            var dictionary = new Dictionary<string, object>
            {
                { "name", "Alice" },
                { "age", 28 },
                { "isMember", true },
                { "address", new Dictionary<string, object>
                    {
                        { "street", "456 Maple Rd" },
                        { "city", "Los Angeles" }
                    }
                }
            };

            // Act
            string result = _sut.Serialize(dictionary);

            // Assert
            Assert.Equal("{ name: \"Alice\", age: 28, isMember: true, address: { street: \"456 Maple Rd\", city: \"Los Angeles\" } }", result);
        }
    }
}
