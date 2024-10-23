using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.Descriptors
{
    internal class GqlMemberHelper
    {        
        private static readonly ConcurrentDictionary<string, object> _cache = new ConcurrentDictionary<string, object>();

        public static Dictionary<TMember, TAttribute> MapMembers<TMember, TAttribute>(Type type, bool unwrapType = true) 
            where TMember : MemberInfo 
            where TAttribute : class, IGqlMember, new()
        {
            if (unwrapType)
                type = UnwrapType(type);

            var ckey = typeof(TMember).FullName + typeof(TAttribute).FullName + type.FullName;

            Dictionary <TMember, TAttribute> Map()                    
            {
                var result = new Dictionary<TMember, TAttribute>();

                foreach (var m in type.GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy).OfType<TMember>())
                {
                    var attr = MapMember<TAttribute>(m);                    
                    result.Add(m, attr);
                }

                return result;
            }

            return (Dictionary<TMember, TAttribute>)_cache.GetOrAdd(ckey, _ => Map());
        }

        public static TAttribute MapMember<TAttribute>(MemberInfo m) where TAttribute : class, IGqlMember, new()
        {
            return MapMember<TAttribute>(m, m.Name, m.GetCustomAttributes());
        }

        public static TAttribute MapMember<TAttribute>(object member, string memberName, IEnumerable<Attribute> memberAttributes) where TAttribute : class, IGqlMember, new()
        {
            var attr = memberAttributes.OfType<TAttribute>().FirstOrDefault() ?? new TAttribute();
            return attr.CloneWithDefaults(attr, member);
        }

        private static Type UnwrapType(Type type)
        {            
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return Nullable.GetUnderlyingType(type);
            }
                        
            if (type != typeof(string) && (type.IsGenericType && typeof(IEnumerable).IsAssignableFrom(type.GetGenericTypeDefinition())))
            {
                return type.GetGenericArguments().FirstOrDefault() ?? type;
            }
                        
            return type;
        }
    }
}
