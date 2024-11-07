using MTecl.GraphQlClient.ObjectMapping.Rendering;

namespace MTecl.GraphQlClient.UnitTests
{
    public class GqlObjectSerializerTests
    {
        private readonly GraphQlQueryBuilder _builder = GraphQlQueryBuilder.Create();

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
            string result = GqlObjectSerializer.Serialize(obj, _builder);

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
            string result = GqlObjectSerializer.Serialize(obj, _builder);

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
            string result = GqlObjectSerializer.Serialize(obj, _builder);

            // Assert
            Assert.Equal("{ items: [ \"apple\", \"banana\", \"cherry\" ] }", result);
        }
                
        [Fact]
        public void Serialize_ShouldHandleEmptyObject()
        {
            // Arrange
            var obj = new { };

            // Act
            string result = GqlObjectSerializer.Serialize(obj, _builder);

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
            string result = GqlObjectSerializer.Serialize(dictionary, _builder);

            // Assert
            Assert.Equal("{ name: \"Alice\", age: 28, isMember: true, address: { street: \"456 Maple Rd\", city: \"Los Angeles\" } }", result);
        }

        [Fact]
        public void Serialize_ShouldSkipNullProperties()
        {
            // Arrange
            var obj = new
            {
                name = "Alice",
                age = (int?)null,
                isMember = true,
                address = (string)null
            };

            // Act
            string result = GqlObjectSerializer.Serialize(obj, _builder);

            // Assert
            Assert.Equal("{ name: \"Alice\", isMember: true }", result);
        }

        [Fact]
        public void Enum_ShouldBeSerializedAsValueName()
        {
            var obj = new { e = Enum1.ValueA, x = new { f = Enum1.ValueB }};
            
            var result = GqlObjectSerializer.Serialize(obj, _builder);

            Assert.Equal($"{{ e: {nameof(Enum1.ValueA)}, x: {{ f: {nameof(Enum1.ValueB)} }} }}", result);
        }

        public enum Enum1
        {
            ValueA, ValueB
        }
    }
}
