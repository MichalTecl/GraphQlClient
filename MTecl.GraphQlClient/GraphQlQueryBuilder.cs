using MTecl.GraphQlClient.Execution;
using MTecl.GraphQlClient.ObjectMapping;
using MTecl.GraphQlClient.ObjectMapping.GraphModel;
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

            JsonSerializerOptions.Converters.Add(DateTimeConverter);
            JsonSerializerOptions.Converters.Add(new EnumValueConverter(false));

            InputObjectSerializer = new GqlObjectSerializer(this);
        }

        public Action<INode> CustomizeQueryGraph { get; set; }

        public Func<string, string> CustomizeQueryText { get; set; }

        public DateTimeConverter DateTimeConverter { get; } = new DateTimeConverter
        {
            // By default ISO 8601 https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip
            Mode = DateTimeConverter.StringConversionMode("o", CultureInfo.InvariantCulture)
        };

        public bool ConvertFieldNamesToCamelCase { get; set; } = true;

        public IInputObjectSerializer InputObjectSerializer { get; set; }

        public JsonSerializerOptions JsonSerializerOptions { get; set; }        
                
        public static GraphQlQueryBuilder Create() => new GraphQlQueryBuilder();
    }

    public class GraphQlQueryBuilder<TQueryType> : GraphQlQueryBuilder
    {
        public IQuery<TResult> Build<TResult>(Expression<Func<TQueryType, TResult>> expression)
        {            
            var built = QueryMapper.MapQuery(expression);

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
