namespace Hassium.Parser.Ast
{
    public class ArgListNode: AstNode
    {
        public static ArgListNode Parse(Hassium.Parser.Parser parser)
        {
            ArgListNode ret = new ArgListNode();
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

