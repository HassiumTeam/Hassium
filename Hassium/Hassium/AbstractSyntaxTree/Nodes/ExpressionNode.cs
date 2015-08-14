using System;

namespace Hassium
{
    public static class ExpressionNode
    {
        public static AstNode Parse(Parser parser)
        {
            return ParseAdditive (parser);
        }

        private static AstNode ParseAdditive (Parser parser)
        {
            AstNode left = ParseMultiplicative(parser);

            if (parser.AcceptToken(TokenType.Operation, "+"))
            {
                AstNode right = ParseAdditive(parser);
                return new BinOpNode(BinaryOperation.Addition, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, "-"))
            {
                AstNode right = ParseAdditive(parser);
                return new BinOpNode(BinaryOperation.Subtraction, left, right);
            }
            else
            {
                return left;
            }
        }

        private static AstNode ParseMultiplicative (Parser parser)
        {
            AstNode left = ParseTerm(parser);
            
            if (parser.AcceptToken(TokenType.Operation, "*"))
            {
                AstNode right = ParseMultiplicative(parser);
                return new BinOpNode(BinaryOperation.Multiplication, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, "/"))
            {
                AstNode right = ParseMultiplicative(parser);
                return new BinOpNode(BinaryOperation.Division, left, right);
            }
            else
            {
                return left;
            }
        }

        private static AstNode ParseTerm (Parser parser)
        {
            if (parser.MatchToken(TokenType.Number))
            {
                return new NumberNode(Convert.ToDouble(parser.ExpectToken(TokenType.Number).Value));
            }
            else if (parser.AcceptToken(TokenType.Parentheses, "("))
            {
                AstNode statement = ExpressionNode.Parse(parser);
                parser.ExpectToken(TokenType.Parentheses, ")");
                return statement;
            }
            else
            {
                return new ExceptionNode("Unknown thing encountered in parser");
            }

        }
    }
}

