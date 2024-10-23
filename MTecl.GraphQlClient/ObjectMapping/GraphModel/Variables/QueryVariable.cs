using MTecl.GraphQlClient.ObjectMapping.Rendering;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables
{
    public sealed class QueryVariable<TValue> : QueryVariable
    {
        public QueryVariable(string name) : base(name)
        {
        }

        public TValue Value { get; }
    }

    public abstract class QueryVariable
    {
        private readonly VariableName _name;

        protected QueryVariable(string name)
        {
            _name = new VariableName(name);
        }

        internal object RenderToQuery()
        {
            return _name;
        }

        internal static bool IsValueProperty(MemberInfo member)
        {
            return (
                member is PropertyInfo pi && 
                (pi.Name == nameof(QueryVariable<object>.Value)) 
                && typeof(QueryVariable).IsAssignableFrom(pi.DeclaringType)); 
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
        }
    }
}
