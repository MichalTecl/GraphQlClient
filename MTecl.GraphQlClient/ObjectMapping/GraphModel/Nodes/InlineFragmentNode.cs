using MTecl.GraphQlClient.ObjectMapping.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes
{
    internal class InlineFragmentNode : NodeBase
    {
        public override string NodeId => $"... on {OnType}";

        public string OnType { get; set; }

        public override bool IsImportant => true;

        protected override void Render(RenderHelper renderHelper)
        {
            renderHelper.Indent(Level).Literal(NodeId).OpenCodeBlock();
            Nodes.Render(renderHelper.StringBuilder, renderHelper.RenderOptions);
            renderHelper.Indent(Level).CloseCodeBlock();
        }
    }
}
