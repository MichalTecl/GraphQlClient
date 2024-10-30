using MTecl.GraphQlClient.Exceptions;
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

        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public IRequestMessageBuilder RequestMessageBuilder { get; set; } = DefaultRequestMessageBuilder.Instance;
        public Func<HttpResponseMessage, string, Exception> CreateResponseException { get; set; } = (response, responseString) => new Exception($"({response.StatusCode}): {responseString}");

        public IResponseDeserializer ResponseDeserializer { get; set; } = DefaultResponseDeserializer.Instance;

        public interface IRequestMessageBuilder
        {
            HttpRequestMessage CreateHttpRequestMessage(Uri uri, Dictionary<string, string> headers, string body, Encoding encoding);
        }

        public JsonNamingPolicy JsonNamingPolicy { get; set; } = JsonNamingPolicy.CamelCase;

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
            T DeserializeResponse<T>(string response, JsonNamingPolicy namingPolicy);
        }

        private sealed class DefaultResponseDeserializer : IResponseDeserializer
        {
            public static readonly DefaultResponseDeserializer Instance = new DefaultResponseDeserializer();

            public T DeserializeResponse<T>(string response, JsonNamingPolicy namingPolicy)
            {
                var container = JsonSerializer.Deserialize<ResponseModel<T>>(response, new JsonSerializerOptions { PropertyNamingPolicy = namingPolicy });

                if (container.errors != null && container.errors.Count > 0)
                    throw new ServerErrorResponseException(container.GetErrorsText());

                return container.data;
            }

            private sealed class ResponseModel<T>
            {
                public T data { get; set; }

                public List<ErrorModel> errors { get; set; }

                public string GetErrorsText() => string.Join(Environment.NewLine, errors);
            }

            private sealed class ErrorModel
            {
                public string message { get; set; }
                public List<object> path { get; set; }
                public List<ErrorLocation> locations { get; set; }

                public override string ToString()
                {
                    return $"{message} [{string.Join(", ", locations)}] {string.Join("/", path)}";
                }
            }

            private sealed class ErrorLocation
            {
                public int line { get; set; }
                public int column { get; set; }

                public override string ToString()
                {
                    return $"ln: {line}, col: {column}";
                }
            }
        }
    }
}
