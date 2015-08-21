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
            AstNode left = ParseEquality(parser);

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

        private static AstNode ParseEquality (Parser parser)
        {
            AstNode left = ParseAdditive(parser);
            if (parser.AcceptToken(TokenType.Comparison, "="))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.Equals, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "!="))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.NotEqualTo, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "<"))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.LessThan, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, ">"))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.GreaterThan, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "<="))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.LesserOrEqual, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, ">="))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.GreaterOrEqual, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "&&"))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.And, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "||"))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.Or, left, right);
            }
            else if (parser.AcceptToken(TokenType.Xor, "^"))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.Xor, left, right);
            }
            else if (parser.AcceptToken(TokenType.Bitshift, "<<"))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.BitshiftLeft, left, right);
            }
            else if (parser.AcceptToken(TokenType.Bitshift, ">>"))
            {
                AstNode right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.BitshiftRight, left, right);
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
            AstNode left = ParseUnary(parser);
            
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
            else  if (parser.AcceptToken(TokenType.Modulus, "%"))
            {
                AstNode right = ParseMultiplicative(parser);
                return new BinOpNode(BinaryOperation.Modulus, left, right);
            }
            else
            {
                return left;
            }
        }

        private static AstNode ParseUnary(Parser parser)
        {
            if (parser.AcceptToken(TokenType.Not, "!"))
            {
                return new UnaryOpNode(UnaryOperation.Not, ParseUnary(parser));
            }
            else if (parser.AcceptToken(TokenType.Operation, "-"))
            {
                return new UnaryOpNode(UnaryOperation.Negate, ParseUnary(parser));
            }
            else if (parser.AcceptToken(TokenType.Complement, "~"))
            {
                return new UnaryOpNode(UnaryOperation.Complement, ParseUnary(parser));
            }
            else
            {
                return ParseFunctionCall(parser);
            }
        }

        private static AstNode ParseFunctionCall(Parser parser)
        {
            return ParseFunctionCall(parser, ParseTerm(parser));
        }

        private static AstNode ParseFunctionCall(Parser parser, AstNode left)
        {
            if (parser.AcceptToken(TokenType.Parentheses, "("))
            {
                return ParseFunctionCall(parser, new FunctionCallNode(left, ArgListNode.Parse(parser)));
            }
            else if (parser.AcceptToken(TokenType.Bracket, "["))
            {
                return ParseFunctionCall(parser, new ArrayGetNode(left, ArrayIndexerNode.Parse(parser)));
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
            else if (parser.AcceptToken(TokenType.Bracket, "["))
            {
                AstNode statement = ExpressionNode.Parse(parser);
                parser.ExpectToken(TokenType.Bracket, "]");
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
                throw new Exception("Unexpected in Parser: " + parser.CurrentToken().Value);
            }

        }
    }
}

