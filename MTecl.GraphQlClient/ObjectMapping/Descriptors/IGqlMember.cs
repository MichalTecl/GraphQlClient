using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.Descriptors
{
    public interface IGqlMember
    {
        FieldInclusionMode InclusionMode { get; }

        string Name { get; }

        string IsAliasFor { get; }
                
        T CloneWithDefaults<T>(T source, object member) where T : IGqlMember;
    }
}
