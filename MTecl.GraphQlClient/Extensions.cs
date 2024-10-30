using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient.ObjectMapping.MethodVisitors;
using MTecl.GraphQlClient.ObjectMapping.Visitors;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace MTecl.GraphQlClient
{
    public static class Extensions
    {
        [TreeVisitor(typeof(WithVisitor))]
        public static object With<T>(this IEnumerable<T> src, params Func<T, object>[] f) => Nope();
        
        [TreeVisitor(typeof(WithVisitor))]
        public static object With<T>(this T src, params Func<T, object>[] f) => Nope();
                
        [TreeVisitor(typeof(ArgumentMethodVisitor))]
        public static T Argument<T>(this T src, string Name, object value) => (T)Nope();

        [TreeVisitor(typeof(ArgumentMethodVisitor))]
        public static T Argument<T>(this T src, object arguments) => (T)Nope();

        [TreeVisitor(typeof(ArgumentMethodVisitor))]
        public static T Argument<T>(this T src, Dictionary<string, object> arguments) => (T)Nope();

        [TreeVisitor(typeof(IsAliasForVisitor))]
        public static T IsAliasFor<T>(this T src, string aliasedFieldName) => (T)Nope();

        [TreeVisitor(typeof(IsAliasForVisitor))]
        public static T IsAliasFor<T>(this T src, string aliasedFieldName, string argumentName, object argumentValue) => (T)Nope();

        [TreeVisitor(typeof(IsAliasForVisitor))]
        public static T IsAliasFor<T>(this T src, string aliasedFieldName, Dictionary<string, object> arguments) => (T)Nope();

        [TreeVisitor(typeof(IsAliasForVisitor))]
        public static T IsAliasFor<T>(this T src, string aliasedFieldName, object arguments) => (T)Nope();

        private static object Nope([CallerMemberName] string callerName = "") 
        {
            throw new InvalidOperationException($"The {callerName} method should be used only in expression tree. Direct call is not permitted.");
        }
    }
}
