using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class IdentifierNode: AstNode
    {
        public string Identifier { get; private set; }

        public IdentifierNode(int position, string value) : base(position)
        {
            Identifier = value;
        }

        public override string ToString()
        {
            return Identifier;
        }

        public static AstNode Parse(Hassium.Parser.Parser parser)
        {
            return new IdentifierNode(parser.codePos, parser.ExpectToken(TokenType.Identifier).Value.ToString());
        }
    }
}

