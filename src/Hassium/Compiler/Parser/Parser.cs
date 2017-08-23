using System;
using System.Collections.Generic;
using System.Text;

using Hassium.Compiler.Exceptions;
using Hassium.Compiler.Lexer;
using Hassium.Compiler.Parser.Ast;

namespace Hassium.Compiler.Parser
{
    public class Parser
    {
        private List<Token> tokens;
        private int position;
        private bool endOfStream { get { return position >= tokens.Count; } }

        private SourceLocation location
        {
            get
            {
                try
                {
                    return tokens[position].SourceLocation;
                }
                catch
                {
                    return tokens[position - 1].SourceLocation;
                }
            }
        }

        public AstNode Parse(List<Token> tokens)
        {
            this.tokens = tokens;
            position = 0;

            CodeBlockNode ast = new CodeBlockNode(location);
            while (!endOfStream)
                ast.Children.Add(parseStatement());
            return ast;
        }

        private AstNode parseStatement()
        {
            if (acceptToken(TokenType.Identifier, "break"))
                return new BreakNode(location);
            else if (acceptToken(TokenType.Identifier, "continue"))
                return new ContinueNode(location);
            else if (matchToken(TokenType.Identifier, "class"))
                return parseClassDeclaration();
            else if (matchToken(TokenType.Identifier, "do"))
                return parseDoWhile();
            else if (matchToken(TokenType.Identifier, "enum"))
                return parseEnum();
            else if (matchToken(TokenType.Identifier, "for"))
                return parseFor();
            else if (matchToken(TokenType.Identifier, "foreach"))
                return parseForeach();
            else if (matchToken(TokenType.Identifier, "from"))
                return parseFrom();
            else if (matchToken(TokenType.Identifier, "func"))
                return parseFunctionDeclaration();
            else if (matchToken(TokenType.Identifier, "if"))
                return parseIf();
            else if (matchToken(TokenType.Identifier, "priv"))
                return parsePriv();
            else if (matchToken(TokenType.Identifier, "raise"))
                return parseRaise();
            else if (matchToken(TokenType.Identifier, "return"))
                return parseReturn();
            else if (matchToken(TokenType.Identifier, "switch"))
                return parseSwitch();
            else if (matchToken(TokenType.Identifier, "trait"))
                return parseTrait();
            else if (matchToken(TokenType.Identifier, "try"))
                return parseTryCatch();
            else if (matchToken(TokenType.Identifier, "use"))
                return parseUse();
            else if (matchToken(TokenType.Identifier, "while"))
                return parseWhile();
            else if (position + 1 < tokens.Count)
            {
                if (matchToken(TokenType.Identifier) && tokens[position + 1].TokenType == TokenType.Comma)
                    return parseMultipleAssignment();
                else if (matchToken(TokenType.OpenCurlyBrace))
                    return parseCodeBlockNode();
                else
                    return parseExpressionStatement();
            }
            else if (matchToken(TokenType.OpenCurlyBrace))
                return parseCodeBlockNode();
            else
                return parseExpressionStatement();
        }

        private ArgumentListNode parseArgumentList()
        {
            var location = this.location;

            expectToken(TokenType.OpenParentheses);
            List<AstNode> parameters = new List<AstNode>();
            while (!acceptToken(TokenType.CloseParentheses))
            {
                parameters.Add(parseExpression());
                acceptToken(TokenType.Comma);
            }

            return new ArgumentListNode(location, parameters);
        }

