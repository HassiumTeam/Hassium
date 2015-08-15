using System;

namespace Hassium
{
    public static class ExpressionNode
    {
        public static AstNode Parse(Parser parser)
        {
            return ParseAssignment(parser);
        }


        private static AstNode ParseAssignment (Parser parser)
        {
            AstNode left = ParseAdditive(parser);

            if (parser.AcceptToken(TokenType.Store))
            {
                AstNode right = ParseAssignment(parser);
                return new BinOpNode(BinaryOperation.Assignment, left, right);
            }
            else
            {
                return left;
            }
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
            AstNode left = ParseFunctionCall(parser);
            
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

        private static AstNode ParseFunctionCall (Parser parser)
        {
            return ParseFunctionCall(parser, ParseTerm(parser));
        }
        private static AstNode ParseFunctionCall(Parser parser, AstNode left)
        {
            if (parser.AcceptToken(TokenType.Parentheses, "("))
            {
                return ParseFunctionCall(parser, new FunctionCallNode(left, ArgListNode.Parse(parser)));
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
            else if (parser.MatchToken(TokenType.String))
            {
                return new StringNode(parser.ExpectToken(TokenType.String).Value);
            }
            else if (parser.MatchToken(TokenType.Identifier))
            {
                return new IdentifierNode(parser.ExpectToken(TokenType.Identifier).Value);
            }
            else
            {
                throw new Exception("Unexpected in Parser");
            }

        }
    }
}

