using System;
using System.Collections.Generic;

using Hassium.Compiler;
using Hassium.Compiler.Parser.Ast;
using Hassium.Compiler.Scanner;

namespace Hassium.Compiler.Parser
{
    public class Parser
    {
        public List<Token> Tokens { get; private set; }
        public int Position { get; set; }
        public bool EndOfStream { get { return Position >= Tokens.Count; } }
        public SourceLocation Location
        {
            get
            {
                try
                {
                    return Tokens[Position].SourceLocation;
                }
                catch
                {
                    return Tokens[Position - 1].SourceLocation;
                }
            }
        }

        public AstNode Parse(List<Token> tokens)
        {
            Tokens = tokens;
            Position = 0;
            CodeBlockNode code = new CodeBlockNode();
            while (!EndOfStream)
                code.Children.Add(parseStatement());
            return code;
        }

        private AstNode parseStatement()
        {
            if (AcceptToken(TokenType.Identifier, "break"))
                return new BreakNode(Location);
            else if (MatchToken(TokenType.Identifier, "class"))
                return parseClass();
            else if (AcceptToken(TokenType.Identifier, "continue"))
                return new ContinueNode(Location);
            else if (MatchToken(TokenType.Identifier, "enum"))
                return parseEnum();
            else if (MatchToken(TokenType.Identifier, "extend"))
                return parseExtend();
            else if (MatchToken(TokenType.Identifier, "for"))
                return parseFor();
            else if (MatchToken(TokenType.Identifier, "foreach"))
                return parseForeach();
            else if (MatchToken(TokenType.Identifier, "func"))
                return parseFunc();
            else if (MatchToken(TokenType.Identifier, "global"))
                return parseGlobal();
            else if (MatchToken(TokenType.Identifier, "if"))
                return parseIf();
            else if (AcceptToken(TokenType.Identifier, "raise"))
                return new RaiseNode(Location, parseExpression());
            else if (AcceptToken(TokenType.Identifier, "return"))
                return new ReturnNode(Location, parseExpression());
            else if (MatchToken(TokenType.Identifier, "switch"))
                return parseSwitch();
            else if (MatchToken(TokenType.Identifier, "trait"))
                return parseTrait();
            else if (MatchToken(TokenType.Identifier, "try"))
                return parseTryCatch();
            else if (MatchToken(TokenType.Identifier, "until"))
                return parseWhile(true);
            else if (MatchToken(TokenType.Identifier, "use"))
                return parseUse();
            else if (MatchToken(TokenType.Identifier, "while"))
                return parseWhile();
            else if (AcceptToken(TokenType.OpenBracket))
            {
                var block = new CodeBlockNode();
                while (!AcceptToken(TokenType.CloseBracket))
                    block.Children.Add(parseStatement());
                return block;
            }
            else if (MatchToken(TokenType.Identifier) && !MatchToken(TokenType.Identifier, "thread") && Tokens[Position + 1].TokenType == TokenType.OpenBracket)
                return parseProperty();
            else if (MatchToken(TokenType.Identifier) && Tokens[Position + 1].TokenType == TokenType.Identifier)
                return parseEnforcedAssignment();
            else
                return parseExpressionStatement();
        }
        private ArgumentListNode parseArgList()
        {
            ExpectToken(TokenType.OpenParentheses);
            List<AstNode> elements = new List<AstNode>();
            while (!AcceptToken(TokenType.CloseParentheses))
            {
                elements.Add(parseExpression());
                AcceptToken(TokenType.Comma);
            }
            return new ArgumentListNode(Location, elements);
        }
        private ClassNode parseClass()
        {
            ExpectToken(TokenType.Identifier, "class");
            string name = ExpectToken(TokenType.Identifier).Value;
            List<string> inherits = new List<string>();
            if (AcceptToken(TokenType.Colon))
            {
                inherits.Add(ExpectToken(TokenType.Identifier).Value);
                while (AcceptToken(TokenType.Comma))
                    inherits.Add(ExpectToken(TokenType.Identifier).Value);
            }
            AstNode body = parseStatement();

            return new ClassNode(Location, name, inherits, body);
        }
        private EnforcedAssignmentNode parseEnforcedAssignment()
        {
            string type = ExpectToken(TokenType.Identifier).Value;
            string variable = ExpectToken(TokenType.Identifier).Value;
            ExpectToken(TokenType.Assignment);
            AstNode value = parseExpression();
            return new EnforcedAssignmentNode(Location, type, variable, value);
        }
        private EnumNode parseEnum()
        {
            ExpectToken(TokenType.Identifier, "enum");
            string enumName = ExpectToken(TokenType.Identifier).Value;
            EnumNode node = new EnumNode(Location, enumName);
            ExpectToken(TokenType.OpenBracket);
            int nextIndex = 0;
            while (!AcceptToken(TokenType.CloseBracket))
            {
                string name = ExpectToken(TokenType.Identifier).Value;
                AstNode value = new IntegerNode(Location, AcceptToken(TokenType.Assignment) ? Convert.ToInt64(ExpectToken(TokenType.Integer).Value) : nextIndex++);
                node.Children.Add(new BinaryOperationNode(Location, BinaryOperation.Assignment, new StringNode(Location, name), value));
                AcceptToken(TokenType.Comma);
            }
            return node;
        }
        private ExtendNode parseExtend()
        {
            ExpectToken(TokenType.Identifier, "extend");
            string name = ExpectToken(TokenType.Identifier).Value;
            ExpectToken(TokenType.OpenBracket);
            ExtendNode node = new ExtendNode(Location, name);
            while (!AcceptToken(TokenType.CloseBracket))
                node.Children.Add(parseStatement());
            return node;
        }
        private ForNode parseFor()
        {
            ExpectToken(TokenType.Identifier, "for");
            ExpectToken(TokenType.OpenParentheses);
            AstNode startStatement = parseStatement();
            AcceptToken(TokenType.Semicolon);
            AstNode predicate = parseExpression();
            AcceptToken(TokenType.Semicolon);
            AstNode repeatStatement = parseStatement();
            ExpectToken(TokenType.CloseParentheses);
            AstNode body = parseStatement();

            return new ForNode(Location, startStatement, predicate, repeatStatement, body);
        }
        private ForeachNode parseForeach()
        {
            ExpectToken(TokenType.Identifier, "foreach");
            ExpectToken(TokenType.OpenParentheses);
            string variable = ExpectToken(TokenType.Identifier).Value;
            ExpectToken(TokenType.Identifier, "in");
            AstNode target = parseExpression();
            ExpectToken(TokenType.CloseParentheses);
            AstNode body = parseStatement();

            return new ForeachNode(Location, variable, target, body);
        }
        private FuncNode parseFunc()
        {
            ExpectToken(TokenType.Identifier, "func");
            string name = ExpectToken(TokenType.Identifier).Value;
            List<FuncParameter> parameters = new List<FuncParameter>();
            ExpectToken(TokenType.OpenParentheses);
            while (!AcceptToken(TokenType.CloseParentheses))
            {
                parameters.Add(parseParameter());
                AcceptToken(TokenType.Comma);
            }
            if (AcceptToken(TokenType.Colon))
            {
                string returnType = ExpectToken(TokenType.Identifier).Value;
                return new FuncNode(Location, name, parameters, parseStatement(), returnType);
            }
            return new FuncNode(Location, name, parameters, parseStatement());
        }
        private GlobalNode parseGlobal()
        {
            ExpectToken(TokenType.Identifier, "global");
            string variable = ExpectToken(TokenType.Identifier).Value;
            ExpectToken(TokenType.Assignment, "=");
            AstNode value = parseExpression();
            return new GlobalNode(Location, variable, value);
        }
        private IfNode parseIf()
        {
            ExpectToken(TokenType.Identifier, "if");
            ExpectToken(TokenType.OpenParentheses);
            AstNode predicate = parseExpression();
            ExpectToken(TokenType.CloseParentheses);
            AstNode body = parseStatement();
            if (AcceptToken(TokenType.Identifier, "else"))
                return new IfNode(Location, predicate, body, parseStatement());
            return new IfNode(Location, predicate, body);
        }
        private KeyValuePairNode parseKeyValuePair()
        {
            AstNode key = parseExpression();
            ExpectToken(TokenType.Colon);
            AstNode value = parseExpression();
            AcceptToken(TokenType.Comma);
            return new KeyValuePairNode(Location, key, value);
        }
        private LambdaNode parseLambda()
        {
            ExpectToken(TokenType.Identifier, "lambda");
            ArgumentListNode parameters = parseArgList();
            AstNode body = parseStatement();
            return new LambdaNode(Location, parameters, body);
        }
        private ListDeclarationNode parseListDeclaration()
        {
            ExpectToken(TokenType.OpenSquare);
            List<AstNode> initial = new List<AstNode>();
            while (!AcceptToken(TokenType.CloseSquare))
            {
                initial.Add(parseExpression());
                AcceptToken(TokenType.Comma);
            }
            return new ListDeclarationNode(Location, initial);
        }
        private FuncParameter parseParameter()
        {
            string name = ExpectToken(TokenType.Identifier).Value;
            if (AcceptToken(TokenType.Colon))
                return new FuncParameter(name, ExpectToken(TokenType.Identifier).Value);
            return new FuncParameter(name);
        }
        private PropertyNode parseProperty()
        {
            string variable = ExpectToken(TokenType.Identifier).Value;
            ExpectToken(TokenType.OpenBracket);
            ExpectToken(TokenType.Identifier, "get");
            AstNode getBody = parseStatement();
            ExpectToken(TokenType.Identifier, "set");
            AstNode setBody = parseStatement();
            ExpectToken(TokenType.CloseBracket);
            return new PropertyNode(Location, variable, getBody, setBody);
        }
        private UseNode parseUse()
        {
            ExpectToken(TokenType.Identifier, "use");
            if (MatchToken(TokenType.String))
                return new UseNode(Location, new List<string> () { ExpectToken(TokenType.String).Value });
            var parts = new List<string>();
            parts.Add(ExpectToken(TokenType.Identifier).Value);
            while (AcceptToken(TokenType.Operation, "/") || AcceptToken(TokenType.Dot))
                parts.Add(ExpectToken(TokenType.Identifier).Value);
            return new UseNode(Location, parts);
        }
        private SwitchNode parseSwitch()
        {
            ExpectToken(TokenType.Identifier, "switch");
            AstNode expression = parseExpression();
            ExpectToken(TokenType.OpenBracket);
            var cases = new List<Case>();
            while (!AcceptToken(TokenType.CloseBracket))
            {
                if (AcceptToken(TokenType.Identifier, "default"))
                {
                    var ret = new SwitchNode(Location, expression, cases, parseStatement());
                    ExpectToken(TokenType.CloseBracket);
                    return ret;
                }
                ExpectToken(TokenType.Identifier, "case");
                var expressions = new List<AstNode>();
                expressions.Add(parseExpression());
                while (AcceptToken(TokenType.Comma))
                    expressions.Add(parseExpression());
                AstNode caseBody = parseStatement();
                cases.Add(new Case(expressions, caseBody));
            }
            return new SwitchNode(Location, expression, cases, new StatementNode(Location));
        }
        private TraitNode parseTrait()
        {
            ExpectToken(TokenType.Identifier, "trait");
            string name = ExpectToken(TokenType.Identifier).Value;
            ExpectToken(TokenType.OpenBracket);
            List<Trait> traits = new List<Trait>();
            while (!AcceptToken(TokenType.CloseBracket))
            {
                string attribute = ExpectToken(TokenType.Identifier).Value;
                ExpectToken(TokenType.Colon);
                string type = ExpectToken(TokenType.Identifier).Value;
                AcceptToken(TokenType.Comma);
                traits.Add(new Trait(attribute, type));
            }

            return new TraitNode(Location, name, traits);
        }
        private TryCatchNode parseTryCatch()
        {
            ExpectToken(TokenType.Identifier, "try");
            AstNode tryBody = parseStatement();
            ExpectToken(TokenType.Identifier, "catch");
            AstNode catchBody = parseStatement();

            return new TryCatchNode(Location, tryBody, catchBody);
        }
        private WhileNode parseWhile(bool until = false)
        {
            ExpectToken(TokenType.Identifier);
            ExpectToken(TokenType.OpenParentheses);
            AstNode predicate = parseExpression();
            if (until)
                predicate = new UnaryOperationNode(Location, predicate, UnaryOperation.LogicalNot);
            ExpectToken(TokenType.CloseParentheses);
            AstNode body = parseStatement();
            if (AcceptToken(TokenType.Identifier, "else"))
                return new WhileNode(Location, predicate, body, parseStatement());
            return new WhileNode(Location, predicate, body);
        }

