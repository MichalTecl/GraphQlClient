using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes;
using MTecl.GraphQlClient.ObjectMapping.Visitors;
using MTecl.GraphQlClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.MethodVisitors
{
    internal class ArgumentMethodVisitor : IMethodVisitor
    {
        public INode Visit(INode parent, MethodCallExpression expression)
        {
            var upper = QueryMapper.Visit(parent, expression.Arguments.First());

            if (!(upper is FieldNode field))
                throw new ArgumentException($"Argument can be set only to field node. {parent.GetType().Name} not expected here.");

            if (expression.Arguments.Count == 3)
            {
                var argName = ExpressionTreeHelper.EvaluateExpression(expression.Arguments[1]) as string;
                var argValue = DefaultMethodVisitor.ResolveArgumentValue(expression.Arguments[2]);

                field.Arguments.Add(new KeyValuePair<string, object>(argName, argValue));
            }
            else if (expression.Arguments.Count == 2)
            {
                var argsModel = DefaultMethodVisitor.ResolveArgumentValue(expression.Arguments[1]);

                foreach(var f in argsModel.GetType().GetProperties()) 
                {
                    var mapped = GqlMemberHelper.MapMember(f);
                    if (mapped.Attribute.InclusionMode == FieldInclusionMode.Exclude)
                        continue;

                    field.Arguments.Add(new KeyValuePair<string, object>(mapped.Attribute.Name, f.GetValue(argsModel)));
                }
            }

            return upper;
        }
    }
}
