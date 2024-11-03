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

            var methodArguments = mce.Method.GetParameters().Select(p => GqlMemberHelper.MapParameter(p, p.GetCustomAttributes())).ToList();

            for(var paramIndex  = 0; paramIndex < Math.Min(expression.Arguments.Count, methodArguments.Count); paramIndex++)
            {
                var methodArgument = methodArguments[paramIndex];

                var value = ResolveArgumentValue(parent, mce.Arguments[paramIndex]);
                methodNode.Arguments.Add(new KeyValuePair<string, object>(methodArgument.Attribute.Name, value));
            }

            return methodNode;
        }

        internal static object ResolveArgumentValue(INode parent, Expression expression)
        {
            if (expression is UnaryExpression unr && unr.NodeType == ExpressionType.Convert)
                return ResolveArgumentValue(parent, unr.Operand);
                        
            else if (expression is MethodCallExpression mce && QueryVariable.IsPassMethod(mce.Method))
            {
                return QueryVariable.ProcessPassCall(parent, mce);
            }

            return ExpressionTreeHelper.EvaluateExpression(expression);
        }
    }
}
