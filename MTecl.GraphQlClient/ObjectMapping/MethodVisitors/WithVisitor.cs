using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using System.Linq;
using System.Linq.Expressions;

namespace MTecl.GraphQlClient.ObjectMapping.Visitors
{
    internal class WithVisitor : IMethodVisitor
    {
        public INode Visit(INode parent, MethodCallExpression expression)
        {
            var upper = QueryMapper.Visit(parent, expression.Arguments.First());

            foreach (var argument in expression.Arguments.Skip(1)) 
            {
                if (argument is NewArrayExpression ary)
                {
                    foreach (var ine in ary.Expressions)
                        QueryMapper.Visit(upper, ine);

                    continue;
                }

                QueryMapper.Visit(upper, argument);
            }

            return upper;
        }
    }
}