        private ClassDeclarationNode parseClassDeclaration()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "class");
            string name = expectToken(TokenType.Identifier).Value;
            if (acceptToken(TokenType.Colon))
            {
                var inherits = new List<AstNode>();
                do
                {
                    inherits.Add(parseExpression());
                } while (acceptToken(TokenType.Comma));
                return new ClassDeclarationNode(location, name, parseStatement(), inherits);
            }
            return new ClassDeclarationNode(location, name, parseStatement());
        }

        private CodeBlockNode parseCodeBlockNode()
        {
            var location = this.location;
            expectToken(TokenType.OpenCurlyBrace);
            var block = new CodeBlockNode(location);
            while (!acceptToken(TokenType.CloseCurlyBrace))
                block.Children.Add(parseStatement());
            return block;
        }

        private DictionaryDeclarationNode parseDictionaryDeclaration()
        {
            var location = this.location;
            var keys = new List<AstNode>();
            var values = new List<AstNode>();
            expectToken(TokenType.OpenCurlyBrace);
            while (!acceptToken(TokenType.CloseCurlyBrace))
            {
                keys.Add(parseExpression());
                expectToken(TokenType.Colon);
                values.Add(parseExpression());
                acceptToken(TokenType.Comma);
            }

            return new DictionaryDeclarationNode(location, keys, values);
        }

        private DoWhileNode parseDoWhile()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "do");
            AstNode body = parseStatement();
            expectToken(TokenType.Identifier, "while");
            AstNode condition = parseStatement();

            return new DoWhileNode(location, condition, body);
        }

        private EnumNode parseEnum()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "enum");
            string name = expectToken(TokenType.Identifier).Value;
            EnumNode enum_ = new EnumNode(location, name);
            expectToken(TokenType.OpenCurlyBrace);
            int count = 0;
            while (!acceptToken(TokenType.CloseCurlyBrace))
            {
                int num = count++;
                string attrib = expectToken(TokenType.Identifier).Value;
                if (acceptToken(TokenType.Assignment))
                    num = Convert.ToInt32(expectToken(TokenType.Integer).Value);
                acceptToken(TokenType.Comma);
                enum_.Attributes.Add(num, attrib);
            }

            return enum_;
        }

        private ForNode parseFor()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "for");
            acceptToken(TokenType.OpenParentheses);
            AstNode initialStatement = parseStatement();
            acceptToken(TokenType.Semicolon);
            AstNode condition = parseExpression();
            acceptToken(TokenType.Semicolon);
            AstNode repeatStatement = parseStatement();
            acceptToken(TokenType.Semicolon);
            acceptToken(TokenType.CloseParentheses);
            AstNode body = parseStatement();

            return new ForNode(location, initialStatement, condition, repeatStatement, body);
        }

        private ForeachNode parseForeach()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "foreach");
            acceptToken(TokenType.OpenParentheses);
            string variable = expectToken(TokenType.Identifier).Value;
            expectToken(TokenType.Identifier, "in");
            AstNode expression = parseExpression();
            acceptToken(TokenType.CloseParentheses);
            AstNode body = parseStatement();

            return new ForeachNode(location, variable, expression, body);
        }

        private UseFromNode parseFrom()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "from");

            var first = new StringBuilder();

            while (matchToken(TokenType.Dot) || matchToken(TokenType.Operation, "/"))
                first.Append(tokens[position++].Value);
            do
            {
                first.Append(expectToken(TokenType.Identifier).Value);
                if (matchToken(TokenType.Dot) || matchToken(TokenType.Operation, "/"))
                    first.Append("/");
            }
            while (acceptToken(TokenType.Dot) || acceptToken(TokenType.Operation, "/"));

            expectToken(TokenType.Identifier, "use");

            var second = new StringBuilder();
            if (matchToken(TokenType.Operation, "*"))
                second.Append(expectToken(TokenType.Operation, "*").Value);
            else
            {
                while (matchToken(TokenType.Dot) || matchToken(TokenType.Operation, "/"))
                    second.Append(tokens[position++].Value);
                do
                {
                    second.Append(expectToken(TokenType.Identifier).Value);
                    if (matchToken(TokenType.Dot) || matchToken(TokenType.Operation, "/"))
                        second.Append("/");
                }
                while (acceptToken(TokenType.Dot) || acceptToken(TokenType.Operation, "/"));
            }

            return new UseFromNode(location, second.ToString(), first.ToString());
        }

        private FunctionDeclarationNode parseFunctionDeclaration()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "func");
            string name = expectToken(TokenType.Identifier).Value;
            var parameters = new List<FunctionParameter>();
            expectToken(TokenType.OpenParentheses);
            while (!acceptToken(TokenType.CloseParentheses))
            {
                var paramType = FunctionParameterType.Normal;
                if (acceptToken(TokenType.Identifier, "params"))
                    paramType = FunctionParameterType.Variadic;
                string paramName = expectToken(TokenType.Identifier).Value;
                if (acceptToken(TokenType.Colon))
                    parameters.Add(new FunctionParameter(FunctionParameterType.Enforced, paramName, parseExpression()));
                else
                    parameters.Add(new FunctionParameter(paramType, paramName));
                acceptToken(TokenType.Comma);
            }
            if (acceptToken(TokenType.Colon))
                return new FunctionDeclarationNode(location, name, parameters, parseExpression(), parseStatement());
            return new FunctionDeclarationNode(location, name, parameters, parseStatement());
        }

        private IfNode parseIf()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "if");
            var condition = parseExpression();
            var ifBody = parseStatement();
            if (acceptToken(TokenType.Identifier, "else"))
                return new IfNode(location, condition, ifBody, parseStatement());
            return new IfNode(location, condition, ifBody);
        }

        private LambdaNode parseLambda()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "lambda");
            var parameters = parseArgumentList();
            var body = parseStatement();
            return new LambdaNode(location, parameters, body);
        }

        private ListDeclarationNode parseListDeclaration()
        {
            var location = this.location;
            var elements = new List<AstNode>();
            expectToken(TokenType.OpenSquareBrace);
            while (!acceptToken(TokenType.CloseSquareBrace))
            {
                elements.Add(parseExpression());
                acceptToken(TokenType.Comma);
            }

            return new ListDeclarationNode(location, elements);
        }

        private MultipleAssignmentNode parseMultipleAssignment()
        {
            var location = this.location;
            List<AstNode> targets = new List<AstNode>();
            do
            {
                if (tokens[position + 1].TokenType == TokenType.Comma || tokens[position + 1].TokenType == TokenType.Assignment)
                    targets.Add(new IdentifierNode(this.location, expectToken(TokenType.Identifier).Value));
                else
                    targets.Add(parseExpression());
            }
            while (acceptToken(TokenType.Comma));
            expectToken(TokenType.Assignment);

            AstNode value = parseExpression();

            return new MultipleAssignmentNode(location, targets, value);
        }

        private AstNode parsePriv()
        {
            expectToken(TokenType.Identifier, "priv");
            AstNode ast = parseStatement();
            ast.IsPrivate = true;
            return ast;
        }

        private RaiseNode parseRaise()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "raise");
            AstNode exception = parseExpression();

            return new RaiseNode(location, exception);
        }

        private ReturnNode parseReturn()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "return");
            AstNode value = parseExpression();

            return new ReturnNode(location, value);
        }

        private SwitchNode parseSwitch()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "switch");
            AstNode value = parseExpression();
            expectToken(TokenType.OpenCurlyBrace);
            Dictionary<AstNode, AstNode> cases = new Dictionary<AstNode, AstNode>();
            do
            {
                expectToken(TokenType.Identifier, "case");
                AstNode c = parseExpression();
                AstNode body = parseStatement();
                cases.Add(c, body);
            } while (!matchToken(TokenType.Identifier, "default") && !matchToken(TokenType.CloseCurlyBrace));
            AstNode _default;
            if (acceptToken(TokenType.Identifier, "default"))
                _default = parseStatement();
            else
                _default = new CodeBlockNode(this.location);
            expectToken(TokenType.CloseCurlyBrace);

            return new SwitchNode(location, cases, _default, value);
        }

        private TraitNode parseTrait()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "trait");
            string name = expectToken(TokenType.Identifier).Value;
            TraitNode trait = new TraitNode(location, name);

            expectToken(TokenType.OpenCurlyBrace);
            while (!acceptToken(TokenType.CloseCurlyBrace))
            {
                string attrib = expectToken(TokenType.Identifier).Value;
                expectToken(TokenType.Colon);
                AstNode type = new IdentifierNode(this.location, expectToken(TokenType.Identifier).Value);
                trait.Attributes.Add(attrib, type);
                acceptToken(TokenType.Comma);
            }

            return trait;
        }

        private TryCatchNode parseTryCatch()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "try");
            AstNode tryBody = parseStatement();
            expectToken(TokenType.Identifier, "catch");
            AstNode catchBody = parseStatement();

            return new TryCatchNode(location, tryBody, catchBody);
        }

        private TupleNode parseTuple(AstNode init)
        {
            var location = this.location;
            var elements = new List<AstNode>();
            elements.Add(init);
            acceptToken(TokenType.Comma);
            while (!acceptToken(TokenType.CloseParentheses))
            {
                elements.Add(parseExpression());
                acceptToken(TokenType.Comma);
            }

            return new TupleNode(location, elements);
        }

        private AstNode parseUse()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "use");

            var first = new StringBuilder();
            if (matchToken(TokenType.Operation, "*"))
                first.Append(expectToken(TokenType.Operation, "*").Value);
            else
            {
                while (matchToken(TokenType.Dot) || matchToken(TokenType.Operation, "/"))
                    first.Append(tokens[position++].Value);
                do
                {
                    first.Append(expectToken(TokenType.Identifier).Value);
                    if (matchToken(TokenType.Dot) || matchToken(TokenType.Operation, "/"))
                        first.Append("/");
                }
                while (acceptToken(TokenType.Dot) || acceptToken(TokenType.Operation, "/"));
            }
            if (!acceptToken(TokenType.Identifier, "from"))
                return new UseNode(location, first.ToString());

            var second = new StringBuilder();
            while (matchToken(TokenType.Dot) || matchToken(TokenType.Operation, "/"))
                second.Append(tokens[position++].Value);
            do
            {
                second.Append(expectToken(TokenType.Identifier).Value);
                if (matchToken(TokenType.Dot) || matchToken(TokenType.Operation, "/"))
                    second.Append("/");
            }
            while (acceptToken(TokenType.Dot) || acceptToken(TokenType.Operation, "/"));
            
            return new UseFromNode(location, first.ToString(), second.ToString());
        }

        private WhileNode parseWhile()
        {
            var location = this.location;
            expectToken(TokenType.Identifier, "while");
            AstNode condition = parseExpression();
            AstNode body = parseStatement();

            return new WhileNode(location, condition, body);
        }

        private AstNode parseExpressionStatement()
        {
            var location = this.location;
            AstNode expression = parseExpression();
            acceptToken(TokenType.Semicolon);
            if (expression is FunctionCallNode || expression is BinaryOperationNode)
                return new ExpressionStatementNode(location, expression);
            if (expression is UnaryOperationNode)
                return new ExpressionStatementNode(location, expression);
            return expression;
        }

        private AstNode parseExpression()
        {
            return parseAssignment();
        }
        private AstNode parseAssignment()
        {
            var location = this.location;
            AstNode left = parseTernary();
            if (matchToken(TokenType.Assignment))
            {
                switch (tokens[position].Value)
                {
                    case "=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, parseAssignment());
                    case "+=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, new BinaryOperationNode(location, BinaryOperation.Addition, left, parseAssignment()));
                    case "-=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, new BinaryOperationNode(location, BinaryOperation.Subtraction, left, parseAssignment()));
                    case "*=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, new BinaryOperationNode(location, BinaryOperation.Multiplication, left, parseAssignment()));
                    case "/=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, new BinaryOperationNode(location, BinaryOperation.Division, left, parseAssignment()));
                    case "%=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, new BinaryOperationNode(location, BinaryOperation.Modulus, left, parseAssignment()));
                    case "<<=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, new BinaryOperationNode(location, BinaryOperation.BitshiftLeft, left, parseAssignment()));
                    case ">>=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, new BinaryOperationNode(location, BinaryOperation.BitshiftRight, left, parseAssignment()));
                    case "&=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, new BinaryOperationNode(location, BinaryOperation.LogicalAnd, left, parseAssignment()));
                    case "|=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, new BinaryOperationNode(location, BinaryOperation.LogicalOr, left, parseAssignment()));
                    case "^=":
                        acceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(location, BinaryOperation.Assignment, left, new BinaryOperationNode(location, BinaryOperation.BitwiseXor, left, parseAssignment()));
                    default:
                        break;
                }
            }
            return left;
        }

        private AstNode parseTernary()
        {
            var location = this.location;
            AstNode left = parseLogicalOr();

            while (acceptToken(TokenType.Question))
            {
                AstNode trueStatement = parseExpression();
                expectToken(TokenType.Colon);
                AstNode falseStatement = parseExpression();
                left = new TernaryOperationNode(location, left, trueStatement, falseStatement);
            }
            return left;
        }
        private AstNode parseLogicalOr()
        {
            var location = this.location;
            AstNode left = parseLogicalAnd();
            while (acceptToken(TokenType.Operation, "||"))
                left = new BinaryOperationNode(location, BinaryOperation.LogicalOr, left, parseLogicalOr());
            return left;
        }

        private AstNode parseLogicalAnd()
        {
            var location = this.location;
            AstNode left = parseEquality();
            while (acceptToken(TokenType.Operation, "&&"))
                left = new BinaryOperationNode(location, BinaryOperation.LogicalAnd, left, parseLogicalAnd());
            return left;
        }

        private AstNode parseEquality()
        {
            var location = this.location;
            AstNode left = parseComparison();
            AstNode expr;
            while (matchToken(TokenType.Comparison))
            {
                switch (tokens[position].Value)
                {
                    case "==":
                        acceptToken(TokenType.Comparison);
                        expr = parseComparison();
                        if (matchToken(TokenType.Comparison))
                            return new BinaryOperationNode(location, BinaryOperation.LogicalAnd, new BinaryOperationNode(location, BinaryOperation.EqualTo, left, expr), new BinaryOperationNode(location, stringToBinaryOperation(expectToken(TokenType.Comparison).Value), expr, parseOr()));
                        return new BinaryOperationNode(location, BinaryOperation.EqualTo, left, expr);
                    case "!=":
                        acceptToken(TokenType.Comparison);
                        expr = parseComparison();
                        if (matchToken(TokenType.Comparison))
                            return new BinaryOperationNode(location, BinaryOperation.LogicalAnd, new BinaryOperationNode(location, BinaryOperation.NotEqualTo, left, expr), new BinaryOperationNode(location, stringToBinaryOperation(expectToken(TokenType.Comparison).Value), expr, parseOr()));
                        return new BinaryOperationNode(location, BinaryOperation.NotEqualTo, left, expr);
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private AstNode parseComparison()
        {
            var location = this.location;
            AstNode left = parseOr();
            AstNode expr;
            while (matchToken(TokenType.Comparison))
            {
                switch (tokens[position].Value)
                {
                    case ">":
                        acceptToken(TokenType.Comparison);
                        expr = parseOr();
                        if (matchToken(TokenType.Comparison))
                            return new BinaryOperationNode(location, BinaryOperation.LogicalAnd, new BinaryOperationNode(location, BinaryOperation.GreaterThan, left, expr), new BinaryOperationNode(location, stringToBinaryOperation(expectToken(TokenType.Comparison).Value), expr, parseOr()));
                        return new BinaryOperationNode(location, BinaryOperation.GreaterThan, left, expr);
                    case ">=":
                        acceptToken(TokenType.Comparison);
                        expr = parseOr();
                        if (matchToken(TokenType.Comparison))
                            return new BinaryOperationNode(location, BinaryOperation.LogicalAnd, new BinaryOperationNode(location, BinaryOperation.GreaterThanOrEqual, left, expr), new BinaryOperationNode(location, stringToBinaryOperation(expectToken(TokenType.Comparison).Value), expr, parseOr()));
                        return new BinaryOperationNode(location, BinaryOperation.GreaterThanOrEqual, left, expr);
                    case "<":
                        acceptToken(TokenType.Comparison);
                        expr = parseOr();
                        if (matchToken(TokenType.Comparison))
                            return new BinaryOperationNode(location, BinaryOperation.LogicalAnd, new BinaryOperationNode(location, BinaryOperation.LesserThan, left, expr), new BinaryOperationNode(location, stringToBinaryOperation(expectToken(TokenType.Comparison).Value), expr, parseOr()));
                        return new BinaryOperationNode(location, BinaryOperation.LesserThan, left, expr);
                    case "<=":
                        acceptToken(TokenType.Comparison);
                        expr = parseOr();
                        if (matchToken(TokenType.Comparison))
                            return new BinaryOperationNode(location, BinaryOperation.LogicalAnd, new BinaryOperationNode(location, BinaryOperation.LesserThanOrEqual, left, expr), new BinaryOperationNode(location, stringToBinaryOperation(expectToken(TokenType.Comparison).Value), expr, parseOr()));
                        return new BinaryOperationNode(location, BinaryOperation.LesserThanOrEqual, left, expr);
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private AstNode parseOr()
        {
            var location = this.location;
            AstNode left = parseXor();
            while (acceptToken(TokenType.Operation, "|"))
                left = new BinaryOperationNode(location, BinaryOperation.BitwiseOr, left, parseOr());
            return left;
        }

        private AstNode parseXor()
        {
            var location = this.location;
            AstNode left = parseAnd();
            while (acceptToken(TokenType.Operation, "^"))
                left = new BinaryOperationNode(location, BinaryOperation.BitwiseXor, left, parseXor());
            return left;
        }

        private AstNode parseAnd()
        {
            var location = this.location;
            AstNode left = parseBitshift();
            while (acceptToken(TokenType.Operation, "&"))
                left = new BinaryOperationNode(location, BinaryOperation.BitwiseAnd, left, parseAnd());
            return left;
        }

        private AstNode parseBitshift()
        {
            var location = this.location;
            AstNode left = parseAdditive();
            while (matchToken(TokenType.Operation))
            {
                switch (tokens[position].Value)
                {
                    case "<<":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.BitshiftLeft, left, parseBitshift());
                    case ">>":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.BitshiftRight, left, parseBitshift());
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private AstNode parseAdditive()
        {
            var location = this.location;
            AstNode left = parseMultiplicative();
            while (matchToken(TokenType.Operation))
            {
                switch (tokens[position].Value)
                {
                    case "+":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.Addition, left, parseAdditive());
                    case "-":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.Subtraction, left, parseAdditive());
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private AstNode parseMultiplicative()
        {
            var location = this.location;
            AstNode left = parseUnary();
            while (matchToken(TokenType.Operation))
            {
                switch (tokens[position].Value)
                {
                    case "*":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.Multiplication, left, parseMultiplicative());
                    case "/":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.Division, left, parseMultiplicative());
                    case "%":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.Modulus, left, parseMultiplicative());
                    case "**":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.Power, left, parseMultiplicative());
                    case "//":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.IntegerDivision, left, parseMultiplicative());
                    case "??":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.NullCoalescing, left, parseMultiplicative());
                    case "is":
                        acceptToken(TokenType.Operation);
                        return new BinaryOperationNode(location, BinaryOperation.Is, left, parseMultiplicative());
                    default:
                        break;
                }
                break;
            }
            return left;
        }

        private AstNode parseUnary()
        {
            var location = this.location;
            if (matchToken(TokenType.Operation))
            {
                switch (tokens[position].Value)
                {
                    case "~":
                        acceptToken(TokenType.Operation);
                        return new UnaryOperationNode(location, parseUnary(), UnaryOperation.BitwiseNot);
                    case "!":
                        acceptToken(TokenType.Operation);
                        return new UnaryOperationNode(location, parseUnary(), UnaryOperation.LogicalNot);
                    case "-":
                        acceptToken(TokenType.Operation);
                        return new UnaryOperationNode(location, parseUnary(), UnaryOperation.Negate);
                    case "--":
                        acceptToken(TokenType.Operation);
                        return new UnaryOperationNode(location, parseUnary(), UnaryOperation.PreDecrement);
                    case "++":
                        acceptToken(TokenType.Operation);
                        return new UnaryOperationNode(location, parseUnary(), UnaryOperation.PreIncrement);
                }
            }
            return parseAccess();
        }

        private AstNode parseAccess()
        {
            return parseAccess(parseTerm());
        }
        private AstNode parseAccess(AstNode left)
        {
            var location = this.location;
            if (matchToken(TokenType.OpenParentheses))
                return parseAccess(new FunctionCallNode(location, left, parseArgumentList(), parseInitialAttributes()));
            else if (acceptToken(TokenType.OpenSquareBrace))
            {
                var index = parseExpression();
                expectToken(TokenType.CloseSquareBrace);
                return parseAccess(new IterableAccessNode(location, left, index));
            }
            else if (acceptToken(TokenType.Operation, "--"))
                return new UnaryOperationNode(location, left, UnaryOperation.PostIncrement);
            else if (acceptToken(TokenType.Operation, "++"))
                return new UnaryOperationNode(location, left, UnaryOperation.PostIncrement);
            else if (acceptToken(TokenType.Dot))
                return parseAccess(new AttributeAccessNode(location, left, expectToken(TokenType.Identifier).Value));
            return left;
        }

        private AstNode parseTerm()
        {
            var location = this.location;

            if (matchToken(TokenType.Char))
                return new CharNode(location, expectToken(TokenType.Char).Value);
            else if (matchToken(TokenType.Float))
                return new FloatNode(location, expectToken(TokenType.Float).Value);
            else if (matchToken(TokenType.Integer))
                return new IntegerNode(location, expectToken(TokenType.Integer).Value);
            else if (matchToken(TokenType.String))
                return new StringNode(location, expectToken(TokenType.String).Value);
            else if (matchToken(TokenType.OpenCurlyBrace))
                return parseDictionaryDeclaration();
            else if (acceptToken(TokenType.OpenParentheses))
            {
                var expr = parseExpression();
                if (matchToken(TokenType.Comma))
                    return parseTuple(expr);
                expectToken(TokenType.CloseParentheses);
                return expr;
            }
            else if (matchToken(TokenType.OpenSquareBrace))
                return parseListDeclaration();
            else if (acceptToken(TokenType.Semicolon))
                return new CodeBlockNode(location);
            else if (matchToken(TokenType.Identifier, "lambda"))
                return parseLambda();
            else if (acceptToken(TokenType.Identifier, "new"))
                return parseExpression();
            else if (matchToken(TokenType.Identifier, "thread"))
                return parseThread();
            else if (position + 1 < tokens.Count)
            {
                if (matchToken(TokenType.Identifier) && tokens[position + 1].TokenType == TokenType.Identifier)
                    return parseEnforcedAssignment();
                else if (matchToken(TokenType.Identifier))
                    return new IdentifierNode(location, expectToken(TokenType.Identifier).Value);
            }
            else if (matchToken(TokenType.Identifier))
                return new IdentifierNode(location, expectToken(TokenType.Identifier).Value);
            throw new ParserException(location, "Unexpected token of type '{0}' with value '{1}'", tokens[position].TokenType, tokens[position].Value);
        }

        private Dictionary<string, AstNode> parseInitialAttributes()
        {
            Dictionary<string, AstNode> attribs = new Dictionary<string, AstNode>();

            if (acceptToken(TokenType.OpenCurlyBrace))
            {
                do
                {
                    string attrib = expectToken(TokenType.Identifier).Value;
                    expectToken(TokenType.Assignment);
                    AstNode value = parseExpression();
                    acceptToken(TokenType.Comma);
                    attribs.Add(attrib, value);
                }
                while (!acceptToken(TokenType.CloseCurlyBrace));
            }

            return attribs;
        }

        private EnforcedAssignmentNode parseEnforcedAssignment()
        {
            var location = this.location;
            AstNode type;
            if (tokens[position + 1].TokenType == TokenType.Dot)
                type = parseExpression();
            else
                type = new IdentifierNode(location, expectToken(TokenType.Identifier).Value);
            string variable = expectToken(TokenType.Identifier).Value;
            expectToken(TokenType.Assignment);
            AstNode value = parseExpression();

            return new EnforcedAssignmentNode(location, type, variable, value);
        }

        private ThreadNode parseThread()
        {
            var location = this.location;
            bool doImmediately = false;
            expectToken(TokenType.Identifier, "thread");
            if (acceptToken(TokenType.Identifier, "do"))
                doImmediately = true;
            AstNode body = parseStatement();

            return new ThreadNode(location, body, doImmediately);
        }

        private bool matchToken(TokenType tokenType)
        {
            return !endOfStream && tokens[position].TokenType == tokenType;
        }
        private bool matchToken(TokenType tokenType, string value)
        {
            return !endOfStream && tokens[position].TokenType == tokenType && tokens[position].Value == value;
        }

        private bool acceptToken(TokenType tokenType)
        {
            bool ret = matchToken(tokenType);
            if (ret)
                position++;
            return ret;
        }
        private bool acceptToken(TokenType tokenType, string value)
        {
            bool ret = matchToken(tokenType, value);
            if (ret)
                position++;
            return ret;
        }

        private Token expectToken(TokenType tokenType)
        {
            if (matchToken(tokenType))
                return tokens[position++];
            throw new ParserException(location, "Expected token of type '{0}', got token of type '{1}' with value '{2}'", tokenType, tokens[position].TokenType, tokens[position].Value);
        }
        private Token expectToken(TokenType tokenType, string value)
        {
            if (matchToken(tokenType, value))
                return tokens[position++];
            throw new ParserException(location, "Expected token of type '{0}' with value '{1}', got token of type '{2}' with value '{3}'", tokenType, value, tokens[position].TokenType, tokens[position].Value);
        }

        private BinaryOperation stringToBinaryOperation(string operation)
        {
            switch (operation)
            {
                case ">":
                    return BinaryOperation.GreaterThan;
                case ">=":
                    return BinaryOperation.GreaterThanOrEqual;
                case "<":
                    return BinaryOperation.LesserThan;
                case "<=":
                    return BinaryOperation.LesserThanOrEqual;
                case "!=":
                    return BinaryOperation.NotEqualTo;
                default:
                    return BinaryOperation.EqualTo;
            }
        }
    }
}
