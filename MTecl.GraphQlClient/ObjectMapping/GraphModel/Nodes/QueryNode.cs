using MTecl.GraphQlClient.ObjectMapping.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes
{
    internal class QueryNode<TResult> : NodeBase, IQueryNode
    {
        public override string NodeId => $"Query<{typeof(TResult).Name}>";

        public override bool IsImportant => true;

        public List<QueryVariableInfo> QueryVariables { get; } = new List<QueryVariableInfo>();
        public string QueryName { get; set; }

        protected override void Render(RenderHelper renderHelper)
        {
            renderHelper.Literal("query ");

            if (!string.IsNullOrWhiteSpace(QueryName))
                renderHelper.Literal(QueryName);

            if (QueryVariables.Count > 0)
            {
                renderHelper.OpenArgumentList();

                renderHelper.Literal(string.Join(", ", QueryVariables.Select(GetVariableDeclaration)));

                renderHelper.CloseArgumentList();
            }
            
            renderHelper.OpenCodeBlock();                        
            Nodes.Render(renderHelper.StringBuilder, renderHelper.RenderOptions);
            renderHelper.CloseCodeBlock();            
        }

        private string GetVariableDeclaration(QueryVariableInfo source)
        {
            var name = source.Name?.Trim();

            if (name?.StartsWith("$") == false)
                name = $"${name}";

            var declaration = $"{name}: {source.TypeName?.Trim()}";

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(source.TypeName))
                throw new ArgumentException($"Invalid variable declaration '{declaration}'");

            return declaration;
        }
    }

    internal interface IQueryNode
    {
        string QueryName { get; set; }

        List<QueryVariableInfo> QueryVariables { get; }
    }

    internal class QueryVariableInfo
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
    }
}
