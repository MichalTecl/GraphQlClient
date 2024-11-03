using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.Rendering
{
    public interface IInputObjectSerializer
    {
        string Serialize(object o);
    }
}
