using MTecl.GraphQlClient.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Nodes;
using static MTecl.GraphQlClient.Exceptions.ServerErrorResponseException;

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
                    var errors = node.Deserialize<Errors>(builder.JsonSerializerOptions);

                    throw new ServerErrorResponseException(response, errors.errors);
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

        public sealed class Errors
        {
            public List<ErrorModel> errors { get; set; }
        }
    }
}
