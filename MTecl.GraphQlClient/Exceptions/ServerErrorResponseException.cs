using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.Exceptions
{
    public class ServerErrorResponseException : Exception
    {
        public ServerErrorResponseException(string message):base(message) { }
    }
}
