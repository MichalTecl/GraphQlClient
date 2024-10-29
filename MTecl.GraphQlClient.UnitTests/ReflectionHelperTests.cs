using FluentAssertions;
using MTecl.GraphQlClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.UnitTests
{
    public class ReflectionHelperTests
    {
        [Theory]
        [InlineData("X1", new Type[0])]
        [InlineData("S1", new Type[] { typeof(IA), typeof(IB) })]
        [InlineData("A1", new Type[] { typeof(IA) })]
        [InlineData("B1", new Type[] { typeof(IB) })]
        [InlineData("C1", new Type[] { typeof(IC) })]
        [InlineData("D1", new Type[] { typeof(ID) })]
        [InlineData("E1", new Type[] { typeof(IE) })]
        public void FindsInterfacesDefiningTheMember(string memberName, Type[] expectedTypes)
        {
            var member = typeof(C2).GetMember(memberName).Single();

            var ifaces = ReflectionHelper.GetDefiningInterfaces(member);

            ifaces.Should().BeEquivalentTo(expectedTypes);
        }

        public interface IA 
        { 
            int A1 { get; set; }
            int S1 { get; set; }
        }
        public interface IB 
        { 
            int B1 { get; set; }
            int S1 { get; set; }
        }

        public interface IC { int C1 { get; set; } }
        public interface ID { int D1(int a, int b); }
        public interface IE { void E1(); }

        public class C1 : IA, IB, ID, IE
        {
            public int A1 { get; set; }
            public int B1 { get; set; }
            public int S1 { get; set; }

            public int D1(int a, int b)
            {
                throw new NotImplementedException();
            }

            public void E1()
            {
                throw new NotImplementedException();
            }
        }
        public class C2 : C1, IC
        {
            public int C1 { get; set; }

            public int X1 { get; set; }
        }
    }
}
