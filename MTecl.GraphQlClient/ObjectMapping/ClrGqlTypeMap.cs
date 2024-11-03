using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping
{
    internal static class ClrGqlTypeMap
    {
        public static readonly IDictionary<Type, string> TypeMap = new Dictionary<Type, string>
        {
            { typeof(string), "String" },
            { typeof(int), "Int" },
            { typeof(float), "Float" },
            { typeof(double), "Float" },
            { typeof(decimal), "Float" },
            { typeof(bool), "Boolean" },
            { typeof(char), "String" },
            { typeof(byte), "Int" },
            { typeof(sbyte), "Int" },
            { typeof(short), "Int" },
            { typeof(ushort), "Int" },
            { typeof(uint), "Int" },
            { typeof(long), "Int" },
            { typeof(ulong), "Int" },
            { typeof(DateTime), "String" }
        };
    }
}
