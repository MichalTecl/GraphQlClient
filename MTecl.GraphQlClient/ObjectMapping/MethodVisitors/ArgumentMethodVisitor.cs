using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes;
using MTecl.GraphQlClient.ObjectMapping.Visitors;
using MTecl.GraphQlClient.Utils;
using System;
using System.Collections;
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

            SetNodeArguments(field, expression.Arguments.Skip(1).ToArray());            

            return upper;
        }

        internal static void SetNodeArguments(FieldNode target, Expression[] arguments)
        {
            if (arguments.Length == 2)
            {
                var argName = ExpressionTreeHelper.EvaluateExpression(arguments[0]) as string;
                var argValue = DefaultMethodVisitor.ResolveArgumentValue(target, arguments[1]);

                target.Arguments.Add(new KeyValuePair<string, object>(argName, argValue));
            }
            else if (arguments.Length == 1)
            {
                var argsModel = DefaultMethodVisitor.ResolveArgumentValue(target, arguments[0]);

                if (argsModel is IDictionary dictionary)
                {
                    foreach (var key in dictionary.Keys)
                    {
                        target.Arguments.Add(new KeyValuePair<string, object>(key.ToString(), dictionary[key]));
                    }
                }
                else
                {
                    foreach (var f in argsModel.GetType().GetProperties())
                    {
                        var mapped = GqlMemberHelper.MapMember(f);
                        if (mapped.Attribute.InclusionMode == FieldInclusionMode.Exclude)
                            continue;

                        target.Arguments.Add(new KeyValuePair<string, object>(mapped.Attribute.Name, f.GetValue(argsModel)));
                    }
                }
            }
        }
    }
}
