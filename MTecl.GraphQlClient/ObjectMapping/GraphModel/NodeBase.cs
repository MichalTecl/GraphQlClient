using MTecl.GraphQlClient.ObjectMapping.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel
{
    internal abstract class NodeBase : INode
    {
        public NodeBase()
        {
            Nodes = new NodeCollection(this);
        }

        public int Level => Parent == null ? 0 : Parent.Level + 1;

        public INode Parent { get; set; }

        public NodeCollection Nodes { get; }

        public abstract string NodeId { get; }
        public abstract bool IsImportant { get; }

        public void Render(StringBuilder sb)
        {
            Render(new RenderHelper(sb));
        }

        protected abstract void Render(RenderHelper renderHelper);

        public override string ToString()
        {
            var sb = new StringBuilder();
            Render(sb);

            return sb.ToString();
        }
    }
}
