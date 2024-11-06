using MTecl.GraphQlClient.ObjectMapping.Rendering;
using MTecl.GraphQlClient.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.GraphModel.Nodes
{
    internal class FieldNode : NodeBase
    {
        public string Name { get; set; }

        public string IsAliasFor { get; set; }

        public List<KeyValuePair<string, object>> Arguments { get; } = new List<KeyValuePair<string, object>>();

        public override string NodeId => Name;

        public override bool IsImportant => Arguments.Count > 0 || Nodes.Count > 0;

        protected override void Render(RenderHelper renderHelper)
        {
            renderHelper.Indent(Level);
                        
            if (!string.IsNullOrEmpty(IsAliasFor))
            {            
                renderHelper.Literal(NamingConventionHelper.ConvertName(Name, renderHelper.Builder)).Literal(": ").Literal(IsAliasFor);
            }
            else
            {
                renderHelper.Literal(NamingConventionHelper.ConvertName(Name, renderHelper.Builder));
            }

            if (Arguments.Count > 0) 
            {
                renderHelper.OpenArgumentList();
                
                foreach (var arg in Arguments)
                    renderHelper.Argument(arg.Key, arg.Value);
                
                renderHelper.CloseArgumentList();
            }


            if (Nodes.Count > 0)
            {
                renderHelper.OpenCodeBlock();
                foreach (var child in Nodes.Filtered)
                    child.Render(renderHelper.StringBuilder, renderHelper.Builder);
                renderHelper.Indent(Level).CloseCodeBlock();
            }
            else
                renderHelper.CrLf();
        }
    }
}
