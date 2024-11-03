using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes;
using MTecl.GraphQlClient.ObjectMapping.Rendering;
using MTecl.GraphQlClient.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables
{    
    public abstract class QueryVariable
    {      
        public static T Pass<T>(string name, bool optional, string graphQlTypeName) 
        {
            throw new InvalidOperationException($"The {nameof(Pass)}<T> method is just a placeholder in expression tree. If you need to place the variable name into arguemtns object, use {nameof(GetVariableNameRenderer)} method");
        }

        public static T Pass<T>(string name, bool optional) => Pass<T>(null, true, null);

        public static T Pass<T>(string name, string graphQlTypeName) => Pass<T>(null, true, null);

        public static T Pass<T>(string name) => Pass<T>(null, true, null);

        internal static bool IsPassMethod(MethodInfo method)
        {
            return method.Name == nameof(Pass) && method.DeclaringType == typeof(QueryVariable);
        }

        public static IRenderMyself GetVariableNameRenderer(string value)
        {
            return new VariableName(value);
        }

        internal static object ProcessPassCall(INode parent, MethodCallExpression expression)
        {
            var name = (ExpressionTreeHelper.EvaluateExpression(expression.Arguments[0]) as string)?.Trim();
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("Variable name must be specified");

            var isOptional = ExpressionTreeHelper.TryGetArgument<bool>(expression, "optional", () => false);
            var typeName = ExpressionTreeHelper.TryGetArgument<string>(expression, "graphQlTypeName", () => GqlMemberHelper.ObtainGqlTypeName(expression.Method.GetGenericArguments().Single())).Trim();

            if (!isOptional && !typeName.EndsWith("!"))
                typeName = $"{typeName}!";

            IQueryNode q;
            for (var n = parent; (q = n as IQueryNode) == null && n.Parent != null; n = n.Parent) ;

            if (q == null)
                throw new InvalidOperationException("The query must have QueryNode root");

            if (!name.StartsWith("$"))
                name = $"${name}";

            q.QueryVariables.Add(new QueryVariableInfo
            {
                Name = name,
                TypeName = typeName
            });

            return GetVariableNameRenderer(name);
        }

        private sealed class VariableName : IRenderMyself
        {
            private readonly string _name;

            public VariableName(string name)
            {
                _name = name;
            }

            public void Render(int level, StringBuilder sb)
            {
                sb.Append(_name);
            }

            public override string ToString() => _name;           
        }        
    }
}
