using MTecl.GraphQlClient.ObjectMapping.Descriptors;
using MTecl.GraphQlClient.ObjectMapping.MethodVisitors;
using MTecl.GraphQlClient.ObjectMapping.Visitors;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MTecl.GraphQlClient
{
    /// <summary>
    /// Methods in this class are only intended to be used in expression trees
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Used to include fields into parent object or to perform some specific operations with included fields
        /// </summary>
        [TreeVisitor(typeof(WithVisitor))]
        public static IEnumerable<T> With<T>(this IEnumerable<T> src, params Func<T, object>[] f) => (IEnumerable<T>)Nope();

        /// <summary>
        /// Used to include fields into parent object or to perform some specific operations with included fields
        /// </summary>
        [TreeVisitor(typeof(WithVisitor))]
        public static List<T> With<T>(this List<T> src, params Func<T, object>[] f) => (List<T>)Nope();

        /// <summary>
        /// Used to include fields into parent object or to perform some specific operations with included fields
        /// </summary>
        [TreeVisitor(typeof(WithVisitor))]
        public static T With<T>(this T src, params Func<T, object>[] f) => (T)Nope();
                
        /// <summary>
        /// Adds argument to parent object
        /// </summary>
        [TreeVisitor(typeof(ArgumentMethodVisitor))]
        public static T Argument<T>(this T src, string Name, object value) => (T)Nope();
               
        /// <summary>
        /// Sets up an alias for parent object
        /// </summary>
        [TreeVisitor(typeof(IsAliasForVisitor))]
        public static T IsAliasFor<T>(this T src, string aliasedFieldName) => (T)Nope();

        /// <summary>
        /// Sets up an alias and argument for parent object
        /// </summary>
        [TreeVisitor(typeof(IsAliasForVisitor))]
        public static T IsAliasFor<T>(this T src, string aliasedFieldName, string argumentName, object argumentValue) => (T)Nope();
                
        private static object Nope([CallerMemberName] string callerName = "") 
        {
            throw new InvalidOperationException($"The {callerName} method should be used only in expression tree. Direct call is not permitted.");
        }
    }
}
