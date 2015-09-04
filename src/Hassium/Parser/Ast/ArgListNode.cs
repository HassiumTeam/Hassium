using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class ArgListNode: AstNode
    {
        private ArgListNode(int position) : base(position)
        {
        }

        public static ArgListNode Parse(Parser parser)
        {
            ArgListNode ret = new ArgListNode(parser.codePos);
            //parser.ExpectToken(TokenType.Parentheses, "(");

            while (!parser.MatchToken(TokenType.Parentheses, ")"))
            {
                ret.Children.Add(ExpressionNode.Parse(parser));
                if (!parser.AcceptToken(TokenType.Comma))
                {
                    break;
                }
            }
            parser.ExpectToken(TokenType.Parentheses, ")");

            return ret;
        }
    }
}

