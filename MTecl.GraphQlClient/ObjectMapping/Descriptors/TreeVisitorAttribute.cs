using System;

namespace MTecl.GraphQlClient.ObjectMapping.Descriptors
{
    public sealed class TreeVisitorAttribute : Attribute
    {
        public TreeVisitorAttribute(Type visitorType)
        {
            VisitorType = visitorType;
        }

        public Type VisitorType { get; }
    }
}
