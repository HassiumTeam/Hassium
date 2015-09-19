using System;
using System.Collections.Generic;
using System.Linq;
using Hassium.Interpreter;
using Hassium.Lexer;
using Hassium.Parser.Ast;

namespace Hassium.Parser
{
    /// <summary>
    /// Parser.
    /// </summary>
    public class Parser
    {
        private List<Token> tokens;
        public int position;

        public int codePos
        {
            get { return CurrentToken().Position; }
        }

        public bool EndOfStream
        {
            get { return tokens.Count <= position; }
        }

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        /// <summary>
        /// ParseStatement this instance.
        /// </summary>
        public AstNode Parse()
        {
            CodeBlock block = new CodeBlock(0);
            while (!EndOfStream)
            {
                block.Children.Add(ParseStatement(this));
            }
            return block;
        }

        public Token CurrentToken()
        {
            return position >= tokens.Count ? new Token(TokenType.Identifier, "") : tokens[position];
        }

        public Token PreviousToken(int delay = 1)
        {
            return tokens[position - delay];
        }

        public bool MatchToken(TokenType clazz)
        {
            return position < tokens.Count && tokens[position].TokenClass == clazz;
        }

        public bool MatchToken(TokenType clazz, string value)
        {
            return position < tokens.Count && tokens[position].TokenClass == clazz &&
                   tokens[position].Value.ToString() == value;
        }

        public bool AcceptToken(TokenType clazz)
        {
            if (MatchToken(clazz))
            {
                position++;
                return true;
            }

            return false;
        }

        public bool AcceptToken(TokenType clazz, string value)
        {
            if (MatchToken(clazz, value))
            {
                position++;
                return true;
            }

            return false;
        }

        public Token ExpectToken(TokenType clazz)
        {
            return MatchToken(clazz) ? tokens[position++] : new Token(TokenType.Exception, "Tokens did not match");
        }

        public Token ExpectToken(TokenType clazz, string value)
        {
            return MatchToken(clazz, value)
                ? tokens[position++]
                : new Token(TokenType.Exception, "Tokens did not match");
        }

        public static AstNode ParseStatement(Parser parser)
        {
            if (parser.MatchToken(TokenType.Identifier))
            {
                switch (parser.CurrentToken().Value.ToString().ToLower())
                {
                    case "if":
                        return ParseIf(parser);
                    case "while":
                        return ParseWhile(parser);
                    case "for":
                        return ParseFor(parser);
                    case "foreach":
                        return ParseForEach(parser);
                    case "try":
                        return ParseTryCatch(parser);
                    case "class":
                        return ParseClass(parser);
                    case "func":
                        return ParseFunc(parser);
                    case "property":
                        return ParseProperty(parser);
                    case "lambda":
                        return ParseLambda(parser);
                    case "thread":
                        return ParseThread(parser);
                    case "unchecked":
                        return ParseUnchecked(parser);
                    case "switch":
                        return ParseSwitch(parser);
                    case "return":
                        return ParseReturn(parser);
                    case "continue":
                        return ParseContinue(parser);
                    case "break":
                        return ParseBreak(parser);
                    case "use":
                        return ParseUse(parser);
                }
            }
            else if (parser.MatchToken(TokenType.Brace, "{"))
            {
                return ParseCodeBlock(parser);
            }
            AstNode expr = ParseExpression(parser);
            parser.ExpectToken(TokenType.EndOfLine);
            return expr;
        }

        #region Blocks

        public static AstNode ParseClass(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "class");
            string name = parser.ExpectToken(TokenType.Identifier).Value.ToString();
            string extends = "";
            if (parser.AcceptToken(TokenType.Identifier, ":"))
                extends = parser.ExpectToken(TokenType.Identifier).Value.ToString();
            parser.ExpectToken(TokenType.Brace);
            AstNode body = ParseCodeBlock(parser);
            parser.ExpectToken(TokenType.Brace);

