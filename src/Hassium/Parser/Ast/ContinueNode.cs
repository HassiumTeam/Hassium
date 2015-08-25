using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Parser.Ast
{
    public class ContinueNode : AstNode
    {
        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "continue");
            parser.ExpectToken(TokenType.EndOfLine);
            return new ContinueNode();
        }
    }
}
