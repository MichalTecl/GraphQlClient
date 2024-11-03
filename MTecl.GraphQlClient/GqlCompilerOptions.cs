using MTecl.GraphQlClient.ObjectMapping.GraphModel;
using MTecl.GraphQlClient.ObjectMapping.Rendering;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace MTecl.GraphQlClient
{
    public class GqlCompilerOptions
    {
        public static readonly GqlCompilerOptions Default = new GqlCompilerOptions();

        public Action<INode> CustomizeQueryGraph { get; set; }

        public Func<string, string> CustomizeQueryText { get; set; }

        public RenderOptions RenderOptions { get; set; } = RenderOptions.Default;               
    }
}