            return new ClassNode(pos, name, body, extends);
        }

        public static AstNode ParseFunc(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "func");
            string name = parser.ExpectToken(TokenType.Identifier).Value.ToString();
            parser.ExpectToken(TokenType.Parentheses, "(");

            List<string> result = new List<string>();
            while (parser.MatchToken(TokenType.Identifier))
            {
                result.Add(parser.ExpectToken(TokenType.Identifier).Value.ToString());
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode body = ParseStatement(parser);

            return new FuncNode(pos, name, result, body);
        }

        public static AstNode ParseProperty(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "property");
            string name = parser.ExpectToken(TokenType.Identifier).Value.ToString();

            AstNode getBody = null;
            AstNode setBody = null;
            FuncNode getnode = null;
            FuncNode setnode = null;
            bool autoProp = false;

            if (parser.AcceptToken(TokenType.EndOfLine))
            {
                getBody = new CodeBlock(parser.codePos);
                getBody.Children.Add(new ReturnNode(parser.codePos,
                    new MemberAccessNode(parser.codePos, new IdentifierNode(parser.codePos, "this"), "__prop__" + name)));
                setBody = new CodeBlock(parser.codePos);
                setBody.Children.Add(new BinOpNode(parser.codePos, BinaryOperation.Assignment,
                    new MemberAccessNode(parser.codePos, new IdentifierNode(parser.codePos, "this"), "__prop__" + name),
                    new IdentifierNode(parser.codePos, "value")));
            }
            else if (parser.AcceptToken(TokenType.Lambda, "=>"))
            {
                getBody = new CodeBlock(parser.codePos);
                getBody.Children.Add(new ReturnNode(parser.codePos, ParseExpression(parser)));

                setBody = new CodeBlock(parser.codePos);
                setBody.Children.Add(new BinOpNode(parser.codePos, BinaryOperation.Assignment, ParseExpression(parser),
                    new IdentifierNode(parser.codePos, "value")));
                parser.ExpectToken(TokenType.EndOfLine);
            }
            else
            {
                parser.ExpectToken(TokenType.Brace, "{");

                parser.ExpectToken(TokenType.Identifier, "get");

                if (parser.AcceptToken(TokenType.EndOfLine, ";"))
                {
                    autoProp = true;
                    getBody = new CodeBlock(parser.codePos);
                    getBody.Children.Add(new ReturnNode(parser.codePos,
                        new MemberAccessNode(parser.codePos, new IdentifierNode(parser.codePos, "this"),
                            "__prop__" + name)));
                }
                else
                    getBody = ParseCodeBlock(parser);


                if (parser.AcceptToken(TokenType.Identifier, "set"))
                {
                    if (parser.AcceptToken(TokenType.EndOfLine, ";"))
                    {
                        setBody = new CodeBlock(parser.codePos);
                        setBody.Children.Add(new BinOpNode(parser.codePos, BinaryOperation.Assignment,
                            new MemberAccessNode(parser.codePos, new IdentifierNode(parser.codePos, "this"),
                                "__prop__" + name),
                            new IdentifierNode(parser.codePos, "value")));
                    }
                    else
                    {
                        if (autoProp)
                            throw new ParseException("An auto-property cannot declare a set body", pos);
                        setBody = ParseCodeBlock(parser);
                    }
                }
                else
                {
                    if (autoProp)
                        throw new ParseException("An auto-property must contain a set statement", pos);
                }

                parser.ExpectToken(TokenType.Brace, "}");
            }

            getnode = new FuncNode(parser.codePos, "__getprop__" + name, new List<string> {"this"}, getBody);
            setnode = setBody == null
                ? null
                : new FuncNode(parser.codePos, "__setprop__" + name, new List<string> {"this", "value"}, setBody);

