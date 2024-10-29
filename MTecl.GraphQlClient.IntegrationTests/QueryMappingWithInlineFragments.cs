using FluentAssertions;
using MTecl.GraphQlClient.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.IntegrationTests
{
    public class QueryMappingWithInlineFragments
    {
        [Fact]
        public void TypeCompositionsMappedAsFragments()
        {
            var query = QueryMapper.MapQuery<ILocosQuery>(q => q.GetLocomotives());

            query.Should().NotBeNull();

            query.Field("GetLocomotives").Field("Name").Should().NotBeNull();

            query.Field("GetLocomotives").Fragment("DIESEL").Field("TankCapacity").Should().NotBeNull();
            query.Field("GetLocomotives").Fragment("ELECTRIC").Field("Voltage").Should().NotBeNull();
        }

        
        interface ILocosQuery
        {
            IEnumerable<Locomotive> GetLocomotives();
        }

        [GqlTypeFragment("DIESEL")]
        interface IDieselLoco
        {
            int TankCapacity { get; set; }
        }

        [GqlTypeFragment("ELECTRIC")]
        interface IElectricLoco 
        {
            int Voltage { get; set; }
        }

        class Locomotive : IElectricLoco, IDieselLoco
        {
            public int Voltage { get; set; }
            public int TankCapacity { get; set; }
            public string Name { get; set; }
        }
    }
}
