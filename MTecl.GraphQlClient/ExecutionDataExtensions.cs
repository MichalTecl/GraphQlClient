using MTecl.GraphQlClient.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient
{
    public static  class ExecutionDataExtensions
    {
        public static async Task<TResult> ExecuteAsync<TResult>(this IExecutionData<TResult> ed, HttpClient httpClient, CancellationToken cancellationToken = default)
        {
            return await QueryExecutor.ExecuteAsync<TResult>(ed, httpClient, cancellationToken);
        }

        public static IExecutionData<TResult> WithOptions<TResult>(this IExecutionData<TResult> ed, GqlRequestOptions options)
        {
            return new ExData<TResult>(ed.QueryBody, ed.Variables, options);
        }

        public static IExecutionData<TResult> WithVariable<TResult>(this IExecutionData<TResult> d, string name, object value)
        {            
            var target = Merge(d.Variables, null);
            target[SanitizeVarName(name)] = value;

            return new ExData<TResult>(d.QueryBody, target, d.Options);
        }

        public static IExecutionData<TResult> WithVariables<TResult>(this IExecutionData<TResult> d, IDictionary<string, object> variables)
        {
            var target = Merge(d.Variables, variables?.ToDictionary(dc => SanitizeVarName(dc.Key), dc => dc.Value));
            
            return new ExData<TResult>(d.QueryBody, target, d.Options);
        }

        public static IExecutionData<TResult> WithVariables<TResult>(this IExecutionData<TResult> d, object variables)
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

        private sealed class ExData<TResult> : IExecutionData<TResult>
        {
            public ExData(string queryBody, Dictionary<string, object> variables, GqlRequestOptions options)
            {
                QueryBody = queryBody;
                Variables = variables;
                Options = options;
            }

            public string QueryBody { get; }

            public Dictionary<string, object> Variables { get; }

            public GqlRequestOptions Options { get; }
        }
    }
}
