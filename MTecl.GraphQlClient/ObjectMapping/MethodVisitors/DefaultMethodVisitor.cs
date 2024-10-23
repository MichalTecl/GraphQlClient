using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables;
using MTecl.GraphQlClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.Visitors

{
    internal class DefaultMethodVisitor : IMethodVisitor
    {
        public INode Visit(INode parent, MethodCallExpression expression)
        {
            if (!(expression is MethodCallExpression mce))
                throw new ArgumentException("Method call expected");

            var methodNode = (FieldNode)QueryMapper.ConstructMemberNode(mce.Method, mce.Method.ReturnType);

            parent.Nodes.Add(methodNode);

            var methodArguments = mce.Method.GetParameters().Select(p => GqlMemberHelper.MapMember<GqlAttribute>(p, p.Name, p.GetCustomAttributes())).ToList();

            for(var paramIndex  = 0; paramIndex < Math.Min(expression.Arguments.Count, methodArguments.Count); paramIndex++)
            {
                var methodArgument = methodArguments[paramIndex];

                var value = ResolveArgumentValue(mce.Arguments[paramIndex]);
                methodNode.Arguments.Add(new KeyValuePair<string, object>(methodArgument.Name, value));
            }

            return methodNode;
        }

        private object ResolveArgumentValue(Expression expression)
        {            
            if (expression is MemberExpression mex && QueryVariable.IsValueProperty(mex.Member))
            {
                var owner = ExpressionTreeHelper.EvaluateExpression(mex.Expression) as QueryVariable;
                if (owner != null)
                    return owner.RenderToQuery();
            }

            return ExpressionTreeHelper.EvaluateExpression(expression);
        }
    }
}
