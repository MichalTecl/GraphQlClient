using MTecl.GraphQlClient.Execution;
using MTecl.GraphQlClient.ObjectMapping;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MTecl.GraphQlClient
{
    public class GqlQuery<TResult> : IQuery<TResult>
    {
        private readonly string _queryBody;

        private GqlQuery(string query)
        {
            _queryBody = query;
        }
        
        public static IQuery<TResult> Build<TQueryType>(string queryName, Expression<Func<TQueryType, TResult>> expression, GqlCompilerOptions options = null)
        {
            return Build(expression, options).Named(queryName);
        }

        public static IQuery<TResult> Build<TQueryType>(Expression<Func<TQueryType, TResult>> expression, GqlCompilerOptions options = null)
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

        #region IQuery
        public GqlRequestOptions Options { get; } = new GqlRequestOptions();

        public string QueryBody => _queryBody;

        public Dictionary<string, object> Variables { get; } = new Dictionary<string, object>(0);

        public string QueryName => null;
        #endregion
    }
}
