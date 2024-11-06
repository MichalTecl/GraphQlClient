using MTecl.GraphQlClient.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using static MTecl.GraphQlClient.Execution.GqlRequestOptions;

namespace MTecl.GraphQlClient.ObjectMapping.ResponseProcessing
{
    internal class DefaultResponseDeserializer : IResponseDeserializer
    {
        public static readonly DefaultResponseDeserializer Instance = new DefaultResponseDeserializer();

        public T DeserializeResponse<T>(string response, GraphQlQueryBuilder builder)
        {
            var node = JsonNode.Parse(response);
                                    
            var errorsNode = node["errors"];
            if (errorsNode != null)
            {
                try
                {
                    var errors = node.Deserialize<Errors>();

                    throw new ServerErrorResponseException(response, errors.GetErrorsText());
                }
                catch (ServerErrorResponseException)
                {
                    throw;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to read errors information (\"{ex.Message}\"). Raw: {response}", ex);
                }
            }

            var dataNode = node["data"]?.AsObject();
            if (dataNode == null)
                throw new ArgumentException("Invalid format of server response. No 'data' node found");

            var valueNode = dataNode.FirstOrDefault();
            if (valueNode.Key == null || valueNode.Value == null)
                return default;
            
            return valueNode.Value.Deserialize<T>(builder.JsonSerializerOptions);
        }

        #region Model
        private sealed class Errors
        {
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
                var parts = new string[] {
                   message ?? "ERROR",
                   locations == null || locations.Count == 0 ? string.Empty : $"[{string.Join(", ", locations)}]",
                   path == null || path.Count == 0 ? string.Empty : string.Join("/", path)
                };

                return string.Join(" ", parts);
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
        #endregion
    }
}
