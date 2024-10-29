using FluentAssertions;
using MTecl.GraphQlClient.Utils;
using System.Linq.Expressions;

namespace MTecl.GraphQlClient.UnitTests
{
    public class ExpressionTreeHelperTests
    {
        [Fact]
        public void EvaluateExpression_ShouldReturnConstantValue()
        {
            Expression expression = Expression.Constant(42);

            var result = ExpressionTreeHelper.EvaluateExpression(expression);

            Assert.Equal(42, result);
        }

        [Fact]
        public void EvaluateExpression_ShouldEvaluateLambdaExpression()
        {
            Expression<Func<int>> expression = () => 100;

            var result = ExpressionTreeHelper.EvaluateExpression(expression);

            Assert.Equal(100, result);
        }

        [Fact]
        public void EvaluateExpression_ShouldEvaluateArithmeticExpression()
        {
            Expression<Func<int>> expression = () => 5 + 10 * 2;

            var result = ExpressionTreeHelper.EvaluateExpression(expression);

            Assert.Equal(25, result);
        }

        [Fact]
        public void EvaluateExpression_ShouldEvaluateComplexExpressionWithParameters()
        {
            ParameterExpression paramA = Expression.Parameter(typeof(int), "a");
            ParameterExpression paramB = Expression.Parameter(typeof(int), "b");
            var expressionBody = Expression.Add(paramA, paramB);
            var lambda = Expression.Lambda(expressionBody, paramA, paramB);

            var result = ExpressionTreeHelper.EvaluateExpression(Expression.Invoke(lambda, Expression.Constant(3), Expression.Constant(7)));

            Assert.Equal(10, result);
        }
    }
}