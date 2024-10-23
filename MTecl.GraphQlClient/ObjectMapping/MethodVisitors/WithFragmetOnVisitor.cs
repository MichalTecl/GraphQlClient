using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.Visitors;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.MethodVisitors
{
    internal class WithFragmetOnVisitor : IMethodVisitor
    {
        public INode Visit(INode parent, MethodCallExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}
