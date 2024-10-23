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
        public void MarkedPropertiesAreMapped()
        {
            var members = GqlMemberHelper.MapMembers<PropertyInfo, TestAttr>(typeof(C1));
            members.Should().Contain(m => m.Key.Name == nameof(C1.Property1) && m.Value.Name == TestAttr.DEFAULT_NAME);
        }

        [Fact]
        public void NotMarkedPropertiesAreSkipped()
        {
            var members = GqlMemberHelper.MapMembers<PropertyInfo, TestAttr>(typeof(C1));
            members.Should().Contain(m => m.Key.Name == nameof(C1.NotMarkedProperty));
        }

        [Fact]
        public void IgnoredPropertiesAreSkipped()
        {
            var members = GqlMemberHelper.MapMembers<PropertyInfo, TestAttr>(typeof(C1));
            members.Should().NotContain(m => m.Key.Name == nameof(C1.IgnoredProperty));
        }

        [Fact]
        public void InheritedPropertiesAreMapped()
        {
            var members = GqlMemberHelper.MapMembers<PropertyInfo, TestAttr>(typeof(C2));
            members.Should().Contain(m => m.Key.Name == nameof(C1.Property1) && m.Value.Name == TestAttr.DEFAULT_NAME);            
        }

        private class C1
        {
            public string? NotMarkedProperty { get; set; }

            [TestAttr(InclusionMode = FieldInclusionMode.Exclude)]
            public string? IgnoredProperty { get; set; }

            [TestAttr]
            public string? Property1 { get; set; }
        }

        private class C2 : C1
        {
            [TestAttr]
            public string? Property2 { get; set; }
        }
                
        private class TestAttr : Attribute, IGqlMember
        {
            public const string DEFAULT_NAME = "default";
            

            
            public string Name { get; private set; } = "";
            public FieldInclusionMode InclusionMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

            public T CloneWithDefaults<T>(T source, object member) where T : IGqlMember
            {
                if(source is not TestAttr ta)
                    throw new ArgumentException();

                return (T)(object)new TestAttr()
                {
                    InclusionMode = ta.InclusionMode,
                    Name = DEFAULT_NAME
                };
            }
        }
    }
}
