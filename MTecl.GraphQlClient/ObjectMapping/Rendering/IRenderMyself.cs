using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.Rendering
{
    /// <summary>
    /// Use this interface to implement custom rendering of the object to GraphQL query.
    /// </summary>
    public interface IRenderMyself
    {
        void Render(int level, StringBuilder sb);
    }
}
