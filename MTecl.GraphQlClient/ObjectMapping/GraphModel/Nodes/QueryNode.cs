﻿using MTecl.GraphQlClient.ObjectMapping.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes
{
    internal class QueryNode : NodeBase, IQueryNode
    {
        public override string NodeId => $"QUERY";

        public override bool IsImportant => true;

        public QueryVariables QueryVariables { get; } = new QueryVariables();
        
        public string QueryName { get; set; }

        public string NodeTypeSymbol { get; set; } = "query";

        protected override void Render(RenderHelper renderHelper)
        {
            renderHelper.Literal(NodeTypeSymbol).Literal(" ");

            if (!string.IsNullOrWhiteSpace(QueryName))
                renderHelper.Literal(QueryName);

            if (QueryVariables.Count > 0)
            {
                renderHelper.OpenArgumentList();

                renderHelper.Literal(string.Join(", ", QueryVariables.Select(GetVariableDeclaration)));

                renderHelper.CloseArgumentList();
            }
            
            renderHelper.OpenCodeBlock();                        
            Nodes.Render(renderHelper.StringBuilder, renderHelper.Builder);
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

        QueryVariables QueryVariables { get; }
    }

    internal class QueryVariableInfo
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
    }

    internal class QueryVariables : IEnumerable<QueryVariableInfo>
    {
        private readonly List<QueryVariableInfo> _variables = new List<QueryVariableInfo>();

        public void Add(QueryVariableInfo v)
        {
            var existing = _variables.FirstOrDefault(e => e.Name.Equals(v.Name, StringComparison.InvariantCultureIgnoreCase));

            if (existing != null)
            {
                if (existing.TypeName.TrimEnd('!') != v.TypeName.TrimEnd('!'))
                    throw new ArgumentException($"Cannot use two variables with name {v.Name} with different types ({existing.TypeName} x {v.TypeName})");

                return;
            }

            _variables.Add(v);
        }

        public int Count => _variables.Count;

        public IEnumerator<QueryVariableInfo> GetEnumerator() => _variables.GetEnumerator();
        
        IEnumerator IEnumerable.GetEnumerator() => _variables.GetEnumerator();
    }
}
