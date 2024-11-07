using MTecl.GraphQlClient.Execution;
using MTecl.GraphQlClient.ObjectMapping;
using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes;
using MTecl.GraphQlClient.ObjectMapping.Rendering;
using MTecl.GraphQlClient.ObjectMapping.Rendering.JsonConvertors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MTecl.GraphQlClient
{
    public class GraphQlQueryBuilder
    {        
        protected GraphQlQueryBuilder() 
        {
            JsonSerializerOptions = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            JsonSerializerOptions.Converters.Add(new DateTimeConverter(this));
            JsonSerializerOptions.Converters.Add(new EnumValueConverter(false));
        }

        public Action<INode> CustomizeQueryGraph { get; set; }

        public Func<string, string> CustomizeQueryText { get; set; }

        /// <summary>
        /// By default ISO 8601 https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip
        /// 
        /// Use <see cref="DateTimeConverter.StringConversionMode"/> to specify string format, or implement <see cref="DateTimeConverter.IDateTimeConversionMode"/> to use any other DT format
        /// </summary>
        public DateTimeConverter.IDateTimeConversionMode DateTimeConversionMode { get; set; } = DateTimeConverter.StringConversionMode("o", CultureInfo.InvariantCulture);
                
        public JsonSerializerOptions JsonSerializerOptions { get; set; }        
                
        /// <summary>
        /// For very specific cases where non-generic variant of the builder is needed. 
        /// </summary>
        /// <returns></returns>
        public static GraphQlQueryBuilder Create() => new GraphQlQueryBuilder();
    }

    /// <summary>
    /// This class is responsible for building (compiling) of GraphQL queries based on C# expressions. Please keep in mind that query compilation utilizes lot of reflection, 
    /// string manipulation and other expensive operations. Therefore it's good idea to keep compiled queries as static singletons. Use variables and see <link to best practices></link>
    /// </summary>
    /// <typeparam name="TQueryType">Type of queries root object (typically an interface)</typeparam>
    public class GraphQlQueryBuilder<TQueryType> : GraphQlQueryBuilder
    {
        /// <summary>
        /// Builds a query object
        /// </summary>
        /// <typeparam name="TResult">Type of query result</typeparam>
        /// <param name="queryName">Name of the query</param>
        /// <param name="expression">An expression tree describing the query graph</param>
        /// <returns>Query object</returns>
        public IQuery<TResult> Build<TResult>(string queryName, Expression<Func<TQueryType, TResult>> expression) => Build(queryName, expression, _ => { });

        /// <summary>
        /// Builds a query object
        /// </summary>
        /// <typeparam name="TResult">Type of query result</typeparam>
        /// <param name="expression">An expression tree describing the query graph</param>
        /// <returns>Query object</returns>
        public IQuery<TResult> Build<TResult>(Expression<Func<TQueryType, TResult>> expression) => Build(null, expression);

        /// <summary>
        /// Builds a mutation object
        /// </summary>
        /// <typeparam name="TResult">Type of query result</typeparam>
        /// <param name="queryName">Name fo the query</param>
        /// <param name="expression">An expression tree describing the query graph</param>
        /// <returns>Query object</returns>
        public IQuery<TResult> BuildMutation<TResult>(string queryName, Expression<Func<TQueryType, TResult>> expression)
        {
            return Build(queryName, expression, n => { n.NodeTypeSymbol = "mutation"; });
        }

        /// <summary>
        /// Builds a mutation object
        /// </summary>
        /// <typeparam name="TResult">Type of query result</typeparam>
        /// <param name="expression">An expression tree describing the query graph</param>
        /// <returns>Query object</returns>
        public IQuery<TResult> BuildMutation<TResult>(Expression<Func<TQueryType, TResult>> expression) => BuildMutation(null, expression);

        private IQuery<TResult> Build<TResult>(string queryName, Expression<Func<TQueryType, TResult>> expression, Action<QueryNode> modifyQuery)
        {
            var built = QueryMapper.MapQuery(expression);
            built.QueryName = queryName;

            modifyQuery(built);

            CustomizeQueryGraph?.Invoke(built);

            var sb = new StringBuilder();
            built.Render(sb, this);
            var query = sb.ToString();

            if (CustomizeQueryText != null)
                query = CustomizeQueryText(query);

            return new GqlQuery<TResult>(query, this);
        }


        private sealed class GqlQuery<TResult> : IQuery<TResult>
        {
            private readonly string _queryBody;

            public GqlQuery(string query, GraphQlQueryBuilder builder)
            {
                _queryBody = query;
                Builder = builder;
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

            public GraphQlQueryBuilder Builder { get; }
            #endregion
        }
    }
}
