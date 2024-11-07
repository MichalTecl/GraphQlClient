using MTecl.GraphQlClient.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient
{
    /// <summary>
    /// Sets of extension methods acting together as a builder pattern for parameterizing queries execution 
    /// </summary>
    public static  class QueryExtensions
    {
        /// <summary>
        /// Executes provided query object against the GraphQL API http endpoint
        /// </summary>
        /// <typeparam name="TResult">Type of expected query result</typeparam>
        /// <param name="d">Query</param>
        /// <param name="httpClient">Instance of HttpClient. It may have BaseAddress set so it is then not neccessary to supply it in options</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<TResult> ExecuteAsync<TResult>(this IQuery<TResult> d, HttpClient httpClient, CancellationToken cancellationToken = default)
        {
            return await QueryExecutor.ExecuteAsync<TResult>(d, httpClient, cancellationToken);
        }

        /// <summary>
        /// Adds options to the query execution
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        public static IQuery<TResult> WithOptions<TResult>(this IQuery<TResult> ed, GqlRequestOptions options)
        {
            return new QData<TResult>(ed) { Options = options };
        }

        /// <summary>
        /// Sets variable value to the query
        /// </summary>
        /// <param name="name">Variable name (can start with dollar but doesn't have to)</param>
        /// <param name="value">Value of the variable</param>
        public static IQuery<TResult> WithVariable<TResult>(this IQuery<TResult> d, string name, object value)
        {            
            var target = Merge(d.Variables, null);
            target[SanitizeVarName(name)] = value;

            return new QData<TResult>(d) { Variables = target };
        }

        /// <summary>
        /// Sets multiple variables to the query
        /// </summary>
        /// <param name="variables">Dictionary of variable names (names can start with dollar but doesn't have to) with their values</param>
        public static IQuery<TResult> WithVariables<TResult>(this IQuery<TResult> d, IDictionary<string, object> variables)
        {
            var target = Merge(d.Variables, variables?.ToDictionary(dc => SanitizeVarName(dc.Key), dc => dc.Value));
            
            return new QData<TResult>(d) { Variables = target };
        }

        /// <summary>
        /// Passes variable values 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="d"></param>
        /// <param name="variables"></param>
        /// <returns></returns>
        public static IQuery<TResult> WithVariables<TResult>(this IQuery<TResult> d, object variables)
        {
            if (variables == null)
                return d;

            var dic = new Dictionary<string, object>();

            foreach(var p in variables.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead))
                dic[p.Name] = p.GetValue(variables);

            return d.WithVariables(dic);
        }

        private static Dictionary<string, object> Merge(IDictionary<string, object> a, IDictionary<string, object> b)
        {
            var merged = new Dictionary<string, object>(a?.Count ?? 0 + b?.Count ?? 0);

            if (a != null)
                foreach(var kv in a)
                    merged[kv.Key] = kv.Value;

            if (b != null)
                foreach (var kv in b)
                    merged[kv.Key] = kv.Value;

            return merged;
        }

        private static string SanitizeVarName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name = name.TrimStart().TrimStart('$').Trim()))
                throw new ArgumentException($"Invalid format of variable name '{name}'");

            return name;
        }

        private sealed class QData<TResult> : IQuery<TResult>
        {
            public QData(IQuery<TResult> src)
            {
                QueryBody = src.QueryBody;
                Variables = src.Variables;
                Options = src.Options;
                Builder = src.Builder;
            }

            public string QueryBody { get; set; }

            public Dictionary<string, object> Variables { get; set; }

            public GqlRequestOptions Options { get; set; }

            public GraphQlQueryBuilder Builder { get; }
        }
    }
}
