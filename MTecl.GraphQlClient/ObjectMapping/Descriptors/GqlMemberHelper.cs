using MTecl.GraphQlClient.ObjectMapping.GraphModel.Variables;
using MTecl.GraphQlClient.Utils;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.ObjectMapping.Descriptors
{
    internal class GqlMemberHelper
    {        
        public static List<MappedMemberInfo> MapMembers<TMember>(Type type, bool unwrapType = true) 
            where TMember : MemberInfo 
        {
            if (unwrapType)
                type = UnwrapType(type);

            var result = new List<MappedMemberInfo>();

            foreach (var m in type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).OfType<TMember>())
            {
                var mapped = MapMember(m);                    
                result.Add(mapped);
            }

            return result;
        }

        public static MappedMemberInfo MapMember(MemberInfo m)
        {
            return MapMember(m, m.Name, m.GetCustomAttributes());
        }

        public static MappedMemberInfo MapMember(MemberInfo member, string memberName, IEnumerable<Attribute> memberAttributes) 
        {
            var attr = memberAttributes.OfType<IGqlMember>().FirstOrDefault() ?? new GqlAttribute();

            var belongsToTypes = ReflectionHelper.GetDefiningInterfaces(member)
                   .OfType<Type>()
                   .Select(iface => iface.GetCustomAttributes().OfType<IGraphQlType>().FirstOrDefault()?.TypeName)
                   .Where(tn => tn != null)
                   .Distinct()
                   .ToArray();

            return new MappedMemberInfo(member, attr.CloneWithDefaults(attr, member), belongsToTypes);
        }

        public static MappedMemberInfo MapParameter(ParameterInfo member, IEnumerable<Attribute> memberAttributes)
        {
            var attr = memberAttributes.OfType<IGqlMember>().FirstOrDefault() ?? new GqlAttribute();
            return new MappedMemberInfo(member, attr.CloneWithDefaults(attr, member));
        }

        private static Type UnwrapType(Type type)
        {
            type = Nullable.GetUnderlyingType(type) ?? type;

            if (type == typeof(string))
                return type;
                        
            if (type.IsGenericType)
            {
                var genericTypeDef = type.GetGenericTypeDefinition(); 

                if (genericTypeDef == typeof(Task<>) || genericTypeDef == typeof(ValueTask<>))
                    return type.GetGenericArguments().FirstOrDefault() ?? type;

                if (typeof(IEnumerable).IsAssignableFrom(genericTypeDef))
                    return type.GetGenericArguments().FirstOrDefault() ?? type;
            }
                        
            if (type.IsArray)
                return type.GetElementType();
            
            return type;
        }

        internal static string ObtainGqlTypeName(Type type)
        {
            var glt = Attribute.GetCustomAttributes(type, true).OfType<IGraphQlType>().FirstOrDefault();

            if (glt != null)
                return glt.TypeName;

            if (ClrGqlTypeMap.TypeMap.TryGetValue(type, out var gqlType))
                return gqlType;

            return type.Name;
        }

        public sealed class MappedMemberInfo
        {
            public MappedMemberInfo(MemberInfo member, IGqlMember attribute, string[] gqlTypes)
            {
                Member = member;
                Attribute = attribute;
                GqlTypes = gqlTypes;
            }

            public MappedMemberInfo(ParameterInfo parameter, IGqlMember attribute) : this(null, attribute, new string[0])
            {
                Parameter = parameter;
            }

            public MemberInfo Member { get; }
            public ParameterInfo Parameter { get; }
            public IGqlMember Attribute { get; }
            public string[] GqlTypes { get; }
        }
    }
}
