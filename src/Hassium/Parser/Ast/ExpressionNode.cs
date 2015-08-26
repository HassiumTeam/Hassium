using System;
using Hassium.Parser.Ast;

namespace Hassium
{
    public static class ExpressionNode
    {
        private static BinaryOperation GetBinaryOp(string value)
        {
            switch (value)
            {
                case "**":
                case "**=":
                    return BinaryOperation.Pow;
                case "//":
                case "//=":
                    return BinaryOperation.Root;
                case ">>":
                case ">>=":
                    return BinaryOperation.BitshiftRight;
                case "<<":
                case "<<=":
                    return BinaryOperation.BitshiftLeft;
                case "+":
                case "+=":
                    return BinaryOperation.Addition;
                case "-":
                case "-=":
                    return BinaryOperation.Subtraction;
                case "/":
                case "/=":
                    return BinaryOperation.Division;
                case "*":
                case "*=":
                    return BinaryOperation.Multiplication;
                case "%":
                case "%=":
                    return BinaryOperation.Modulus;
                case "&":
                case "&=":
                    return BinaryOperation.BitwiseAnd;
                case "|":
                case "|=":
                    return BinaryOperation.BitwiseOr;
                case "^":
                case "^=":
                    return BinaryOperation.Xor;
                case ":=":
                    return BinaryOperation.Assignment;
                case "=":
                    return BinaryOperation.Equals;
                case "!=":
                    return BinaryOperation.NotEqualTo;
                case ">":
                    return BinaryOperation.GreaterThan;
                case ">=":
                    return BinaryOperation.GreaterOrEqual;
                case "<":
                    return BinaryOperation.LessThan;
                case "<=":
                    return BinaryOperation.LesserOrEqual;
                case "&&":
                    return BinaryOperation.And;
                case "||":
                    return BinaryOperation.Or;
                default:
                    throw new ArgumentException("Invalid binary operation: " + value);
            }
        }


        public static AstNode Parse(Parser.Parser parser)
        {
            return ParseAssignment(parser);
        }


        private static AstNode ParseAssignment (Parser.Parser parser)
        {
            AstNode left = ParseEquality(parser);

            if (parser.AcceptToken(TokenType.Assignment))
            {
                AstNode right = ParseAssignment(parser);
                return new BinOpNode(BinaryOperation.Assignment, left, right);
            }
            else if (parser.AcceptToken(TokenType.OpAssign))
            {
                var assigntype = GetBinaryOp(parser.PreviousToken().Value.ToString());
                var right = ParseAssignment(parser);
                return new BinOpNode(BinaryOperation.Assignment, assigntype, left, right);
            }
            else
            {
                return left;
            }
        }

        private static AstNode ParseEquality (Parser.Parser parser)
        {
            AstNode left = ParseAdditive(parser);
            if (parser.AcceptToken(TokenType.Comparison, "="))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.Equals, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "!="))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.NotEqualTo, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "<"))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.LessThan, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, ">"))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.GreaterThan, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "<="))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.LesserOrEqual, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, ">="))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.GreaterOrEqual, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "&&"))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.And, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "&&"))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.And, left, right);
            }
            else if (parser.AcceptToken(TokenType.Comparison, "||"))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.Or, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, "^"))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.Xor, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, "<<"))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.BitshiftLeft, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, ">>"))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.BitshiftRight, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, "&"))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.BitwiseAnd, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, "|"))
            {
                var right = ParseEquality(parser);
                return new BinOpNode(BinaryOperation.BitwiseOr, left, right);
            }
            else
            {
                return left;
            }
        }

        private static AstNode ParseAdditive (Parser.Parser parser)
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

        private static AstNode ParseMultiplicative (Parser.Parser parser)
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
            else  if (parser.AcceptToken(TokenType.Operation, "%"))
            {
                AstNode right = ParseMultiplicative(parser);
                return new BinOpNode(BinaryOperation.Modulus, left, right);
            }
            else
            {
                return left;
            }
        }

        private static AstNode ParseUnary(Parser.Parser parser)
        {
            if (parser.AcceptToken(TokenType.UnaryOperation, "!"))
                return new UnaryOpNode(UnaryOperation.Not, ParseUnary(parser));
            else if (parser.AcceptToken(TokenType.MentalOperation, "++"))
                return new MentalNode("++", parser.ExpectToken(TokenType.Identifier).Value.ToString());
            else if (parser.AcceptToken(TokenType.MentalOperation, "--"))
                return new MentalNode("--", parser.ExpectToken(TokenType.Identifier).Value.ToString());
            else if (parser.AcceptToken(TokenType.Operation, "-"))
                return new UnaryOpNode(UnaryOperation.Negate, ParseUnary(parser));
            else if (parser.AcceptToken(TokenType.UnaryOperation, "~"))
                return new UnaryOpNode(UnaryOperation.Complement, ParseUnary(parser));
            else
                return ParseFunctionCall(parser);
        }

        private static AstNode ParseFunctionCall(Parser.Parser parser)
        {
            return ParseFunctionCall(parser, ParseTerm(parser));
        }

        private static AstNode ParseFunctionCall(Parser.Parser parser, AstNode left)
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

        private static AstNode ParseTerm (Parser.Parser parser)
        {
            if (parser.MatchToken(TokenType.Number))
            {
                return new NumberNode(Convert.ToDouble(parser.ExpectToken(TokenType.Number).Value));
            }
            else if (parser.AcceptToken(TokenType.Parentheses, "("))
            {
                AstNode statement = Parse(parser);
                parser.ExpectToken(TokenType.Parentheses, ")");
                return statement;
            }
            else if (parser.AcceptToken(TokenType.Bracket, "["))
            {
                AstNode statement = ArrayInitializerNode.Parse(parser);
                parser.ExpectToken(TokenType.Bracket, "]");
                return statement;
            }
            else if (parser.MatchToken(TokenType.String))
            {
                return new StringNode(parser.ExpectToken(TokenType.String).Value.ToString());
            }
            else if (parser.MatchToken(TokenType.Identifier))
            {
                return new IdentifierNode(parser.ExpectToken(TokenType.Identifier).Value.ToString());
            }
            else
            {
                throw new Exception("Unexpected in Parser: " + parser.CurrentToken().Value);
            }

        }
    }
}

