using MTecl.GraphQlClient.Execution;
using MTecl.GraphQlClient.ObjectMapping;
using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient
{
    public class GqlQuery<TResult> : IExecutionData<TResult>
    {
        private readonly string _queryBody;

        private GqlQuery(string query)
        {
            _queryBody = query;
        }
        
        public static GqlQuery<TResult> Build<TQueryType>(Expression<Func<TQueryType, TResult>> expression, GqlCompilerOptions options = null)
        {
            options = options ?? GqlCompilerOptions.Default;

            var built = QueryMapper.MapQuery(expression);

            options.CustomizeQueryGraph?.Invoke(built);

            var sb = new StringBuilder();
            built.Render(sb, options.RenderOptions);
            var query = sb.ToString();

            if (options.CustomizeQueryText != null)
                query = options.CustomizeQueryText(query);

            return new GqlQuery<TResult>(query);
        }

        public override string ToString()
        {
            return _queryBody;
        }

        #region IExecutionData
        public GqlRequestOptions Options { get; } = new GqlRequestOptions();

        public string QueryBody => _queryBody;

        public Dictionary<string, object> Variables { get; } = new Dictionary<string, object>(0);
        #endregion
    }
}
