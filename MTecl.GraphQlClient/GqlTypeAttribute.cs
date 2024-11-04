using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class GqlTypeAttribute : Attribute, IGraphQlType
    {
        public string TypeName { get; }

        public GqlTypeAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }
}
