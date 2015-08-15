using System;

namespace Hassium
{
    public class StatementNode: AstNode
    {
        public static AstNode Parse(Parser parser)
        {
            /*if (parser.MatchToken(TokenType.Identifier, "if"))
            {
                //TODO: the thing
            }
            else */
            if (parser.MatchToken(TokenType.Bracket, "{"))
            {
                return CodeBlock.Parse(parser);
            }
            else
            {
                AstNode expr = ExpressionNode.Parse(parser);
                parser.ExpectToken(TokenType.EndOfLine);
                return expr;
            }
        }
    }
}

