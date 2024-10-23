using FluentAssertions;
using MTecl.GraphQlClient.Utils;

namespace MTecl.GraphQlClient.UnitTests
{
    public class ExpressionTreeHelperTests
    {
        [Fact]
        public void GetCalledMethod_returnsPassedMethod()
        {
            int param1 = 1;
            string param2 = "test";

            var method = ExpressionTreeHelper.GetCalledMethod<IInterface1, string>(i => i.Method1(param1, param2), out _);

            method.Should().NotBeNull();
            method.Name.Should().Be(nameof(IInterface1.Method1));
        }

        [Fact]
        public void GetCalledMethod_readsLiteralArgumentValues()
        {
            int param1 = 1;
            string param2 = "test";

            var method = ExpressionTreeHelper.GetCalledMethod<IInterface1, string>(i => i.Method1(param1, param2), out var parameters);

            parameters.Should().HaveCount(2);
            parameters.Should().ContainKey("param1").WhoseValue.Should().Be(param1);
            parameters.Should().ContainKey("param2").WhoseValue.Should().Be(param2);                        
        }

        [Fact]
        public void GetCalledMethod_readsDynamicArgumentValues()
        {     
            var method = ExpressionTreeHelper.GetCalledMethod<IInterface1, string>(i => 
            i.Method1(123 + 456 * GetInt(),
               new string(("123" + "456").Reverse().ToArray())  
                ), out var parameters);

            parameters.Should().HaveCount(2);
            parameters.Should().ContainKey("param1").WhoseValue.Should().Be(123 + 456 * GetInt());
            parameters.Should().ContainKey("param2").WhoseValue.Should().Be(new string(("123" + "456").Reverse().ToArray()));
        }


        public interface IInterface1
        {
            string Method1(int param1, string param2);
        }

        private static int GetInt() => DateTime.Now.Year;
    }
}