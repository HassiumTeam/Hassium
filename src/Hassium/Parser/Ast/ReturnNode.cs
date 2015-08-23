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

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier);

            return new ReturnNode(StatementNode.Parse(parser));
        }
    }
}

