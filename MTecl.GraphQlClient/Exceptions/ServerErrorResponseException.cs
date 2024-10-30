using System;

namespace MTecl.GraphQlClient.Exceptions
{
    public class ServerErrorResponseException : Exception
    {
        public string RawResponse { get; }

        public ServerErrorResponseException(string rawResponse, string errors):base(errors) 
        {
            RawResponse = rawResponse;
        }
    }
}
