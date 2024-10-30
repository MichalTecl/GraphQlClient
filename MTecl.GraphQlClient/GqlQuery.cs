using MTecl.GraphQlClient.ObjectMapping;
using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient
{
    public class GqlQuery<T>
    {
        private readonly string _queryBody;
        
        private GqlQuery(string query)
        {
            _queryBody = query;
        }

        public async Task<T> ExecuteAsync(HttpClient httpClient, GqlRequestOptions options = null, CancellationToken ct = default)
        {
            options = options ?? new GqlRequestOptions();

            var uri = options.RequestUri ?? httpClient.BaseAddress ?? throw new ArgumentException($"Both {nameof(options)}.{nameof(options.RequestUri)} and {nameof(httpClient)}.{nameof(httpClient.BaseAddress)} are null");

            string responseString = null;
            using (var httpRequestMessage = options.RequestMessageBuilder.CreateHttpRequestMessage(uri, options.CustomRequestHeaders, _queryBody, options.Encoding))
            {
                using (var response = await httpClient.SendAsync(httpRequestMessage, ct))
                {
                    try
                    {
                        responseString = await response.Content.ReadAsStringAsync();
                    }
                    catch {; }

                    if (!response.IsSuccessStatusCode)
                    {
                        throw options.CreateResponseException(response, responseString);
                    }
                }
            }

            return options.ResponseDeserializer.DeserializeResponse<T>(responseString, options.JsonNamingPolicy);
        }

        public static GqlQuery<TResult> Build<TResult>(Expression<Func<TResult, object>> expression, GqlCompilerOptions options = null)
        {
            options = options ?? GqlCompilerOptions.Default;

            var built = QueryMapper.MapQuery(expression);

            options.CustomizeQueryGraph?.Invoke(built);

            var sb = new StringBuilder();
            built.Render(sb);
            var query = sb.ToString();

            if (options.CustomizeQueryText != null)
                query = options.CustomizeQueryText(query);

            var serialized = options.Serializer.SerializeRequestBody(query);

            return new GqlQuery<TResult>(query);
        }

        public override string ToString()
        {
            return _queryBody;
        }        
    }
}
