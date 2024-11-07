using MTecl.GraphQlClient.ObjectMapping.ResponseProcessing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MTecl.GraphQlClient.Execution
{
    public class GqlRequestOptions
    {
        /// <summary>
        /// Request headers
        /// </summary>
        public Dictionary<string, string> CustomRequestHeaders { get; } = new Dictionary<string, string> { { "accept", "application/json" } };

        /// <summary>
        /// URL of GraphQL API
        /// </summary>
        public Uri RequestUri { get; set; }

        /// <summary>
        /// Possibility to obtain raw request data, mostly for logging or debugging purposes
        /// </summary>
        public Action<string> RawRequestPeek { get; set; } = _ => {; };

        /// <summary>
        /// Possibility to obtain raw response data, mostly for logging or debugging purposes
        /// </summary>
        public Action<string> RawResponsePeek { get; set; } = _ => {; };

        /// <summary>
        /// Communication encoding
        /// </summary>
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        /// <summary>
        /// This instance will be used to generate HttpRequestMessage with all request data
        /// </summary>
        public IRequestMessageBuilder RequestMessageBuilder { get; set; } = DefaultRequestMessageBuilder.Instance;
        
        /// <summary>
        /// Can be used to modify processing of Http communication errors (! this is not about query errors returned from API !)
        /// </summary>
        public Func<HttpResponseMessage, string, Exception> CreateResponseException { get; set; } = (response, responseString) => new Exception($"({response.StatusCode}): {responseString}");

        /// <summary>
        /// This instance will be used to deserialize received responses from GraphQL API
        /// </summary>
        public IResponseDeserializer ResponseDeserializer { get; set; } = DefaultResponseDeserializer.Instance;

        public interface IRequestMessageBuilder
        {
            HttpRequestMessage CreateHttpRequestMessage(Uri uri, Dictionary<string, string> headers, string body, Encoding encoding);
        }

        private sealed class DefaultRequestMessageBuilder : IRequestMessageBuilder
        {
            public static readonly DefaultRequestMessageBuilder Instance = new DefaultRequestMessageBuilder();

            public HttpRequestMessage CreateHttpRequestMessage(Uri uri, Dictionary<string, string> headers, string body, Encoding encoding)
            {
                var msg = new HttpRequestMessage
                {
                    RequestUri = uri,
                    Method = HttpMethod.Post,
                    Content = new StringContent(body, encoding, "application/json")
                };

                foreach (var h in headers)
                {
                    msg.Headers.Add(h.Key, h.Value);
                }

                return msg;
            }
        }

        
    }
}
