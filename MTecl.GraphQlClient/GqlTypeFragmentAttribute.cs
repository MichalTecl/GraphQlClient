using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using System;

namespace MTecl.GraphQlClient
{
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class GqlTypeFragmentAttribute : Attribute, IGraphQlType
    {
        public GqlTypeFragmentAttribute(string typeName)
        {
            TypeName = typeName;
        }

        public string TypeName { get; }
    }
}
