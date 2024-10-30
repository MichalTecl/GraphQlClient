using MTecl.GraphQlClient.ObjectMapping.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTecl.GraphQlClient.UnitTests
{
    public class RenderHelperTests
    {
        [Fact]
        public void Constructor_InitializesWithEmptyStringBuilder()
        {
            // Act
            var renderHelper = new RenderHelper(RenderOptions.Default);

            // Assert
            Assert.NotNull(renderHelper.StringBuilder);
            Assert.Equal(string.Empty, renderHelper.StringBuilder.ToString());
        }

        [Fact]
        public void Indent_AppendsCorrectIndentation()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.Indent(3);

            // Assert
            Assert.Equal("      ", sb.ToString()); // 6 spaces (3 levels)
        }

        [Fact]
        public void Literal_AppendsText()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.Literal("test");

            // Assert
            Assert.Equal("test", sb.ToString());
        }

        [Fact]
        public void OpenArgumentList_AppendsOpeningParenthesis()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.OpenArgumentList();

            // Assert
            Assert.Equal("(", sb.ToString());
        }

        [Fact]
        public void CloseArgumentList_AppendsClosingParenthesis()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.CloseArgumentList();

            // Assert
            Assert.Equal(")", sb.ToString());
        }

        [Fact]
        public void Argument_AppendsNameAndValue()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.OpenArgumentList().Argument("name", "value").CloseArgumentList();

            // Assert
            Assert.Equal("(name: \"value\")", sb.ToString());
        }

        [Fact]
        public void Argument_WithNullValue_DoesNothing()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.OpenArgumentList().Argument("name", null).CloseArgumentList();

            // Assert
            Assert.Equal("()", sb.ToString()); // No argument is added for null value
        }

        [Fact]
        public void CrLf_AppendsNewLine()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.CrLf();

            // Assert
            Assert.Equal(Environment.NewLine, sb.ToString());
        }

        [Fact]
        public void OpenCodeBlock_AppendsOpeningCurlyBraceAndNewLine()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.OpenCodeBlock();

            // Assert
            Assert.Equal(" {" + Environment.NewLine, sb.ToString());
        }

        [Fact]
        public void CloseCodeBlock_AppendsClosingCurlyBraceAndNewLine()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.CloseCodeBlock();

            // Assert
            Assert.Equal("}" + Environment.NewLine, sb.ToString());
        }

        [Fact]
        public void ObjectCanRenderItself()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.Write(new SelfRendered());


            // Assert
            Assert.Equal(SelfRendered.RENDERED, sb.ToString());
        }

        [Fact]
        public void ArgumentCanRenderItself()
        {
            // Arrange
            var sb = new StringBuilder();
            var renderHelper = new RenderHelper(sb, RenderOptions.Default);

            // Act
            renderHelper.Argument("a1", new SelfRendered());


            // Assert
            Assert.Equal($"a1: {SelfRendered.RENDERED}", sb.ToString());
        }
               
        private class SelfRendered : IRenderMyself
        {
            public const string RENDERED = "selfrendered";
            public void Render(int level, StringBuilder sb)
            {
                sb.Append(RENDERED);
            }
        }
    }
}
