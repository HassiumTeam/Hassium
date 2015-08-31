using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Parser.Ast
{
    public class InstanceNode : AstNode
    {
        public AstNode Target
        {
            get { return this.Children[0]; }
        }

        public InstanceNode(AstNode value)
        {
            this.Children.Add(value);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "new");
            var target = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.EndOfLine);
            return new InstanceNode(target);
        }
    }
}
