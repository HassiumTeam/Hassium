using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class BreakNode : AstNode
    {
        private BreakNode(int codePos) : base(codePos)
        {
        }

        public static AstNode Parse(Parser parser)
        {
            int pos = parser.codePos;
            parser.ExpectToken(TokenType.Identifier, "break");
            parser.ExpectToken(TokenType.EndOfLine);
            return new BreakNode(pos);
        }
    }
}
