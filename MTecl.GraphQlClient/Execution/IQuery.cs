using System.Collections.Generic;

namespace MTecl.GraphQlClient.Execution
{
    public interface IQuery<TResult>
    {
        string QueryBody { get; }

        Dictionary<string, object> Variables { get; }

        GqlRequestOptions Options { get; }

        GraphQlQueryBuilder Builder { get; }
    }
}
