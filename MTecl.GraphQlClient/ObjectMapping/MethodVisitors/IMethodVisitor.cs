using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using System.Linq.Expressions;

namespace MTecl.GraphQlClient.ObjectMapping.Visitors
{
    internal interface IMethodVisitor
    {
        INode Visit(INode parent, MethodCallExpression expression);
    }
}
