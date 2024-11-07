using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using System;

namespace MTecl.GraphQlClient
{
    /// <summary>
    /// Specifies that members of marked interface should be included in inline fragment on provided typeName
    /// </summary>
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
