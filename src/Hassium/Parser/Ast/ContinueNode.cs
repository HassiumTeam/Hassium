using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class ContinueNode : AstNode
    {
        private ContinueNode(int codePos) : base(codePos)
        {
        }

        public static AstNode Parse(Parser parser)
        {
            int pos = parser.codePos;
            parser.ExpectToken(TokenType.Identifier, "continue");
            parser.ExpectToken(TokenType.EndOfLine);
            return new ContinueNode(pos);
        }
    }
}
