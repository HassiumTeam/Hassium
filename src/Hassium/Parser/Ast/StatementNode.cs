using System;

namespace Hassium
{
    public class StatementNode: AstNode
    {
        public static AstNode Parse(Parser.Parser parser)
        {
            if (parser.MatchToken(TokenType.Identifier, "if"))
                return IfNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "while"))
                return WhileNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "for"))
                return ForNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "foreach"))
                return ForEachNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "try"))
                return TryNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "func"))
                return FuncNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "thread"))
                return ThreadNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier, "return"))
                return ReturnNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Brace, "{"))
                return CodeBlock.Parse(parser);
            else
            {
                AstNode expr = ExpressionNode.Parse(parser);
                parser.ExpectToken(TokenType.EndOfLine);
                return expr;
            }
        }
    }
}

