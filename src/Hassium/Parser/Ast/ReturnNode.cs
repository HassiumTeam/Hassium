using System;

namespace Hassium
{
    public class ReturnNode: AstNode
    {
        public AstNode Value { get; private set; }

        public ReturnNode(AstNode value)
        {
            this.Value = value;
        }

        

        public static AstNode Parse(Parser.Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "return");

            return parser.AcceptToken(TokenType.EndOfLine, ";") ? new ReturnNode(null) : new ReturnNode(StatementNode.Parse(parser));
        }
    }
}

