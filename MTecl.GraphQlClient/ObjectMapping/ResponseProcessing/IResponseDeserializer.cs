namespace MTecl.GraphQlClient.ObjectMapping.ResponseProcessing
{
    /// <summary>
    /// Implementing class is responsible for results deserialization
    /// </summary>
    public interface IResponseDeserializer
    {
        T DeserializeResponse<T>(string response, GraphQlQueryBuilder Options);
    }
}
