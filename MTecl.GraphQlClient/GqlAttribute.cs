using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using System;
using System.Reflection;

namespace MTecl.GraphQlClient
{
    /// <summary>
    /// Use this attribute to change default behavior of fields, methods, parameters
    /// </summary>
    [AttributeUsage(validOn: AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property)]
    public class GqlAttribute : Attribute, IGqlMember
    {
        public GqlAttribute() : this(null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Overrides default member name</param>
        /// <param name="inclusionMode">Changes default behavior of felds including in the parent node</param>
        public GqlAttribute(string name, FieldInclusionMode inclusionMode = FieldInclusionMode.Default)
        {
            Name = name;
            InclusionMode = inclusionMode;
        }
                
        /// <summary>
        /// Null or custom member name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Null or name of aliased member
        /// </summary>
        public string IsAliasFor { get; set; }

        /// <summary>
        /// Member sepcific behavior of field inclusion
        /// </summary>
        public FieldInclusionMode InclusionMode { get; set; }

        /// <summary>
        /// Returns an instance (new or provided) of GqlAtribute combining default values obtained from passed member with values from provided source 
        /// </summary>
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

            return (T)(object)new GqlAttribute { InclusionMode = source.InclusionMode, Name = memberName, IsAliasFor = source.IsAliasFor };
        }
    }
}
