using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.Execution
{
    public interface IExecutionData<TResult>
    {
        string QueryBody { get; }

        Dictionary<string, object> Variables { get; }

        GqlRequestOptions Options { get; }
    }
}
