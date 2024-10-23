using MTecl.GraphQlClient.ObjectMapping;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables;
using MTecl.GraphQlClient.UnitTests.TestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.UnitTests
{
    public class QueryPlayground
    {
        [Fact]
        public void Parse() 
        {
            var queryVariable1 = new QueryVariable<string>("$var1");
            var queryVariable2 = new QueryVariable<string>("$var2");

            var result = QueryMapper.MapQuery<IQuery>(q => q.GetPublishers(queryVariable1.Value, 10, "nazdar")                                                    
                                                                       .With(p => p.GetPublishedBooks(queryVariable2.Value)
                                                                            .With(b => b.Dimensions.With(d => d.CoverType))));

            var t = result.ToString();



            Assert.NotNull(result);
        }
    }
}
