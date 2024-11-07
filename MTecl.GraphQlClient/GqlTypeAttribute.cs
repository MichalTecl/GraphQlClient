using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using System;

namespace MTecl.GraphQlClient
{
    /// <summary>
    /// Specifies GraphQL type name of marked class
    /// </summary>
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
