using FluentAssertions;
using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.UnitTests
{
    public class GqlMemberHelperTests
    {
        [Fact]
        public void AllPropertiesAreMapped()
        {
            var members = GqlMemberHelper.MapMembers<PropertyInfo>(typeof(C1));
            members.Should().Contain(m => m.Member.Name == nameof(C1.Property1) && m.Attribute.Name == "testName");
            members.Should().Contain(m => m.Member.Name == nameof(C1.NotMarkedProperty) 
                                       && m.Attribute.Name == nameof(C1.NotMarkedProperty)
                                       && m.Attribute.InclusionMode == FieldInclusionMode.Default);
        }

        [Theory]
        [InlineData(typeof(List<C1>), typeof(C1))]
        [InlineData(typeof(IEnumerable<C1>), typeof(C1))]
        [InlineData(typeof(C1[]), typeof(C1))]
        [InlineData(typeof(ValType1?), typeof(ValType1))]
        [InlineData(typeof(Task<C1>), typeof(C1))]
        [InlineData(typeof(ValueTask<C1>), typeof(C1))]
        public void SpecialGenericTypesAreUnwrapped(Type special, Type unwrapped)
        {
            var specialMapped = GqlMemberHelper.MapMembers<PropertyInfo>(special).Select(i => i.Member).OfType<PropertyInfo>().ToList();
            var unwrappedMapped = GqlMemberHelper.MapMembers<PropertyInfo>(unwrapped).Select(i => i.Member).OfType<PropertyInfo>().ToList();

            specialMapped.Should().HaveCount(unwrappedMapped.Count);

            specialMapped.Should().AllSatisfy(pi => unwrappedMapped.Contains(pi));
        }

        [Fact]
        public void InheritedPropertiesAreMapped()
        {
            var members = GqlMemberHelper.MapMembers<PropertyInfo>(typeof(C2));
            members.Should().Contain(m => m.Member.Name == nameof(C1.Property1) && m.Attribute.Name == "testName");            
        }

        [Fact]
        public void FieldsHaveInfoAboutGqlType()
        {
            var members = GqlMemberHelper.MapMembers<PropertyInfo>(typeof(C2));

            var propA1 = members.Single(m => m.Member.Name == nameof(C1.PropertyA1));
            propA1.GqlTypes.Should().BeEquivalentTo("TYPEA");

            var propA2 = members.Single(m => m.Member.Name == nameof(C1.PropertyB1));
            propA2.GqlTypes.Should().BeEquivalentTo("TYPEB");
        }

        private class C1 : IGqlTypeA, IGqlTypeB
        {
            public string? NotMarkedProperty { get; set; }

            [Gql(InclusionMode = FieldInclusionMode.Exclude)]
            public string? ExcludedProperty { get; set; }

            [Gql("testName")]
            public string? Property1 { get; set; }
            public string PropertyA1 { get; set; }
            public string PropertyB1 { get; set; }
        }

        private class C2 : C1
        {
            [Gql]
            public string? Property2 { get; set; }
        }               

        private struct ValType1
        {
            public int Prop1 { get; set; }
        }

        [GqlTypeFragment("TYPEA")]
        public interface IGqlTypeA
        {
            string PropertyA1 { get; set; }
        }

        [GqlTypeFragment("TYPEB")]
        public interface IGqlTypeB
        {
            string PropertyB1 { get; set; }
        }
    }
}
