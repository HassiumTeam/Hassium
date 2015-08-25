using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Parser.Ast
{
    public class BreakNode : AstNode
    {
        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "break");
            parser.ExpectToken(TokenType.EndOfLine);
            return new BreakNode();
        }
    }
}
