using System;
using System.Text;

namespace MTecl.GraphQlClient.ObjectMapping.Rendering
{
    internal class RenderHelper
    {
        private readonly StringBuilder _sb;
        private int _lastIndent = 0;
        private readonly GraphQlQueryBuilder _builder;

        public RenderHelper(StringBuilder sb, GraphQlQueryBuilder builder)
        {
            _sb = sb;
            _builder = builder;
        }

        public RenderHelper(GraphQlQueryBuilder builder) : this(new StringBuilder(), builder) {}

        public StringBuilder StringBuilder => _sb;

        public GraphQlQueryBuilder Builder => _builder;

        public RenderHelper Indent(int level)
        {
            _lastIndent = level;
            _sb.Append(' ', level * 2);
            return this;
        }

        public RenderHelper Literal(string text)
        {
            _sb.Append(text);

            return this;
        }

        public RenderHelper OpenArgumentList() 
        {
            _sb.Append('(');            
            return this;            
        }

        public RenderHelper CloseArgumentList() 
        {
            _sb.Append(')');            
            return this;            
        }

        public RenderHelper Argument(string name, object value, Action<object, StringBuilder> customRenderer = null)
        {
            if (value == null)
                return this;

            if (_sb.Length > 0 && _sb[_sb.Length - 1] != '(')
                _sb.Append(", ");

            _sb.Append($"{name}: ");

            return Write(value, customRenderer);
        }

        public RenderHelper Write(object value, Action<object, StringBuilder> customRenderer = null)
        {
            if (customRenderer != null)
            {
                customRenderer(value, _sb);
            }
            if (value is IRenderMyself rmsf)
            {
                rmsf.Render(_lastIndent, _sb);
            }
            else
            {
                _sb.Append(GqlObjectSerializer.Serialize(value, _builder));
            }
            
            return this;
        }

        public RenderHelper CrLf()
        {
            _sb.AppendLine();
            return this;
        }

        public RenderHelper OpenCodeBlock()
        {
            _sb.AppendLine(" {");
            return this;
        }

        public RenderHelper CloseCodeBlock()
        {            
            _sb.AppendLine("}");
            return this;
        }        
    }

}
