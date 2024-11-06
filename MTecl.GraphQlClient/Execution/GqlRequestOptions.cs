using MTecl.GraphQlClient.Exceptions;
using MTecl.GraphQlClient.ObjectMapping.Rendering;
using MTecl.GraphQlClient.ObjectMapping.Rendering.JsonConvertors;
using MTecl.GraphQlClient.ObjectMapping.ResponseProcessing;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MTecl.GraphQlClient.Execution
{
    public class GqlRequestOptions
    {
        public RequestHeaders CustomRequestHeaders { get; } = new RequestHeaders().Set("accept", "application/json");

        public Uri RequestUri { get; set; }

        public Action<string> RawRequestPeek { get; set; } = _ => {; };

        public Action<string> RawResponsePeek { get; set; } = _ => {; };

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public IRequestMessageBuilder RequestMessageBuilder { get; set; } = DefaultRequestMessageBuilder.Instance;
        public Func<HttpResponseMessage, string, Exception> CreateResponseException { get; set; } = (response, responseString) => new Exception($"({response.StatusCode}): {responseString}");

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

        public IResponseDeserializer ResponseDeserializer { get; set; } = DefaultResponseDeserializer.Instance;


        public sealed class RequestHeaders : Dictionary<string, string>
        {
            public RequestHeaders Set(string key, string value)
            {
                this[key] = value;
                return this;
            }
        }
    }
}
