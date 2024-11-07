using MTecl.GraphQlClient.ObjectMapping.Rendering;

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
            Nodes.Render(renderHelper.StringBuilder, renderHelper.Builder);
            renderHelper.Indent(Level).CloseCodeBlock();
        }
    }
}