            return new PropertyNode(pos, name, getnode, setnode);
        }

        public static AstNode ParseCodeBlock(Parser parser)
        {
            CodeBlock block = new CodeBlock(parser.codePos);
            parser.ExpectToken(TokenType.Brace, "{");

            while (!parser.EndOfStream && !parser.MatchToken(TokenType.Brace, "}"))
            {
                block.Children.Add(ParseStatement(parser));
            }

            parser.ExpectToken(TokenType.Brace, "}");
            return block;
        }

        #endregion

        public static ArgListNode ParseArgList(Parser parser)
        {
            ArgListNode ret = new ArgListNode(parser.codePos);
            //parser.ExpectToken(TokenType.Parentheses, "("); causes problems when a function is called like that myFunc((1+1)*2) (with two parentheses at the beginning)

            while (!parser.MatchToken(TokenType.Parentheses, ")"))
            {
                ret.Children.Add(ParseExpression(parser));
                if (!parser.AcceptToken(TokenType.Comma))
                {
                    break;
                }
            }
            parser.ExpectToken(TokenType.Parentheses, ")");

            return ret;
        }

        public static AstNode ParseUse(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "use");
            string path = "";
            UseNode ret = null;
            if (parser.MatchToken(TokenType.Identifier))
            {
                path = parser.ExpectToken(TokenType.Identifier).Value.ToString();
                ret = new UseNode(pos, path, "", true, true, false);
            }
            else
            {
                path = parser.ExpectToken(TokenType.String).Value.ToString();
                bool global = true;
                string name = "";
                if (parser.AcceptToken(TokenType.Identifier, "as"))
                {
                    global = false;
                    name = parser.ExpectToken(TokenType.Identifier).Value.ToString();
                }

                if (path.EndsWith(".dll"))
                {
                    ret = new UseNode(pos, path, name, global, false, true);
                }
                else
                {
                    ret = new UseNode(pos, path, name, global, false, false);
                }
            }
            parser.ExpectToken(TokenType.EndOfLine);
            return ret;
        }


        public static AstNode ParseTryCatch(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "try");
            AstNode tryBody = ParseStatement(parser);
            parser.ExpectToken(TokenType.Identifier, "catch");
            AstNode catchBody = ParseStatement(parser);

            if (parser.AcceptToken(TokenType.Identifier, "finally"))
            {
                AstNode finallyBody = ParseStatement(parser);
                return new TryNode(pos, tryBody, catchBody, finallyBody);
            }

            return new TryNode(pos, tryBody, catchBody);
        }

        #region Conditions

        public static AstNode ParseIf(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "if");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode predicate = ParseExpression(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode ifBody = ParseStatement(parser);
            if (parser.AcceptToken(TokenType.Identifier, "else"))
            {
                int epos = parser.codePos;
                AstNode elseBody = ParseStatement(parser);
                return new IfNode(epos, predicate, ifBody, elseBody);
            }

            return new IfNode(pos, predicate, ifBody);
        }

        public static AstNode ParseSwitch(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "switch");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode predicate = ParseExpression(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            parser.ExpectToken(TokenType.Brace, "{");
            var cases = new List<CaseNode>();
            CaseNode defn = null;
            while (parser.MatchToken(TokenType.Identifier, "case"))
            {
                int cpos = parser.codePos;
                parser.ExpectToken(TokenType.Identifier, "case");
                var pred = new List<AstNode> {ParseExpression(parser)};
                parser.ExpectToken(TokenType.Identifier, ":");
                while (parser.MatchToken(TokenType.Identifier, "case"))
                {
                    parser.ExpectToken(TokenType.Identifier, "case");
                    var pred2 = ParseExpression(parser);
                    parser.ExpectToken(TokenType.Identifier, ":");
                    pred.Add(pred2);
                }
                var cbody = ParseStatement(parser);
                cases.Add(new CaseNode(cpos, pred, cbody));
            }
            if (parser.MatchToken(TokenType.Identifier, "default"))
            {
                int dpos = parser.codePos;
                parser.ExpectToken(TokenType.Identifier, "default");
                parser.ExpectToken(TokenType.Identifier, ":");
                var dbody = ParseStatement(parser);
                defn = new CaseNode(dpos, null, dbody);
            }
            parser.ExpectToken(TokenType.Brace, "}");
            return new SwitchNode(pos, predicate, cases, defn);
        }

        #endregion

        #region Loops

        public static AstNode ParseFor(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "for");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode left = new CodeBlock(parser.codePos);
            AstNode predicate = new CodeBlock(parser.codePos);
            AstNode right = new CodeBlock(parser.codePos);
            if (!parser.AcceptToken(TokenType.EndOfLine, ";"))
                left = ParseStatement(parser);
            if (!parser.AcceptToken(TokenType.EndOfLine, ";"))
                predicate = ParseStatement(parser);
            if (!parser.AcceptToken(TokenType.EndOfLine, ";"))
                right = ParseStatement(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode forBody = ParseStatement(parser);

            return new ForNode(pos, left, predicate, right, forBody);
        }

        public static AstNode ParseForEach(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "foreach");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode needle = null;
            needle = parser.CurrentToken().Value.ToString() == "["
                ? ParseArrayInitializer(parser)
                : ParseIdentifier(parser);
            parser.ExpectToken(TokenType.Identifier, "in");
            AstNode haystack = ParseStatement(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode forBody = ParseStatement(parser);

            return new ForEachNode(pos, needle, haystack, forBody);
        }

        public static AstNode ParseWhile(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "while");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode predicate = ParseExpression(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode whileBody = ParseStatement(parser);
            if (parser.AcceptToken(TokenType.Identifier, "else"))
            {
                AstNode elseBody = ParseStatement(parser);
                return new WhileNode(pos, predicate, whileBody, elseBody);
            }

            return new WhileNode(pos, predicate, whileBody);
        }

        #endregion

        public static AstNode ParseReturn(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "return");
            if (parser.AcceptToken(TokenType.EndOfLine))
            {
                parser.ExpectToken(TokenType.EndOfLine);
                return new ReturnNode(pos, null);
            }
            else
                return new ReturnNode(pos, ParseStatement(parser));
        }

        #region Expression

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
                case "=":
                    return BinaryOperation.Assignment;
                case "==":
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
                    return BinaryOperation.LogicalAnd;
                case "||":
                    return BinaryOperation.LogicalOr;
                case "??":
                    return BinaryOperation.NullCoalescing;
                case ".":
                    return BinaryOperation.Dot;
                case "is":
                    return BinaryOperation.Is;
                default:
                    throw new ArgumentException("Invalid binary operation: " + value);
            }
        }

        public static AstNode ParseExpression(Parser parser)
        {
            return ParseAssignment(parser);
        }

        private static AstNode ParseAssignment(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseConditional(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Assignment ||
                   parser.CurrentToken().TokenClass == TokenType.OpAssign)
            {
                if (parser.AcceptToken(TokenType.Assignment))
                {
                    AstNode right = ParseConditional(parser);
                    left = new BinOpNode(pos, BinaryOperation.Assignment, left, right);
                }
                else if (parser.AcceptToken(TokenType.OpAssign))
                {
                    var assigntype = GetBinaryOp(parser.PreviousToken().Value.ToString());
                    var right = ParseConditional(parser);
                    left = new BinOpNode(pos, BinaryOperation.Assignment, assigntype, left, right);
                }
                else
                    break;
            }

            return left;
        }

        private static AstNode ParseConditional(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseLogicalOr(parser);

            while (parser.AcceptToken(TokenType.Operation, "?"))
            {
                var ifbody = ParseConditional(parser);
                parser.ExpectToken(TokenType.Identifier, ":");
                var elsebody = ParseConditional(parser);
                left = new ConditionalOpNode(pos, left, ifbody, elsebody);
            }

            return left;
        }

        private static AstNode ParseLogicalOr(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseLogicalAnd(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Comparison ||
                   parser.CurrentToken().TokenClass == TokenType.Operation)
            {
                if (parser.AcceptToken(TokenType.Comparison, "||"))
                {
                    var right = ParseLogicalAnd(parser);
                    left = new BinOpNode(pos, BinaryOperation.LogicalOr, left, right);
                }
                else if (parser.AcceptToken(TokenType.Operation, "??"))
                {
                    var right = ParseLogicalAnd(parser);
                    left = new BinOpNode(pos, BinaryOperation.NullCoalescing, left, right);
                }
                else
                    break;
            }

            return left;
        }

        private static AstNode ParseLogicalAnd(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseEquality(parser);

            while (parser.AcceptToken(TokenType.Comparison, "&&"))
            {
                var right = ParseEquality(parser);
                left = new BinOpNode(pos, BinaryOperation.LogicalAnd, left, right);
            }

            return left;
        }


        private static AstNode ParseEquality(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseComparison(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Comparison ||
                   parser.CurrentToken().TokenClass == TokenType.Operation)
            {
                if (parser.AcceptToken(TokenType.Comparison, "=="))
                {
                    var right = ParseComparison(parser);
                    left = new BinOpNode(pos, BinaryOperation.Equals, left, right);
                }
                else if (parser.AcceptToken(TokenType.Comparison, "!="))
                {
                    var right = ParseComparison(parser);
                    left = new BinOpNode(pos, BinaryOperation.NotEqualTo, left, right);
                }
                else
                    break;
            }

            return left;
        }

        private static AstNode ParseComparison(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseOr(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Comparison ||
                   parser.CurrentToken().Value.ToString() == "is")
            {
                if (parser.AcceptToken(TokenType.Comparison, "<"))
                {
                    var right = ParseOr(parser);
                    left = new BinOpNode(pos, BinaryOperation.LessThan, left, right);
                }
                else if (parser.AcceptToken(TokenType.Comparison, ">"))
                {
                    var right = ParseOr(parser);
                    left = new BinOpNode(pos, BinaryOperation.GreaterThan, left, right);
                }
                else if (parser.AcceptToken(TokenType.Comparison, "<="))
                {
                    var right = ParseOr(parser);
                    left = new BinOpNode(pos, BinaryOperation.LesserOrEqual, left, right);
                }
                else if (parser.AcceptToken(TokenType.Comparison, ">="))
                {
                    var right = ParseOr(parser);
                    left = new BinOpNode(pos, BinaryOperation.GreaterOrEqual, left, right);
                }
                else if (parser.AcceptToken(TokenType.Comparison, "<=>"))
                {
                    var right = ParseOr(parser);
                    left = new BinOpNode(pos, BinaryOperation.CombinedComparison, left, right);
                }
                else if (parser.AcceptToken(TokenType.Identifier, "is"))
                {
                    AstNode right = ParseOr(parser);
                    left = new BinOpNode(pos, BinaryOperation.Is, left, right);
                }
                else
                    break;
            }

            return left;
        }

        private static AstNode ParseOr(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseXor(parser);

            while (parser.AcceptToken(TokenType.Operation, "|"))
            {
                var right = ParseXor(parser);
                left = new BinOpNode(pos, BinaryOperation.BitwiseOr, left, right);
            }

            return left;
        }

        private static AstNode ParseXor(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseAnd(parser);

            while (parser.AcceptToken(TokenType.Operation, "^"))
            {
                var right = ParseAnd(parser);
                left = new BinOpNode(pos, BinaryOperation.Xor, left, right);
            }

            return left;
        }

        private static AstNode ParseAnd(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseBitshift(parser);

            while (parser.AcceptToken(TokenType.Operation, "&"))
            {
                var right = ParseBitshift(parser);
                left = new BinOpNode(pos, BinaryOperation.BitwiseAnd, left, right);
            }

            return left;
        }

        private static AstNode ParseBitshift(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseAdditive(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Operation)
            {
                if (parser.AcceptToken(TokenType.Operation, "<<"))
                {
                    var right = ParseAdditive(parser);
                    left = new BinOpNode(pos, BinaryOperation.BitshiftLeft, left, right);
                }
                else if (parser.AcceptToken(TokenType.Operation, ">>"))
                {
                    var right = ParseAdditive(parser);
                    left = new BinOpNode(pos, BinaryOperation.BitshiftRight, left, right);
                }
                else
                    break;
            }

            return left;
        }

        private static AstNode ParseAdditive(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseMultiplicative(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Operation)
            {
                if (parser.AcceptToken(TokenType.Operation, "+"))
                {
                    AstNode right = ParseMultiplicative(parser);
                    left = new BinOpNode(pos, BinaryOperation.Addition, left, right);
                }
                else if (parser.AcceptToken(TokenType.Operation, "-"))
                {
                    AstNode right = ParseMultiplicative(parser);
                    left = new BinOpNode(pos, BinaryOperation.Subtraction, left, right);
                }
                else
                    break;
            }
            return left;
        }

        private static AstNode ParseMultiplicative(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseExponent(parser);

            /*if(parser.AcceptToken(TokenType.Operation, "**"))
			{
				AstNode right = ParseMultiplicative(parser);
				return new BinOpNode(pos, BinaryOperation.Pow, left, right);
			}
			else if (parser.AcceptToken(TokenType.Operation, "//"))
			{
				AstNode right = ParseMultiplicative(parser);
				return new BinOpNode(pos, BinaryOperation.Root, left, right);
			}
			else */
            if (parser.AcceptToken(TokenType.Operation, "*"))
            {
                AstNode right = ParseMultiplicative(parser);
                return new BinOpNode(pos, BinaryOperation.Multiplication, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, "/"))
            {
                AstNode right = ParseMultiplicative(parser);
                return new BinOpNode(pos, BinaryOperation.Division, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, "%"))
            {
                AstNode right = ParseMultiplicative(parser);
                return new BinOpNode(pos, BinaryOperation.Modulus, left, right);
            }
            else if (parser.AcceptToken(TokenType.Lambda, "=>"))
            {
                AstNode body = ParseStatement(parser);
                if (!(body is CodeBlock))
                    body = new ReturnNode(body.Position, body);

                if (parser.AcceptToken(TokenType.EndOfLine))
                    parser.ExpectToken(TokenType.EndOfLine);

                if (left is ArrayInitializerNode)
                    return new LambdaFuncNode(pos,
                        ((ArrayInitializerNode) left).Value.Values.Select(x => x.ToString()).ToList(), body);
                return new LambdaFuncNode(pos, new List<string>() {left.ToString()}, body);
            }
            else
            {
                return left;
            }
        }


        private static AstNode ParseExponent(Parser parser)
        {
            int pos = parser.codePos;

            AstNode left = ParseUnary(parser);

            if (parser.AcceptToken(TokenType.Operation, "**"))
            {
                AstNode right = ParseExponent(parser);
                return new BinOpNode(pos, BinaryOperation.Pow, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, "//"))
            {
                AstNode right = ParseExponent(parser);
                return new BinOpNode(pos, BinaryOperation.Root, left, right);
            }
            else
            {
                return left;
            }
        }

        private static AstNode ParseUnary(Parser parser)
        {
            int pos = parser.codePos;

            if (parser.AcceptToken(TokenType.UnaryOperation, "!"))
                return new UnaryOpNode(pos, UnaryOperation.Not, ParseUnary(parser));
            else if (parser.AcceptToken(TokenType.MentalOperation, "++"))
                return new MentalNode(pos, "++", parser.ExpectToken(TokenType.Identifier).Value.ToString(), true);
            else if (parser.AcceptToken(TokenType.MentalOperation, "--"))
                return new MentalNode(pos, "--", parser.ExpectToken(TokenType.Identifier).Value.ToString(), true);
            else if (parser.AcceptToken(TokenType.Operation, "-"))
                return new UnaryOpNode(pos, UnaryOperation.Negate, ParseUnary(parser));
            else if (parser.AcceptToken(TokenType.UnaryOperation, "~"))
                return new UnaryOpNode(pos, UnaryOperation.Complement, ParseUnary(parser));
            else
                return ParsePostfixIncDec(parser);
        }

        private static AstNode ParsePostfixIncDec(Parser parser)
        {
            int pos = parser.codePos;

            var left = ParseFunctionCall(parser);
            if (parser.AcceptToken(TokenType.MentalOperation, "++"))
            {
                var varname = "";
                var before = false;
                if (parser.AcceptToken(TokenType.Identifier))
                {
                    varname = parser.ExpectToken(TokenType.Identifier).Value.ToString();
                    before = true;
                }
                else
                {
                    varname = parser.PreviousToken(2).Value.ToString();
                }
                return new MentalNode(pos, "++", varname, before);
            }
            else if (parser.AcceptToken(TokenType.MentalOperation, "--"))
            {
                var varname = "";
                var before = false;
                if (parser.AcceptToken(TokenType.Identifier))
                {
                    varname = parser.ExpectToken(TokenType.Identifier).Value.ToString();
                    before = true;
                }
                else
                {
                    varname = parser.PreviousToken(2).Value.ToString();
                }
                return new MentalNode(pos, "--", varname, before);
            }
            else
            {
                return left;
            }
        }

        private static AstNode ParseFunctionCall(Parser parser)
        {
            return ParseFunctionCall(parser, ParseTerm(parser));
        }

        private static AstNode ParseFunctionCall(Parser parser, AstNode left)
        {
            while (true)
            {
                int pos = parser.codePos;

                if (parser.AcceptToken(TokenType.Parentheses, "("))
                {
                    var parser1 = parser;
                    left = new FunctionCallNode(parser1.PreviousToken(2).Position, left, ParseArgList(parser1));
                }
                else if (parser.AcceptToken(TokenType.Bracket, "["))
                {
                    var parser1 = parser;
                    left = new ArrayGetNode(pos, left, ParseArrayIndexer(parser1));
                }
                else if (parser.AcceptToken(TokenType.Dot, "."))
                {
                    Token ident = parser.ExpectToken(TokenType.Identifier);

                    left = new MemberAccessNode(pos, left, ident.Value.ToString());
                }
                else
                {
                    return left;
                }
            }
        }

        private static AstNode ParseTerm(Parser parser)
        {
            int pos = parser.codePos;
            var curt = parser.CurrentToken();
            if (curt.TokenClass == TokenType.Number)
            {
                return new NumberNode(pos, Convert.ToDouble(parser.ExpectToken(TokenType.Number).Value),
                    parser.PreviousToken().Value is int);
            }
            else if (curt.TokenClass == TokenType.Parentheses)
            {
                parser.ExpectToken(TokenType.Parentheses, "(");
                AstNode statement = ParseExpression(parser);
                parser.ExpectToken(TokenType.Parentheses, ")");
                return statement;
            }
            else if (curt.TokenClass == TokenType.Bracket)
            {
                AstNode statement = ParseArrayInitializer(parser);
                return statement;
            }
            else if (curt.TokenClass == TokenType.String)
            {
                return new StringNode(pos, parser.ExpectToken(TokenType.String).Value.ToString());
            }
            else if (curt.TokenClass == TokenType.Identifier)
            {
                switch (curt.Value.ToString())
                {
                    case "lambda":
                        return ParseLambda(parser);
                    case "new":
                        return ParseInstance(parser);
                }
                return new IdentifierNode(pos, parser.ExpectToken(TokenType.Identifier).Value.ToString());
            }
            else
            {
                throw new ParseException("Unexpected " + curt.Value, pos);
            }
        }

        #endregion

        #region Arrays

        public static ArrayIndexerNode ParseArrayIndexer(Parser parser)
        {
            var ret = new ArrayIndexerNode(parser.codePos);
            parser.ExpectToken(TokenType.Bracket, "[");

            while (!parser.MatchToken(TokenType.Bracket, "]"))
            {
                if (!parser.AcceptToken(TokenType.Identifier, ":"))
                    ret.Children.Add(ParseExpression(parser));
            }
            if (ret.Children.Count == 2 && ret.Children[1].ToString() == ":")
                throw new ParseException("Expected slice number", parser.codePos);
            parser.ExpectToken(TokenType.Bracket, "]");

            return ret;
        }

        public static ArrayInitializerNode ParseArrayInitializer(Parser parser)
        {
            var ret = new ArrayInitializerNode(parser.codePos);
            parser.ExpectToken(TokenType.Bracket, "[");
            ret.IsDictionary = false;

            while (!parser.MatchToken(TokenType.Bracket, "]"))
            {
                var ct1 = ParseExpression(parser);
                if (parser.AcceptToken(TokenType.Identifier, ":"))
                {
                    ret.IsDictionary = true;
                    ret.AddItem(ct1, ParseExpression(parser));
                }
                else
                {
                    ret.AddItem(ct1);
                }
                if (!parser.AcceptToken(TokenType.Comma))
                {
                    break;
                }
            }
            parser.ExpectToken(TokenType.Bracket, "]");

            return ret;
        }

        #endregion

        #region Keywords

        public static AstNode ParseBreak(Parser parser)
        {
            int pos = parser.codePos;
            parser.ExpectToken(TokenType.Identifier, "break");
            parser.ExpectToken(TokenType.EndOfLine);
            return new BreakNode(pos);
        }

        public static AstNode ParseContinue(Parser parser)
        {
            int pos = parser.codePos;
            parser.ExpectToken(TokenType.Identifier, "continue");
            parser.ExpectToken(TokenType.EndOfLine);
            return new ContinueNode(pos);
        }

        #endregion

        public static AstNode ParseIdentifier(Parser parser)
        {
            return new IdentifierNode(parser.codePos, parser.ExpectToken(TokenType.Identifier).Value.ToString());
        }

        public static AstNode ParseInstance(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "new");
            var target = parser.ExpectToken(TokenType.Identifier).Value.ToString();
            AstNode res = new IdentifierNode(parser.codePos, target);
            while (parser.AcceptToken(TokenType.Dot, "."))
            {
                res = new MemberAccessNode(parser.codePos, res,
                    parser.ExpectToken(TokenType.Identifier).Value.ToString());
            }
            parser.ExpectToken(TokenType.Parentheses, "(");


            return new InstanceNode(pos, new FunctionCallNode(parser.codePos, res, ParseArgList(parser)));
        }

        public static AstNode ParseLambda(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "lambda");
            parser.ExpectToken(TokenType.Parentheses, "(");

            List<string> result = new List<string>();
            while (parser.MatchToken(TokenType.Identifier))
            {
                result.Add(parser.ExpectToken(TokenType.Identifier).Value.ToString());
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode body = ParseStatement(parser);

            if (parser.AcceptToken(TokenType.EndOfLine))
                parser.ExpectToken(TokenType.EndOfLine);

            return new LambdaFuncNode(pos, result, body);
        }

        public static ThreadNode ParseThread(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "thread");
            AstNode node = ParseStatement(parser);

            return new ThreadNode(pos, node);
        }

        public static UncheckedNode ParseUnchecked(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "unchecked");
            AstNode node = ParseStatement(parser);

            return new UncheckedNode(pos, node);
        }
    }
}