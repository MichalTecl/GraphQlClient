using MTecl.GraphQlClient.Exceptions;
using MTecl.GraphQlClient.ObjectMapping.ResponseProcessing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace MTecl.GraphQlClient
{
    public class GqlRequestOptions
    {
        public Dictionary<string, string> CustomRequestHeaders { get; } = new Dictionary<string, string>() { { "accept", "application/json" } };

        public Uri RequestUri { get; set; }

        public Action<string> RawRequestPeek { get; set; } = _ => {; };

        public Action<string> RawResponsePeek { get; set; } = _ => {; };

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public IRequestMessageBuilder RequestMessageBuilder { get; set; } = DefaultRequestMessageBuilder.Instance;
        public Func<HttpResponseMessage, string, Exception> CreateResponseException { get; set; } = (response, responseString) => new Exception($"({response.StatusCode}): {responseString}");

        public IResponseDeserializer ResponseDeserializer { get; set; } = DefaultResponseDeserializer.Instance;

        public interface IRequestMessageBuilder
        {
            HttpRequestMessage CreateHttpRequestMessage(Uri uri, Dictionary<string, string> headers, string body, Encoding encoding);
        }

        public JsonSerializerOptions JsonSerializerOptions { get; set; } = new JsonSerializerOptions();

        private sealed class DefaultRequestMessageBuilder : IRequestMessageBuilder
        {
            public static readonly DefaultRequestMessageBuilder Instance = new DefaultRequestMessageBuilder();

            public HttpRequestMessage CreateHttpRequestMessage(Uri uri, Dictionary<string, string> headers, string body, Encoding encoding)
            {
                var msg = new HttpRequestMessage
                {
                    RequestUri = uri,
                    Method = HttpMethod.Post,
                    Content = new StringContent(body, encoding)
                };

                foreach (var h in headers)
                {
                    msg.Headers.Add(h.Key, h.Value);
                }

                return msg;
            }
        }

        public interface IResponseDeserializer
        {
            T DeserializeResponse<T>(string response, JsonSerializerOptions serializerOptions);
        }        
    }
}
