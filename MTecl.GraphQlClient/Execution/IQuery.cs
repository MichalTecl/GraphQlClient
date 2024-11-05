using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.Execution
{
    public interface IQuery<TResult>
    {
        string QueryBody { get; }

        Dictionary<string, object> Variables { get; }

        GqlRequestOptions Options { get; }

        string QueryName { get; }
    }
}
