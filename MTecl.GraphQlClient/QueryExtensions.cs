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
    public static  class QueryExtensions
    {
        public static async Task<TResult> ExecuteAsync<TResult>(this IQuery<TResult> d, HttpClient httpClient, CancellationToken cancellationToken = default)
        {
            return await QueryExecutor.ExecuteAsync<TResult>(d, httpClient, cancellationToken);
        }

        public static IQuery<TResult> WithOptions<TResult>(this IQuery<TResult> ed, GqlRequestOptions options)
        {
            return new QData<TResult>(ed) { Options = options };
        }

        public static IQuery<TResult> WithVariable<TResult>(this IQuery<TResult> d, string name, object value)
        {            
            var target = Merge(d.Variables, null);
            target[SanitizeVarName(name)] = value;

            return new QData<TResult>(d) { Variables = target };
        }

        public static IQuery<TResult> WithVariables<TResult>(this IQuery<TResult> d, IDictionary<string, object> variables)
        {
            var target = Merge(d.Variables, variables?.ToDictionary(dc => SanitizeVarName(dc.Key), dc => dc.Value));
            
            return new QData<TResult>(d) { Variables = target };
        }

        public static IQuery<TResult> Named<TResult>(this IQuery<TResult> d, string queryName) => new QData<TResult>(d) { QueryName = queryName };



        public static IQuery<TResult> WithVariables<TResult>(this IQuery<TResult> d, object variables)
        {
            if (variables == null)
                return d;

            var dic = new Dictionary<string, object>();

            foreach(var p in variables.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead))
                dic[p.Name] = p.GetValue(d);

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
                QueryName = src.QueryName;
                Builder = src.Builder;
            }

            public string QueryName { get; set; }

            public string QueryBody { get; set; }

            public Dictionary<string, object> Variables { get; set; }

            public GqlRequestOptions Options { get; set; }

            public GraphQlQueryBuilder Builder { get; }
        }
    }
}
