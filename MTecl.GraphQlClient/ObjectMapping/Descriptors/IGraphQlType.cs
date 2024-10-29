using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.Descriptors
{
    public interface IGraphQlType
    {
        string TypeName { get; }
    }
}
