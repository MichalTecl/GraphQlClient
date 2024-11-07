using MTecl.GraphQlClient.ObjectMapping.Rendering;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes
{
    internal class FragmentDeclarationNode : NodeBase
    {
        public string Name { get; set; }
        public string OnType { get; set; }

        public override string NodeId => $"{Name}_{OnType}";

        public override bool IsImportant => true;

        protected override void Render(RenderHelper renderHelper)
        {
            renderHelper
                .Indent(Level).Literal("fragment ").Literal(Name).Literal(" on ").Literal(OnType).OpenCodeBlock();
                
            Nodes.Render(renderHelper.StringBuilder, renderHelper.Builder);

            renderHelper.CloseCodeBlock();
        }
     
    }
}
