using System;

namespace Hassium
{
    public class IdentifierNode: AstNode
    {
        public string Identifier { get; private set; }

        public IdentifierNode(string value)
        {
            Identifier = value;
        }

        public override string ToString()
        {
            return Identifier;
        }

        public static AstNode Parse(Parser.Parser parser)
        {
            return new IdentifierNode(parser.ExpectToken(TokenType.Identifier).Value.ToString());
        }
    }
}

