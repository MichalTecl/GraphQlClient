using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes;
using MTecl.GraphQlClient.ObjectMapping.Visitors;
using MTecl.GraphQlClient.Utils;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace MTecl.GraphQlClient.ObjectMapping.MethodVisitors
{
    internal class IsAliasForVisitor : IMethodVisitor
    {
        public INode Visit(INode parent, MethodCallExpression expression)
        {
            var upper = QueryMapper.Visit(parent, expression.Arguments.First());

            if (!(upper is FieldNode field))
                throw new ArgumentException($"IsAliasFor can be set only to field node. {parent.GetType().Name} not expected here.");

            var aliasedFieldName = ExpressionTreeHelper.EvaluateExpression(expression.Arguments[1]) as string;
            if (aliasedFieldName != null)
            {
                field.IsAliasFor = aliasedFieldName;
            }

            if (expression.Arguments.Count > 2)
                ArgumentMethodVisitor.SetNodeArguments(field, expression.Arguments.Skip(2).ToArray());

            return upper;
        }
    }
}
