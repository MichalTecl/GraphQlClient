﻿
using MTecl.GraphQlClient.ObjectMapping.Rendering;

namespace MTecl.GraphQlClient.Utils
{
    internal static class NamingConventionHelper
    {
        public static string ConvertName(string name, GraphQlQueryBuilder options)
        {
            if (options.ConvertFieldNamesToCamelCase)
                name = CCase(name);

            return name;
        }

        public static string CCase(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s;

            if (s.Length == 1)
                return s.ToLower();

            return string.Concat(s.Substring(0, 1).ToLower(), s.Substring(1));
        }
    }
}
