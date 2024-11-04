using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MTecl.GraphQlClient.ObjectMapping.Rendering
{
    public interface IInputObjectSerializer
    {
        string Serialize(object o);
    }
}
