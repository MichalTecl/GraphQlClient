using FluentAssertions;
using MTecl.GraphQlClient.ObjectMapping;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.IntegrationTests
{
    public class QueryMappingWithVariablesTests
    {
        [Fact]
        public void TestPassMethod()
        {
            var query = QueryMapper.MapQuery<IFlowerQuery, IEnumerable<Flower>>(f => f.GetFlowers(QueryVariable.Pass<string>("$v1"), QueryVariable.Pass<bool>("$v2")));

            var methodNode = query.FindChild(nameof(IFlowerQuery.GetFlowers));
            methodNode.Arguments.Should().HaveCount(2);
            methodNode.Arguments[0].Value.ToString().Should().Be("$v1");
            methodNode.Arguments[1].Value.ToString().Should().Be("$v2");

            var rendered = methodNode.ToString();
            rendered.Should().Contain("(namePattern: $v1, needsWatering: $v2)");
        }

        [Fact]
        public void TestTwoUsagesOfSameVar()
        {
            var query = QueryMapper.MapQuery<IFlowerQuery, Flower>(f => f.FindFlower(QueryVariable.Pass<int>("$color"), QueryVariable.Pass<int>("$color")));

            query.QueryVariables.Count.Should().Be(1);
        }

        interface IFlowerQuery
        {
            IEnumerable<Flower> GetFlowers(string namePattern, bool? needsWatering);

            Flower FindFlower(int summerColor, int winterColor);
        }

        class Flower
        {
            public int Color { get; set; }
            public bool NeedsWatering { get; set; }
            public string Name { get; set; }
        }
    }
}
