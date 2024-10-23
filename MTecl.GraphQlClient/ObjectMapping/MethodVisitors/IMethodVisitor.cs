using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.Visitors
{
    internal interface IMethodVisitor
    {
        INode Visit(INode parent, MethodCallExpression expression);
    }
}
