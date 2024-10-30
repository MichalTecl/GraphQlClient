using MTecl.GraphQlClient.ObjectMapping.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes
{
    internal class QueryNode<TResult> : NodeBase
    {
        public override string NodeId => $"Query<{typeof(TResult).Name}>";

        public override bool IsImportant => true;

        protected override void Render(RenderHelper renderHelper)
        {
            renderHelper.Literal("query: ").OpenCodeBlock();
            Nodes.Render(renderHelper.StringBuilder, renderHelper.RenderOptions);
            renderHelper.CloseCodeBlock();
        }
    }
}
