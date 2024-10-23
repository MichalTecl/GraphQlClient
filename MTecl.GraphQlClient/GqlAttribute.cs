using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MTecl.GraphQlClient
{
    [AttributeUsage(validOn: AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property)]
    public class GqlAttribute : Attribute, IGqlMember
    {
        public GqlAttribute() : this(null) { }

        public GqlAttribute(string name, FieldInclusionMode inclusionMode = FieldInclusionMode.Default)
        {
            Name = name;
            InclusionMode = inclusionMode;
        }
                
        public string Name { get; set; }
        public FieldInclusionMode InclusionMode { get; set; }

        public T CloneWithDefaults<T>(T source, object member) where T : IGqlMember
        {
            if (!string.IsNullOrEmpty(source.Name))
                return source;

            string memberName;
            if (member is MemberInfo mi)
                memberName = mi.Name;
            else if (member is ParameterInfo pi)
                memberName = pi.Name;
            else
                throw new ArgumentException($"Unexpected member {member?.GetType()}");

            return (T)(object)new GqlAttribute { InclusionMode = source.InclusionMode, Name = memberName };
        }
    }
}
