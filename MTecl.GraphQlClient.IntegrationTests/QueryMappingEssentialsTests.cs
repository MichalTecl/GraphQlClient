using FluentAssertions;
using MTecl.GraphQlClient.ObjectMapping;
using MTecl.GraphQlClient.ObjectMapping.Descriptors;

namespace MTecl.GraphQlClient.IntegrationTests
{
    public class QueryMappingEssentialsTests
    {
        [Fact]
        public void MethodWithoutParametersIsProjectedAsField()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals());

            var methodNode = query.FindChild("animals");
            methodNode.Should().NotBeNull();
            methodNode.Arguments.Should().HaveCount(0);

            methodNode.ToString().Trim().Should().StartWith("animals {");
        }

        [Fact]
        public void MethodWithoutParametersIsProjectedAsFieldWithArguments()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.FindAnimals("eleph", 10, 1000));

            var methodNode = query.FindChild("animals");
            methodNode.Should().NotBeNull();

            methodNode.Arguments.Should().HaveCount(3);
            methodNode.Arguments.Should().Contain(a => a.Key == "query" && a.Value.Equals("eleph"));
            methodNode.Arguments.Should().Contain(a => a.Key == "page" && a.Value.Equals(10));
            methodNode.Arguments.Should().Contain(a => a.Key == "page_size" && a.Value.Equals(1000));

            methodNode.ToString().Trim().Should().StartWith("animals(");
        }

        [Fact]
        public void MethodReturnTypeProjectedAsComplexType()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals());

            var methodNode = query.FindChild("animals");

            methodNode.Should().NotBeNull();

            methodNode.FindChild("name").Should().NotBeNull();
            methodNode.FindChild("number_of_legs").Should().NotBeNull();
        }

        [Fact]
        public void PrimitiveTypeFieldsCanBeExcludedByAttribute()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals());

            var methodNode = query.FindChild("animals");
            methodNode.Should().NotBeNull();

            methodNode.FindChild("id").Should().BeNull();
        }

        [Fact]
        public void ExcludedFieldsCanBeIncludedByWith()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals().With(a => a.Id));

            var methodNode = query.FindChild("animals");
            methodNode.Should().NotBeNull();

            methodNode.FindChild("id").Should().NotBeNull();
        }

        [Fact]
        public void MultipleExcludedFieldsCanBeIncludedByWith()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals().With(a => a.Id, a => a.InsertUserId));

            var methodNode = query.FindChild("animals");
            methodNode.Should().NotBeNull();

            methodNode.FindChild("id").Should().NotBeNull();
            methodNode.FindChild("insert_user_id").Should().NotBeNull();
        }

        [Fact]
        public void ComplexTypeFieldsAreByDefaultExcluded()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals());

            var methodNode = query.FindChild("animals");
            methodNode.Should().NotBeNull();

            methodNode.FindChild("territories").Should().BeNull();
        }

        [Fact]
        public void ComplexTypeFieldsCanBeIncludedByWith()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals().With(a => a.Territories));

            var methodNode = query.FindChildPath("animals", "territories", "name").Should().NotBeNull();
        }

        [Fact]
        public void WithInsideOfWith()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals().With(a => a.Territories.With(t => t.Id)));

            query.FindChildPath("animals", "territories", "id").Should().NotBeNull();
        }

        [Fact]
        public void WithInsideOfWith2()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals().With(a => a.DimensionsWhenBorn.With(d => d.InformationSource)));

            query.FindChildPath("animals", "dimensions_when_born", "information_source").Should().NotBeNull();
        }

        [Fact]
        public void WithInclusionsOfPropertiesCanBeChained()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals().With(a => a.DimensionsWhenBorn.InformationSource));

            query.FindChildPath("animals", "dimensions_when_born", "information_source").Should().NotBeNull();
        }

        [Fact]
        public void WithInclusionsOfPropertiesCanBeChained2()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals().With(a => a.FindTerritory("filter").Id));

            query.FindChildPath("animals", "territory", "id").Should().NotBeNull();
        }

        [Fact]
        public void IncludedFieldsShouldNotHaveSubfields()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals().With(a => a.Name));
            var nameNode = query.FindChildPath("animals", "name");

            nameNode.Should().NotBeNull();

            nameNode.Nodes.Count.Should().Be(0);
        }

        [Fact]
        public void ComplexTypeFieldsCanBeIncludedByAttribute()
        {
            var query = QueryMapper.MapQuery<AnimalsQuery, IEnumerable<Animal>>(q => q.GetAnimals());

            var methodNode = query.FindChild("animals");
            methodNode.Should().NotBeNull();

            var dimensionsNode = methodNode.FindChild("dimensions");
            dimensionsNode.Should().NotBeNull();

            dimensionsNode.FindChild("weight").Should().NotBeNull();
            dimensionsNode.FindChild("height").Should().NotBeNull();
        }
                
        class AnimalsQuery
        {
            [Gql("animals")]
            public List<Animal> Animals { get; set; }

            [Gql("animals")]
            public IEnumerable<Animal> GetAnimals() { yield break; }

            [Gql("animals")]
            public IEnumerable<Animal> FindAnimals(string query, int page, [Gql("page_size")] int pageSize) { yield break; }
        }

        class Animal
        {
            [Gql("id", FieldInclusionMode.Exclude)]
            public int Id { get; set; }

            [Gql("insert_user_id", FieldInclusionMode.Exclude)]
            public int InsertUserId { get; set; }

            [Gql("name")]
            public string Name { get; set; }

            [Gql("number_of_legs")]
            public int NumberOfLegs { get; set; }

            [Gql("territories")]
            public List<Territory> Territories { get; set; }

            [Gql("territory")]
            public Territory FindTerritory(string filter) => default!;

            [Gql("dimensions", FieldInclusionMode.Include)]
            public PhysicalDimensions PhysicalDimensions { get; set; }

            [Gql("dimensions_when_born")]
            public PhysicalDimensions DimensionsWhenBorn { get; set; }
        }
        class Territory
        {
            [Gql("name")]
            public string Name { get; set; }

            [Gql("id", FieldInclusionMode.Exclude)]
            public int Id { get; set; }
        }   
        
        class PhysicalDimensions
        {
            [Gql("weight")]
            public decimal Weight { get; set; }

            [Gql("height")]
            public decimal Height { get; set; }

            [Gql("information_source", FieldInclusionMode.Exclude)]
            public string InformationSource { get; set; }
        }        
    }
}