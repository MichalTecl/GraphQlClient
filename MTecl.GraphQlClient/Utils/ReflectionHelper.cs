using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MTecl.GraphQlClient.Utils
{
    internal static class ReflectionHelper
    {        
        public static IEnumerable<Type> GetDefiningInterfaces(MemberInfo member)
        {
            var interfaces = member.DeclaringType.GetInterfaces();

            if (member is PropertyInfo pi)
                return interfaces.Where(iface => iface.GetProperties().Any(prop => MembersMatch(prop, pi)));

            if (member is MethodInfo mi)
                return interfaces.Where(iface => iface.GetMethods().Any(m =>  MembersMatch(m, mi)));

            throw new NotSupportedException($"{member.MemberType} is not supported.");
        }

        private static bool MembersMatch(MethodInfo interfaceMethod, MethodInfo targetMethod)
        {
            return interfaceMethod.Name == targetMethod.Name &&
                   interfaceMethod.ReturnType == targetMethod.ReturnType &&
                   interfaceMethod.GetParameters()
                       .Select(p => p.ParameterType)
                       .SequenceEqual(targetMethod.GetParameters().Select(p => p.ParameterType));
        }

        private static bool MembersMatch(PropertyInfo interfaceProperty, PropertyInfo targetProperty)
        {
            return interfaceProperty.Name == targetProperty.Name &&
                interfaceProperty.PropertyType == targetProperty.PropertyType;
        }
    }
}
