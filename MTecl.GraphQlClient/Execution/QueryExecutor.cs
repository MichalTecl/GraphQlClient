using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.Json;

namespace MTecl.GraphQlClient.Execution
{
    internal static class QueryExecutor
    {
        public static async Task<TResult> ExecuteAsync<TResult>(IQuery<TResult> data, HttpClient httpClient, CancellationToken ct = default)
        {            
            var uri = data.Options.RequestUri ?? httpClient.BaseAddress ?? throw new ArgumentException($"Both {nameof(GqlRequestOptions)}.{nameof(GqlRequestOptions.RequestUri)} and {nameof(httpClient)}.{nameof(httpClient.BaseAddress)} are null");

            var options = data.Options;

            var requestModel = new
            {
                query = data.QueryBody,
                variables = (data.Variables?.Count ?? 0) == 0 ? null : data.Variables 
            };

            string requestString = JsonSerializer.Serialize(requestModel, options.JsonSerializerOptions);

            string responseString = null;
            using (var httpRequestMessage = options.RequestMessageBuilder.CreateHttpRequestMessage(uri, options.CustomRequestHeaders, requestString, options.Encoding))
            {
                options.RawRequestPeek(requestString);

                using (var response = await httpClient.SendAsync(httpRequestMessage, ct))
                {
                    try
                    {
                        responseString = await response.Content.ReadAsStringAsync();
                        options.RawResponsePeek(responseString);
                    }
                    catch {; }

                    if (!response.IsSuccessStatusCode)
                    {
                        throw options.CreateResponseException(response, responseString);
                    }
                }
            }

            return options.ResponseDeserializer.DeserializeResponse<TResult>(responseString, options.JsonSerializerOptions);
        }        
    }
}
