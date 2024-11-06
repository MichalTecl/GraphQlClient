using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MTecl.GraphQlClient.ObjectMapping.ResponseProcessing
{
    public interface IResponseDeserializer
    {
        T DeserializeResponse<T>(string response, GraphQlQueryBuilder Options);
    }
}
