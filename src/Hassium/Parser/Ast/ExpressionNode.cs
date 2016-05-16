using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class ExpressionNode: AstNode
    {
        public ExpressionNode(SourceLocation location)
        {
            this.SourceLocation = location;
        }

        public static AstNode Parse(Parser parser)
        {
            return parseAssignment(parser);
        }

        private static AstNode parseAssignment(Parser parser)
        {
            AstNode left = parseConditional(parser);

            if (parser.AcceptToken(TokenType.Assignment, "="))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, parseAssignment(parser), parser.Location);
            else if (parser.AcceptToken(TokenType.Assignment, "+="))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(BinaryOperation.Addition, left, parseAssignment(parser), parser.Location), parser.Location);
            else if (parser.AcceptToken(TokenType.Assignment, "-="))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(BinaryOperation.Subtraction, left, parseAssignment(parser), parser.Location), parser.Location);
            else if (parser.AcceptToken(TokenType.Assignment, "*="))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(BinaryOperation.Multiplication, left, parseAssignment(parser), parser.Location), parser.Location);
            else if (parser.AcceptToken(TokenType.Assignment, "/="))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(BinaryOperation.Division, left, parseAssignment(parser), parser.Location), parser.Location);
            else if (parser.AcceptToken(TokenType.Assignment, "%="))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(BinaryOperation.Modulus, left, parseAssignment(parser), parser.Location), parser.Location);
            else if (parser.AcceptToken(TokenType.Assignment, "^="))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(BinaryOperation.XOR, left, parseAssignment(parser), parser.Location), parser.Location);
            else if (parser.AcceptToken(TokenType.Assignment, "|="))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(BinaryOperation.OR, left, parseAssignment(parser), parser.Location), parser.Location);
            else if (parser.AcceptToken(TokenType.Assignment, "&="))
                return new BinaryOperationNode(BinaryOperation.Assignment, left, new BinaryOperationNode(BinaryOperation.XAnd, left, parseAssignment(parser), parser.Location), parser.Location);
            else if (parser.AcceptToken(TokenType.BinaryOperation, "<->"))
                return new BinaryOperationNode(BinaryOperation.Swap, left, parseAssignment(parser), parser.Location);
            else
                return left;
        }

        private static AstNode parseConditional(Parser parser)
        {
            AstNode left = parseLogicalOr(parser);

            while (parser.AcceptToken(TokenType.Question))
            {
                var ifBody = parseConditional(parser);
                parser.ExpectToken(TokenType.Colon);
                var elseBody = parseConditional(parser);
                left = new TernaryOperationNode(left, ifBody, elseBody, parser.Location);
            }

            return left;
        }

        private static AstNode parseLogicalOr(Parser parser)
        {
            AstNode left = parseLogicalAnd(parser);
            while (parser.MatchToken(TokenType.BinaryOperation))
            {
                switch (parser.GetToken().Value)
                {
                    case "||":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.LogicalOr, left, parseLogicalAnd(parser), parser.Location);
                        continue;
                    case "??":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.NullCoalescing, left, parseLogicalAnd(parser), parser.Location);
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseLogicalAnd(Parser parser)
        {
            AstNode left = parseEquality(parser);
            while (parser.MatchToken(TokenType.BinaryOperation))
            {
                switch (parser.GetToken().Value)
                {
                    case "&&":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.LogicalAnd, left, parseEquality(parser), parser.Location);
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseEquality(Parser parser)
        {
            AstNode left = parseIn(parser);
            while (parser.MatchToken(TokenType.Comparison))
            {
                switch (parser.GetToken().Value)
                {
                    case "!=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.NotEqualTo, left, parseIn(parser), parser.Location);
                        continue;
                    case "==":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.EqualTo, left, parseIn(parser), parser.Location);
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseIn(Parser parser)
        {
            AstNode left = parseComparison(parser);
            while (parser.AcceptToken(TokenType.Identifier, "in"))
                left = new BinaryOperationNode(BinaryOperation.In, left, parseComparison(parser), parser.Location);
            return left;
        }

        private static AstNode parseComparison(Parser parser)
        {
            AstNode left = parseOr(parser);
            while (parser.MatchToken(TokenType.Comparison))
            {
                switch (parser.GetToken().Value)
                {
                    case ">":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.GreaterThan, left, parseOr(parser), parser.Location);
                        continue;
                    case "<":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.LesserThan, left, parseOr(parser), parser.Location);
                        continue;
                    case ">=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.GreaterThanOrEqual, left, parseOr(parser), parser.Location);
                        continue;
                    case "<=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.LesserThanOrEqual, left, parseOr(parser), parser.Location);
                        continue;
                    case "!=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.NotEqualTo, left, parseOr(parser), parser.Location);
                        continue;
                    case "==":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.EqualTo, left, parseOr(parser), parser.Location);
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseOr(Parser parser)
        {
            AstNode left = parseXor(parser);
            while (parser.AcceptToken(TokenType.BinaryOperation, "|"))
                left = new BinaryOperationNode(BinaryOperation.OR, left, parseXor(parser), parser.Location);
            return left;
        }
        private static AstNode parseXor(Parser parser)
        {
            AstNode left = parseAnd(parser);
            while (parser.AcceptToken(TokenType.BinaryOperation, "^"))
                left = new BinaryOperationNode(BinaryOperation.XOR, left, parseAnd(parser), parser.Location);
            return left;
        }
        private static AstNode parseAnd(Parser parser)
        {
            AstNode left = parseBitShift(parser);
            while (parser.AcceptToken(TokenType.BinaryOperation, "&"))
                left = new BinaryOperationNode(BinaryOperation.XAnd, left, parseBitShift(parser), parser.Location);
            return left;
        }

        private static AstNode parseBitShift(Parser parser)
        {
            AstNode left = parseAdditive(parser);
            while (parser.MatchToken(TokenType.BinaryOperation))
            {
                switch (parser.GetToken().Value)
                {
                    case "<<":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.BitShiftLeft, left, parseAdditive(parser), parser.Location);
                        continue;
                    case ">>":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.BitShiftRight, left, parseAdditive(parser), parser.Location);
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseAdditive(Parser parser)
        {
            AstNode left = parseMultiplicative(parser);
            while (parser.MatchToken(TokenType.BinaryOperation))
            {
                switch (parser.GetToken().Value)
                {
                    case "+":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Addition, left, parseMultiplicative(parser), parser.Location);
                        continue;
                    case "-":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Subtraction, left, parseMultiplicative(parser), parser.Location);
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseMultiplicative(Parser parser)
        {
            AstNode left = parseUnary(parser);
            while (parser.MatchToken(TokenType.BinaryOperation))
            {
                switch (parser.GetToken().Value)
                {
                    case "is":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Is, left, parseMultiplicative(parser), parser.Location);
                        continue;
                    case "*":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Multiplication, left, parseMultiplicative(parser), parser.Location);
                        continue;
                    case "**":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Power, left, parseMultiplicative(parser), parser.Location);
                        continue;
                    case "..":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Range, left, parseMultiplicative(parser), parser.Location);
                        continue;
                    case "/":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Division, left, parseMultiplicative(parser), parser.Location);
                        continue;
                    case "//":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.IntegerDivision, left, parseMultiplicative(parser), parser.Location);
                        continue;
                    case "%":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Modulus, left, parseMultiplicative(parser), parser.Location);
                        continue;
                     default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseUnary(Parser parser)
        {
            if (parser.MatchToken(TokenType.UnaryOperation))
            {
                switch (parser.GetToken().Value)
                {
                    case "!":
                        parser.ExpectToken(TokenType.UnaryOperation);
                        return new UnaryOperationNode(UnaryOperation.Not, parseUnary(parser), parser.Location);
                    case "++":
                        parser.ExpectToken(TokenType.UnaryOperation);
                        return new UnaryOperationNode(UnaryOperation.PreIncrement, parseUnary(parser), parser.Location);
                    case "--":
                        parser.ExpectToken(TokenType.UnaryOperation);
                        return new UnaryOperationNode(UnaryOperation.PreDecrement, parseUnary(parser), parser.Location);
                    case "~":
                        parser.ExpectToken(TokenType.UnaryOperation);
                        return new UnaryOperationNode(UnaryOperation.BitwiseComplement, parseUnary(parser), parser.Location);
                }
            }
            else if (parser.MatchToken(TokenType.BinaryOperation))
            {
                switch (parser.GetToken().Value)
                {
                    case "-":
                        parser.ExpectToken(TokenType.BinaryOperation);
                        return new UnaryOperationNode(UnaryOperation.Negate, parseUnary(parser), parser.Location);
                }
            }
            return parseAccess(parser);
        }

        private static AstNode parseAccess(Parser parser)
        {
            return parseAccess(parser, parseTerm(parser));
        }
        private static AstNode parseAccess(Parser parser, AstNode left)
        {
            if (parser.MatchToken(TokenType.LeftParentheses))
                return parseAccess(parser, new FunctionCallNode(left, ArgListNode.Parse(parser), parser.Location));
            else if (parser.AcceptToken(TokenType.LeftSquare))
            {
                /*if(parser.AcceptToken(TokenType.RightSquare))
                {
                    return parseAccess(parser, new ArrayAccessNode(left, null, parser.Location));
                }*/
                AstNode expression = Parse(parser);
                parser.ExpectToken(TokenType.RightSquare);
                return parseAccess(parser, new ArrayAccessNode(left, expression, parser.Location));
            }
            else if (parser.AcceptToken(TokenType.UnaryOperation, "++"))
                return new UnaryOperationNode(UnaryOperation.PostIncrement, left, parser.Location);
            else if (parser.AcceptToken(TokenType.UnaryOperation, "--"))
                return new UnaryOperationNode(UnaryOperation.PostDecrement, left, parser.Location);
            else if (parser.AcceptToken(TokenType.BinaryOperation, "."))
            {
                string identifier = parser.ExpectToken(TokenType.Identifier).Value;
                return parseAccess(parser, new AttributeAccessNode(left, identifier, parser.Location));
            }
            else
                return left;
        }

        private static AstNode parseTerm(Parser parser)
        {
            if (parser.AcceptToken(TokenType.Identifier, "new"))
                return new NewNode((FunctionCallNode)parseAccess(parser), parser.Location);
            else if (parser.AcceptToken(TokenType.Identifier, "this"))
                return new ThisNode(parser.Location);
            else if (parser.MatchToken(TokenType.Identifier, "true") || parser.MatchToken(TokenType.Identifier, "false"))
                return new BoolNode(parser.ExpectToken(TokenType.Identifier).Value, parser.Location);
            else if (parser.MatchToken(TokenType.Identifier, "lambda"))
                return LambdaNode.Parse(parser);
            else if (parser.MatchToken(TokenType.Identifier))
                return new IdentifierNode(parser.ExpectToken(TokenType.Identifier).Value, parser.Location);
            else if (parser.MatchToken(TokenType.Double))
                return new DoubleNode(Convert.ToDouble(parser.ExpectToken(TokenType.Double).Value), parser.Location);
            else if (parser.MatchToken(TokenType.Int64))
                return new Int64Node(Convert.ToInt64(parser.ExpectToken(TokenType.Int64).Value), parser.Location);
            else if (parser.MatchToken(TokenType.String))
                return new StringNode(parser.ExpectToken(TokenType.String).Value, parser.Location);
            else if (parser.MatchToken(TokenType.Char))
                return new CharNode(parser.ExpectToken(TokenType.Char).Value, parser.Location);
            else if (parser.AcceptToken(TokenType.LeftParentheses))
            {
                AstNode expression = Parse(parser);
                if (parser.AcceptToken(TokenType.Comma))
                    return TupleNode.Parse(parser, expression);
                parser.ExpectToken(TokenType.RightParentheses);
                return expression;
            }
            else if (parser.AcceptToken(TokenType.LeftBrace))
            {
                CodeBlockNode block = new CodeBlockNode(parser.Location);
                while (!parser.AcceptToken(TokenType.RightBrace))
                {
                    block.Children.Add(StatementNode.Parse(parser));
                    parser.AcceptToken(TokenType.Semicolon);
                }
                return block;
            }
            else if (parser.MatchToken(TokenType.LeftSquare))
                return ArrayDeclarationNode.Parse(parser);
            else if (parser.AcceptToken(TokenType.Semicolon))
                return new StatementNode(parser.Location);
            else
                throw new ParserException(string.Format("Unexpected type {0} with value {1} encountered in parser!", parser.GetToken().TokenType, parser.GetToken().Value), parser.Location);
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode child in Children)
                child.Visit(visitor);
        }
    }
}