        private AstNode parseExpressionStatement()
        {
            AstNode expression = parseExpression();
            AcceptToken(TokenType.Semicolon);
            if (expression is FunctionCallNode || expression is BinaryOperationNode)
                return new ExpressionStatementNode(Location, expression);
            if (expression is UnaryOperationNode)
            if (((UnaryOperationNode)expression).UnaryOperation != UnaryOperation.Reference)
                return new ExpressionStatementNode(Location, expression);
            return expression;
        }
        private AstNode parseExpression()
        {
            return parseAssignment();
        }
        private AstNode parseAssignment()
        {
            AstNode left = parseTernary();
            if (MatchToken(TokenType.Assignment))
            {
                switch (Tokens[Position].Value)
                {
                    case "=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, parseAssignment());
                    case "+=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, new BinaryOperationNode(Location, BinaryOperation.Addition, left, parseAssignment()));
                    case "-=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, new BinaryOperationNode(Location, BinaryOperation.Subraction, left, parseAssignment()));
                    case "*=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, new BinaryOperationNode(Location, BinaryOperation.Multiplication, left, parseAssignment()));
                    case "/=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, new BinaryOperationNode(Location, BinaryOperation.Division, left, parseAssignment()));
                    case "%=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, new BinaryOperationNode(Location, BinaryOperation.Modulus, left, parseAssignment()));
                    case "<<=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, new BinaryOperationNode(Location, BinaryOperation.BitshiftLeft, left, parseAssignment()));
                    case ">>=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, new BinaryOperationNode(Location, BinaryOperation.BitshiftRight, left, parseAssignment()));
                    case "&=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, new BinaryOperationNode(Location, BinaryOperation.LogicalAnd, left, parseAssignment()));
                    case "|=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, new BinaryOperationNode(Location, BinaryOperation.LogicalOr, left, parseAssignment()));
                    case "^=":
                        AcceptToken(TokenType.Assignment);
                        return new BinaryOperationNode(Location, BinaryOperation.Assignment, left, new BinaryOperationNode(Location, BinaryOperation.BitwiseXor, left, parseAssignment()));
                    default:
                        break;
                }
            }
            return left;
        }
        private AstNode parseTernary()
        {
            AstNode left = parseLogicalOr();

            while (AcceptToken(TokenType.Question))
            {
                AstNode trueStatement = parseExpression();
                ExpectToken(TokenType.Colon);
                AstNode falseStatement = parseExpression();
                left = new TernaryOperationNode(Location, left, trueStatement, falseStatement);
            }
            return left;
        }
        private AstNode parseLogicalOr()
        {
            AstNode left = parseEquality();
            while (AcceptToken(TokenType.Operation, "||"))
                left = new BinaryOperationNode(Location, BinaryOperation.LogicalOr, left, parseLogicalOr());
            return left;
        }
        private AstNode parseLogicalAnd()
        {
            AstNode left = parseEquality();
            while (AcceptToken(TokenType.Operation, "&&"))
                left = new BinaryOperationNode(Location, BinaryOperation.LogicalAnd, left, parseLogicalAnd());
            return left;
        }
        private AstNode parseEquality()
        {
            AstNode left = parseComparison();
            while (MatchToken(TokenType.Comparison))
            {
                switch (Tokens[Position].Value)
                {
                    case "==":
                        AcceptToken(TokenType.Comparison);
                        return new BinaryOperationNode(Location, BinaryOperation.EqualTo, left, parseEquality());
                    case "!=":
                        AcceptToken(TokenType.Comparison);
                        return new BinaryOperationNode(Location, BinaryOperation.NotEqualTo, left, parseEquality());
                    default:
                        break;
                }
                break;
            }
            return left;
        }
        private AstNode parseComparison()
        {
            AstNode left = parseOr();
            while (MatchToken(TokenType.Comparison))
            {
                switch (Tokens[Position].Value)
                {
                    case ">":
                        AcceptToken(TokenType.Comparison);
                        return new BinaryOperationNode(Location, BinaryOperation.GreaterThan, left, parseComparison());
                    case ">=":
                        AcceptToken(TokenType.Comparison);
                        return new BinaryOperationNode(Location, BinaryOperation.GreaterThanOrEqual, left, parseComparison());
                    case "<":
                        AcceptToken(TokenType.Comparison);
                        return new BinaryOperationNode(Location, BinaryOperation.LesserThan, left, parseComparison());
                    case "<=":
                        AcceptToken(TokenType.Comparison);
                        return new BinaryOperationNode(Location, BinaryOperation.LesserThanOrEqual, left, parseComparison());
                    default:
                        break;
                }
                break;
            }
            return left;
        }
        private AstNode parseOr()
        {
            AstNode left = parseXor();
            while (AcceptToken(TokenType.Operation, "|"))
                left = new BinaryOperationNode(Location, BinaryOperation.BitwiseOr, left, parseOr());
            return left;
        }
        private AstNode parseXor()
        {
            AstNode left = parseAnd();
            while (AcceptToken(TokenType.Operation, "^"))
                left = new BinaryOperationNode(Location, BinaryOperation.BitwiseXor, left, parseXor());
            return left;
        }
        private AstNode parseAnd()
        {
            AstNode left = parseBitshift();
            while (AcceptToken(TokenType.Operation, "&"))
                left = new BinaryOperationNode(Location, BinaryOperation.BitwiseAnd, left, parseAnd());
            return left;
        }
        private AstNode parseBitshift()
        {
            AstNode left = parseAdditive();
            while (MatchToken(TokenType.Operation))
            {
                switch (Tokens[Position].Value)
                {
                    case "<<":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.BitshiftLeft, left, parseBitshift());
                    case ">>":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.BitshiftRight, left, parseBitshift());
                    default:
                        break;
                }
                break;
            }
            return left;
        }
        private AstNode parseAdditive()
        {
            AstNode left = parseMultiplicative();
            while (MatchToken(TokenType.Operation))
            {
                switch (Tokens[Position].Value)
                {
                    case "+":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.Addition, left, parseAdditive());
                    case "-":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.Subraction, left, parseAdditive());
                    default:
                        break;
                }
                break;
            }
            return left;
        }
        private AstNode parseMultiplicative()
        {
            AstNode left = parseUnary();
            while (MatchToken(TokenType.Operation))
            {
                switch (Tokens[Position].Value)
                {
                    case "*":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.Multiplication, left, parseMultiplicative());
                    case "/":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.Division, left, parseMultiplicative());
                    case "%":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.Modulus, left, parseMultiplicative());
                    case "**":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.Power, left, parseMultiplicative());
                    case "//":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.IntegerDivision, left, parseMultiplicative());
                    case "??":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.NullCoalescing, left, parseMultiplicative());
                    case "is":
                        AcceptToken(TokenType.Operation);
                        return new BinaryOperationNode(Location, BinaryOperation.Is, left, parseMultiplicative());
                    default:
                        break;
                }
                break;
            }
            return left;
        }
        private AstNode parseUnary()
        {
            if (MatchToken(TokenType.Operation))
            {
                switch (Tokens[Position].Value)
                {
                    case "~":
                        AcceptToken(TokenType.Operation);
                        return new UnaryOperationNode(Location, parseUnary(), UnaryOperation.BitwiseNot);
                    case "*":
                        AcceptToken(TokenType.Operation);
                        return new UnaryOperationNode(Location, parseUnary(), UnaryOperation.Dereference);
                    case "!":
                        AcceptToken(TokenType.Operation);
                        return new UnaryOperationNode(Location, parseUnary(), UnaryOperation.LogicalNot);
                    case "-":
                        AcceptToken(TokenType.Operation);
                        return new UnaryOperationNode(Location, parseUnary(), UnaryOperation.Negate);
                    case "--":
                        AcceptToken(TokenType.Operation);
                        return new UnaryOperationNode(Location, parseUnary(), UnaryOperation.PreDecrement);
                    case "++":
                        AcceptToken(TokenType.Operation);
                        return new UnaryOperationNode(Location, parseUnary(), UnaryOperation.PreIncrement);
                    case "&":
                        AcceptToken(TokenType.Operation);
                        return new UnaryOperationNode(Location, parseUnary(), UnaryOperation.Reference);
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
            if (MatchToken(TokenType.OpenParentheses))
                return parseAccess(new FunctionCallNode(Location, left, parseArgList(), parseFuncInitialList()));
            else if (AcceptToken(TokenType.OpenSquare))
            {
                AstNode expression = parseExpression();
                ExpectToken(TokenType.CloseSquare);
                return parseAccess(new ListAccessNode(Location, left, expression));
            }
            else if (AcceptToken(TokenType.Operation, "--"))
                return new UnaryOperationNode(Location, left, UnaryOperation.PostDecrement);
            else if (AcceptToken(TokenType.Operation, "++"))
                return new UnaryOperationNode(Location, left, UnaryOperation.PostIncrement);
            else if (AcceptToken(TokenType.Dot))
                return parseAccess(new AttributeAccessNode(Location, left, ExpectToken(TokenType.Identifier).Value));
            else
                return left;
        }
        private AstNode parseTerm()
        {
            if (AcceptToken(TokenType.Identifier, "new"))
                return parseExpression();
            else if (MatchToken(TokenType.Identifier, "lambda"))
                return parseLambda();
            else if (AcceptToken(TokenType.Identifier, "thread"))
                return new ThreadNode(Location, parseStatement());
            else if (MatchToken(TokenType.OpenSquare))
                return parseListDeclaration();
            else if (AcceptToken(TokenType.OpenBracket))
            {
                var dict = new DictionaryDeclarationNode(Location);
                while (!AcceptToken(TokenType.CloseBracket))
                    dict.Children.Add(parseKeyValuePair());
                return dict;
            }
            else if (AcceptToken(TokenType.OpenParentheses))
            {
                var expr = parseExpression();
                if (AcceptToken(TokenType.Comma))
                {
                    TupleNode tuple = new TupleNode(Location);
                    tuple.Children.Add(expr);
                    while (!MatchToken(TokenType.CloseParentheses))
                    {
                        tuple.Children.Add(parseExpression());
                        if (!AcceptToken(TokenType.Comma))
                            break;
                    }
                    ExpectToken(TokenType.CloseParentheses);
                    return tuple;
                }
                ExpectToken(TokenType.CloseParentheses);
                return expr;
            }
            else if (MatchToken(TokenType.Identifier))
                return new IdentifierNode(Location, ExpectToken(TokenType.Identifier).Value);
            else if (MatchToken(TokenType.String))
                return new StringNode(Location, ExpectToken(TokenType.String).Value);
            else if (MatchToken(TokenType.Integer))
                return new IntegerNode(Location, Convert.ToInt64(ExpectToken(TokenType.Integer).Value));
            else if (MatchToken(TokenType.Float))
                return new FloatNode(Location, Convert.ToDouble(ExpectToken(TokenType.Float).Value));
            else if (MatchToken(TokenType.Char))
                return new CharNode(Location, Convert.ToChar(ExpectToken(TokenType.Char).Value));
            else if (AcceptToken(TokenType.Semicolon))
                return new StatementNode(Location);
            else
                throw new CompileException(Location, "Unexpected token type {0} with value {1}!", Tokens[Position].TokenType, Tokens[Position].Value);
        }

        private List<BinaryOperationNode> parseFuncInitialList()
        {
            var result = new List<BinaryOperationNode>();
            if (AcceptToken(TokenType.OpenBracket))
            {
                while (!AcceptToken(TokenType.CloseBracket))
                {
                    string identifier = ExpectToken(TokenType.Identifier).Value;
                    ExpectToken(TokenType.Assignment);
                    AstNode value = parseExpression();
                    result.Add(new BinaryOperationNode(Location, BinaryOperation.Assignment, new IdentifierNode(Location, identifier), value));
                    AcceptToken(TokenType.Comma);
                }
            }
            return result;
        }

        public bool MatchToken(TokenType tokenType)
        {
            return !EndOfStream && Tokens[Position].TokenType == tokenType;
        }
        public bool MatchToken(TokenType tokenType, string value)
        {
            return !EndOfStream && Tokens[Position].TokenType == tokenType && Tokens[Position].Value == value;
        }

        public bool AcceptToken(TokenType tokenType)
        {
            bool ret = MatchToken(tokenType);
            if (ret)
                Position++;
            return ret;
        }
        public bool AcceptToken(TokenType tokenType, string value)
        {
            bool ret = MatchToken(tokenType, value);
            if (ret)
                Position++;
            return ret;
        }

        public Token ExpectToken(TokenType tokenType)
        {
            if (MatchToken(tokenType))
                return Tokens[Position++];
            throw new CompileException(Location, "Expected token type {0}!", tokenType);
        }
        public Token ExpectToken(TokenType tokenType, string value)
        {
            if (MatchToken(tokenType, value))
                return Tokens[Position++];
            throw new CompileException(Location, "Expected token type {0} with value {1}!", tokenType, value);
        }
    }
}