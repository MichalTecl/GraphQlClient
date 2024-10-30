using MTecl.GraphQlClient.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.IntegrationTests
{
    public class QueryMappingWithInputTypesTests
    {
        [Fact]
        public void TestComplexTypeArgumentSerialization()
        {
            var query = QueryMapper.MapQuery<Query, string>(q => q.Get(new InputType { PropA = "hi", PropB = 42 }));

            var rendered = query.ToString();

        }

        public class Query
        {
            public string Get(InputType t) => null;
        }

        public class InputType
        {
            public string PropA { get; set; }
            public int PropB { get; set; }
        }
    }
}
