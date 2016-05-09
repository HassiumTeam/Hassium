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
            AstNode expression = parseAssignment(parser);
            if (parser.AcceptToken(TokenType.Question))
            {
                AstNode ternaryTrue = Parse(parser);
                parser.ExpectToken(TokenType.Colon);
                AstNode ternaryFalse = Parse(parser);
                expression = new TernaryOperationNode(expression, ternaryTrue, ternaryFalse, parser.Location);
            }
            else if (parser.AcceptToken(TokenType.Colon))
                return new KeyValuePairNode(expression, ExpressionNode.Parse(parser), parser.Location);
            return expression;
        }

        private static AstNode parseAssignment(Parser parser)
        {
            AstNode left = parseAndOr(parser);

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

        private static AstNode parseAndOr(Parser parser)
        {
            AstNode left = parseOr(parser);
            while (parser.MatchToken(TokenType.BinaryOperation))
            {
                switch (parser.GetToken().Value)
                {
                    case "||":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.LogicalOr, left, parseOr(parser), parser.Location);
                        continue;
                    case "&&":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.LogicalAnd, left, parseOr(parser), parser.Location);
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
            AstNode left = parseComparison(parser);
            while (parser.AcceptToken(TokenType.BinaryOperation, "&"))
                left = new BinaryOperationNode(BinaryOperation.XAnd, left, parseComparison(parser), parser.Location);
            return left;
        }

        private static AstNode parseComparison(Parser parser)
        {
            AstNode left = parseAdditive(parser);
            while (parser.MatchToken(TokenType.Comparison))
            {
                switch (parser.GetToken().Value)
                {
                    case ">":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.GreaterThan, left, parseAdditive(parser), parser.Location);
                        continue;
                    case "<":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.LesserThan, left, parseAdditive(parser), parser.Location);
                        continue;
                    case ">=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.GreaterThanOrEqual, left, parseAdditive(parser), parser.Location);
                        continue;
                    case "<=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.LesserThanOrEqual, left, parseAdditive(parser), parser.Location);
                        continue;
                    case "!=":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.NotEqualTo, left, parseAdditive(parser), parser.Location);
                        continue;
                    case "==":
                        parser.AcceptToken(TokenType.Comparison);
                        left = new BinaryOperationNode(BinaryOperation.EqualTo, left, parseAdditive(parser), parser.Location);
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
            AstNode left = parseMultiplicitive(parser);
            while (parser.MatchToken(TokenType.BinaryOperation))
            {
                switch (parser.GetToken().Value)
                {
                    case "+":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Addition, left, parseMultiplicitive(parser), parser.Location);
                        continue;
                    case "-":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Subtraction, left, parseMultiplicitive(parser), parser.Location);
                        continue;
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private static AstNode parseMultiplicitive(Parser parser)
        {
            AstNode left = parseUnary(parser);
            while (parser.MatchToken(TokenType.BinaryOperation) || parser.MatchToken(TokenType.Colon))
            {
                switch (parser.GetToken().Value)
                {
                    case "*":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Multiplication, left, parseMultiplicitive(parser), parser.Location);
                        continue;
                    case "**":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Power, left, parseMultiplicitive(parser), parser.Location);
                        continue;
                    case "/":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Division, left, parseMultiplicitive(parser), parser.Location);
                        continue;
                    case "%":
                        parser.AcceptToken(TokenType.BinaryOperation);
                        left = new BinaryOperationNode(BinaryOperation.Modulus, left, parseMultiplicitive(parser), parser.Location);
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

