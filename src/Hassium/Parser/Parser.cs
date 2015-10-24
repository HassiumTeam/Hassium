// Copyright (c) 2015, HassiumTeam (JacobMisirian, zdimension) All rights reserved.
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//  * Redistributions of source code must retain the above copyright notice, this list
//    of conditions and the following disclaimer.
// 
//  * Redistributions in binary form must reproduce the above copyright notice, this
//    list of conditions and the following disclaimer in the documentation and/or
//    other materials provided with the distribution.
// Neither the name of the copyright holder nor the names of its contributors may be
// used to endorse or promote products derived from this software without specific
// prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
// SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT ,STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
// DAMAGE.

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using Hassium.Interpreter;
using Hassium.Lexer;
using Hassium.Parser.Ast;

namespace Hassium.Parser
{
    /// <summary>
    ///     Parser.
    /// </summary>
    public class Parser
    {
        private List<Token> tokens;
        private string code { get; set; }
        private int position;

        private int codePosition
        {
            get { return CurrentToken().Position; }
        }

        private bool endOfStream
        {
            get { return tokens.Count <= position; }
        }

        public Parser(List<Token> tokens, string code)
        {
            this.tokens = tokens;
            this.code = code;
        }

        /// <summary>
        ///     ParseStatement this instance.
        /// </summary>
        public AstNode Parse()
        {
            CodeBlock block = new CodeBlock(0);
            while (!endOfStream)
                block.Children.Add(ParseStatement(this));
            return block;
        }

        public Token CurrentToken()
        {
            return position >= tokens.Count ? new Token(TokenType.Identifier, "") : tokens[position];
        }

        public Token PreviousToken(int delay = 1)
        {
            return position - delay >= tokens.Count ? new Token(TokenType.Identifier, null) : tokens[position - delay];
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

        public Token ExpectToken(TokenType clazz, string value = "")
        {
            return ExpectToken("Expected " + (value == "" ? clazz.ToString() : value) + ", got " + (position >= tokens.Count ? "EOF" : CurrentToken().Value + " [" + CurrentToken().TokenClass + "]"), clazz, value);
        }

        public Token ExpectToken(string msg, TokenType clazz, string value = "")
        {
            if (value == "" ? MatchToken(clazz) : MatchToken(clazz, value)) return tokens[position++];
            else throw new ParseException(msg, code[codePosition - 1] == '\n' ? codePosition - 1 : codePosition);
        }

        private static AstNode ParseStatement(Parser parser)
        {
            if (parser.MatchToken(TokenType.Identifier))
            {
                switch (parser.CurrentToken().Value.ToString().ToLower())
                {
                    case "if":
                        return parseIf(parser);
                    case "while":
                        return parseWhile(parser);
                    case "for":
                        return parseFor(parser);
                    case "foreach":
                        return parseForEach(parser);
                    case "try":
                        return parseTryCatch(parser);
                    case "class":
                        return parseClass(parser);
                    case "func":
                        return parseFunc(parser);
                    case "property":
                        return parseProperty(parser);
                    case "lambda":
                        return parseLambda(parser);
                    case "thread":
                        return parseThread(parser);
                    case "unchecked":
                        return parseUnchecked(parser);
                    case "switch":
                        return parseSwitch(parser);
                    case "return":
                        return parseReturn(parser);
                    case "yield":
                        return parseYield(parser);
                    case "continue":
                        return parseContinue(parser);
                    case "break":
                        return parseBreak(parser);
                    case "use":
                        return parseUse(parser);
                    case "do":
                        return parseDo(parser);
                    case "goto":
                        return parseGoto(parser);
                    case "enum":
                        return parseEnum(parser);
                }
            }
            else if(parser.MatchToken(TokenType.Echo))
            {
                return new EchoNode(parser.codePosition, parser.ExpectToken(TokenType.Echo).ToString());
            }
            else if (parser.MatchToken(TokenType.LBrace))
            {
                return parseCodeBlock(parser);
            }
            if (parser.MatchToken(TokenType.RBrace) || parser.MatchToken(TokenType.RBracket) || parser.MatchToken(TokenType.RParen))
            {
                throw new ParseException(
                        "Unexpected closing " + parser.CurrentToken().Value + " [" + parser.CurrentToken().TokenClass +
                        "]", parser.codePosition);
            }
            AstNode expr = parseExpression(parser);
            if(parser.MatchToken(TokenType.EndOfLine))
            parser.ExpectToken(TokenType.EndOfLine);
            return expr;
        }

        #region Blocks

        private static AstNode parseClass(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "class");
            string name = parser.ExpectToken("Expected class name", TokenType.Identifier).Value.ToString();
            string extends = "";
            if (parser.AcceptToken(TokenType.Colon))
                extends = parser.ExpectToken("Expected base class name", TokenType.Identifier).Value.ToString();
            AstNode body = parseCodeBlock(parser);

            return new ClassNode(position, name, body, extends);
        }

        private static AstNode parseGoto(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "goto");
            string name = parser.ExpectToken("Expected label name", TokenType.Identifier).Value.ToString();
            parser.ExpectToken(TokenType.EndOfLine);
            return new GotoNode(position, name);
        }

        private static AstNode parseFunc(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "func");
            string name = parser.ExpectToken("Expected function name", TokenType.Identifier).Value.ToString();

            parser.ExpectToken(TokenType.LParen);
            bool parms = false;
            List<string> result = new List<string>();
            while (!parser.MatchToken(TokenType.RParen))
            {
                string pname = parser.ExpectToken("Expected argument name", TokenType.Identifier).Value.ToString();
                if (parser.PreviousToken(-1).TokenClass == TokenType.RParen && parser.AcceptToken(TokenType.Operation, "*"))
                    parms = true;
                result.Add(pname);
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            parser.ExpectToken(TokenType.RParen);

            FunctionCallNode constr = null;

            if (name == "new" && parser.AcceptToken(TokenType.Colon))
            {
                int tempPosition = parser.codePosition;
                var callee = parser.ExpectToken("Expected 'this' or 'base'", TokenType.Identifier);
                if (callee.Value.ToString() == "base" || callee.Value.ToString() == "this")
                    constr = new FunctionCallNode(tempPosition, new IdentifierNode(tempPosition, callee.Value.ToString()), parseArgList(parser));
                else
                    throw new ParseException("Expected 'this' or 'base' in constructor", tempPosition);
            }

            AstNode body = null;

            if (parser.AcceptToken(TokenType.Lambda))
            {
                body = ParseStatement(parser);
                if (!(body is CodeBlock))
                    body = new ReturnNode(body.Position, body);

                if (parser.MatchToken(TokenType.EndOfLine))
                    parser.ExpectToken(TokenType.EndOfLine);
            }
            else body = ParseStatement(parser);

            return new FuncNode(position, name, result, body, constr, parms);
        }

        private static AstNode parseProperty(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "property");
            string name = parser.ExpectToken("Expected property name", TokenType.Identifier).Value.ToString();

            AstNode getBody = null;
            AstNode setBody = null;
            FuncNode getnode = null;
            FuncNode setnode = null;
            bool autoProp = false;

            if (parser.AcceptToken(TokenType.EndOfLine))
            {
                getBody = new CodeBlock(parser.codePosition);
                getBody.Children.Add(new ReturnNode(parser.codePosition,
                    new MemberAccessNode(parser.codePosition, new IdentifierNode(parser.codePosition, "this"), "__prop__" + name)));
                setBody = new CodeBlock(parser.codePosition);
                setBody.Children.Add(new BinOpNode(parser.codePosition, BinaryOperation.Assignment,
                    new MemberAccessNode(parser.codePosition, new IdentifierNode(parser.codePosition, "this"), "__prop__" + name),
                    new IdentifierNode(parser.codePosition, "value")));
            }
            else if (parser.AcceptToken(TokenType.Lambda, "=>"))
            {
                getBody = new CodeBlock(parser.codePosition);
                getBody.Children.Add(new ReturnNode(parser.codePosition, parseExpression(parser)));

                setBody = new CodeBlock(parser.codePosition);
                setBody.Children.Add(new BinOpNode(parser.codePosition, BinaryOperation.Assignment, parseExpression(parser),
                    new IdentifierNode(parser.codePosition, "value")));
                parser.ExpectToken(TokenType.EndOfLine);
            }
            else
            {
                parser.ExpectToken(TokenType.LBrace);

                parser.ExpectToken(TokenType.Identifier, "get");

                if (parser.AcceptToken(TokenType.EndOfLine, ";"))
                {
                    autoProp = true;
                    getBody = new CodeBlock(parser.codePosition);
                    getBody.Children.Add(new ReturnNode(parser.codePosition,
                        new MemberAccessNode(parser.codePosition, new IdentifierNode(parser.codePosition, "this"),
                            "__prop__" + name)));
                }
                else
                    getBody = parseCodeBlock(parser);


                if (parser.AcceptToken(TokenType.Identifier, "set"))
                {
                    if (parser.AcceptToken(TokenType.EndOfLine, ";"))
                    {
                        setBody = new CodeBlock(parser.codePosition);
                        setBody.Children.Add(new BinOpNode(parser.codePosition, BinaryOperation.Assignment,
                            new MemberAccessNode(parser.codePosition, new IdentifierNode(parser.codePosition, "this"),
                                "__prop__" + name),
                            new IdentifierNode(parser.codePosition, "value")));
                    }
                    else
                    {
                        if (autoProp)
                            throw new ParseException("An auto-property cannot declare a set body", position);
                        setBody = parseCodeBlock(parser);
                    }
                }
                else
                {
                    if (autoProp)
                        throw new ParseException("An auto-property must contain a set statement", position);
                }

                parser.ExpectToken(TokenType.RBrace);
            }

            getnode = new FuncNode(parser.codePosition, "__getprop__" + name, new List<string> {"this"}, getBody);
            setnode = setBody == null
                ? null
                : new FuncNode(parser.codePosition, "__setprop__" + name, new List<string> {"this", "value"}, setBody);

            return new PropertyNode(position, name, getnode, setnode);
        }

        private static AstNode parseCodeBlock(Parser parser)
        {
            CodeBlock block = new CodeBlock(parser.codePosition);
            Token opening = parser.ExpectToken(TokenType.LBrace);

            while (!parser.endOfStream && !parser.MatchToken(TokenType.RBrace))
                block.Children.Add(ParseStatement(parser));

            try
            {
                parser.ExpectToken(TokenType.RBrace);
            }
            catch
            {
                throw new ParseException("Unterminated code block", opening.Position);
            }
            return block;
        }

        #endregion

        private static ArgListNode parseArgList(Parser parser)
        {
            ArgListNode ret = new ArgListNode(parser.codePosition);
            parser.ExpectToken(TokenType.LParen);

            while (!parser.MatchToken(TokenType.RParen))
            {
                ret.Children.Add(parseExpression(parser));
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            parser.ExpectToken("Unterminated argument list", TokenType.RParen);

            return ret;
        }

        private static AstNode parseUse(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "use");
            string path = "";
            UseNode ret = null;
            if (parser.MatchToken(TokenType.Identifier))
            {
                path = parser.ExpectToken(TokenType.Identifier).Value.ToString();
                ret = new UseNode(position, path, "", true, true);
            }
            else
            {
                path = parser.ExpectToken("Expected module name or path", TokenType.String).Value.ToString();
                bool global = true;
                string name = "";
                if (parser.AcceptToken(TokenType.Identifier, "as"))
                {
                    global = false;
                    name = parser.ExpectToken("Expected module name", TokenType.Identifier).Value.ToString();
                }

                ret = path.EndsWith(".dll")
                    ? new UseNode(position, path, name, global, false, true)
                    : new UseNode(position, path, name, global, false);
            }
            parser.ExpectToken(TokenType.EndOfLine);
            return ret;
        }


        private static AstNode parseTryCatch(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "try");
            AstNode tryBody = ParseStatement(parser);
            parser.ExpectToken("Expected catch block", TokenType.Identifier, "catch");
            AstNode catchBody = ParseStatement(parser);

            if (parser.AcceptToken(TokenType.Identifier, "finally"))
            {
                AstNode finallyBody = ParseStatement(parser);
                return new TryNode(position, tryBody, catchBody, finallyBody);
            }

            return new TryNode(position, tryBody, catchBody);
        }

        #region Conditions

        private static AstNode parseIf(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "if");
            parser.ExpectToken(TokenType.LParen);
            AstNode predicate = parseExpression(parser);
            parser.ExpectToken(TokenType.RParen);
            AstNode ifBody = ParseStatement(parser);
            if (parser.AcceptToken(TokenType.Identifier, "else"))
            {
                int elsePosition = parser.codePosition;
                AstNode elseBody = ParseStatement(parser);
                return new IfNode(elsePosition, predicate, ifBody, elseBody);
            }

            return new IfNode(position, predicate, ifBody);
        }

        private static AstNode parseSwitch(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "switch");
            parser.ExpectToken(TokenType.LParen);
            AstNode predicate = parseExpression(parser);
            parser.ExpectToken(TokenType.RParen);
            parser.ExpectToken(TokenType.LBrace);
            var cases = new List<CaseNode>();
            CaseNode defn = null;
            while (parser.MatchToken(TokenType.Identifier, "case"))
            {
                int cpos = parser.codePosition;
                parser.ExpectToken(TokenType.Identifier, "case");
                List<AstNode> pred = new List<AstNode>();
                if (parser.MatchToken(TokenType.Comparison) || parser.MatchToken(TokenType.Identifier, "in") ||
                    parser.MatchToken(TokenType.Identifier, "is"))
                {
                    parser.tokens.Insert(parser.position,
                        new Token(TokenType.Identifier, "__caseval", parser.codePosition));
                    pred.Add((BinOpNode) parseExpression(parser));
                }
                else if ((parser.MatchToken(TokenType.Identifier) || parser.MatchToken(TokenType.Number)) && parser.PreviousToken(-1).ToString() == "to")
                {
                    var lower = parseExpression(parser);
                    parser.ExpectToken(TokenType.Identifier, "to");
                    if (parser.MatchToken(TokenType.Colon))
                        throw new ParseException("Expected upper range value", parser.codePosition);
                    var upper = parseExpression(parser);
                    pred.Add(new BinOpNode(cpos, BinaryOperation.Range, lower, upper));
                }
                else pred = new List<AstNode> {parseExpression(parser)};
                parser.ExpectToken("Expected case value", TokenType.Colon);
                while (parser.MatchToken(TokenType.Identifier, "case"))
                {
                    parser.ExpectToken(TokenType.Identifier, "case");
                    var pred2 = parseExpression(parser);
                    parser.ExpectToken(TokenType.Colon);
                    pred.Add(pred2);
                }
                var cbody = ParseStatement(parser);
                cases.Add(new CaseNode(cpos, pred, cbody));
            }
            if (parser.MatchToken(TokenType.Identifier, "default"))
            {
                int dpos = parser.codePosition;
                parser.ExpectToken(TokenType.Identifier, "default");
                parser.ExpectToken(TokenType.Colon);
                var dbody = ParseStatement(parser);
                defn = new CaseNode(dpos, null, dbody);
            }
            parser.ExpectToken(TokenType.RBrace);
            return new SwitchNode(position, predicate, cases, defn);
        }

        #endregion

        #region Loops

        private static AstNode parseFor(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "for");
            parser.ExpectToken(TokenType.LParen);
            AstNode left = new CodeBlock(parser.codePosition);
            AstNode predicate = new CodeBlock(parser.codePosition);
            AstNode right = new CodeBlock(parser.codePosition);
            if (!parser.AcceptToken(TokenType.EndOfLine, ";"))
                left = ParseStatement(parser);
            if (!parser.AcceptToken(TokenType.EndOfLine, ";"))
                predicate = ParseStatement(parser);
            if (!parser.AcceptToken(TokenType.EndOfLine, ";"))
                right = ParseStatement(parser);
            parser.ExpectToken(TokenType.RParen);
            AstNode forBody = ParseStatement(parser);

            return new ForNode(position, left, predicate, right, forBody);
        }

        private static AstNode parseForEach(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "foreach");
            parser.ExpectToken(TokenType.LParen);
            AstNode needle = null;
            needle = parser.CurrentToken().Value.ToString() == "["
                ? ParseArrayInitializer(parser)
                : ParseIdentifier(parser);
            parser.ExpectToken(TokenType.Identifier, "in");
            AstNode haystack = ParseStatement(parser);
            parser.ExpectToken(TokenType.RParen);
            AstNode forBody = ParseStatement(parser);

            return new ForEachNode(position, needle, haystack, forBody);
        }

        private static AstNode parseDo(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "do");
            AstNode doBody = ParseStatement(parser);
            parser.ExpectToken(TokenType.Identifier, "while");
            parser.ExpectToken(TokenType.LParen);
            AstNode predicate = parseExpression(parser);
            parser.ExpectToken(TokenType.RParen);
            parser.ExpectToken(TokenType.EndOfLine);

            return new DoNode(position, predicate, doBody);
        }

        private static AstNode parseWhile(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "while");
            parser.ExpectToken(TokenType.LParen);
            AstNode predicate = parseExpression(parser);
            parser.ExpectToken(TokenType.RParen);
            AstNode whileBody = ParseStatement(parser);
            if (parser.AcceptToken(TokenType.Identifier, "else"))
            {
                AstNode elseBody = ParseStatement(parser);
                return new WhileNode(position, predicate, whileBody, elseBody);
            }

            return new WhileNode(position, predicate, whileBody);
        }

        #endregion

        private static AstNode parseReturn(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "return");
            return parser.AcceptToken(TokenType.EndOfLine) ? new ReturnNode(position, null) : new ReturnNode(position, ParseStatement(parser));
        }

        private static AstNode parseYield(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "yield");
            if(parser.MatchToken(TokenType.EndOfLine)) throw new ParseException("Expected yield value", parser.codePosition);
            return new ReturnNode(position, ParseStatement(parser), true);
        }

        private static AstNode parseEnum(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "enum");
            string name = parser.ExpectToken("Expected enum name", TokenType.Identifier).Value.ToString();
            parser.ExpectToken(TokenType.LBrace, "{");

            CodeBlock body = new CodeBlock(position);
           for (int x = 0; !parser.MatchToken(TokenType.RBrace); x++)
            {
                string entry = parser.ExpectToken(TokenType.Identifier).Value.ToString();
                int entryNumber = 0;

                if (parser.AcceptToken(TokenType.Assignment))
                    entryNumber = Convert.ToInt32(parser.ExpectToken(TokenType.Number).Value);
                else
                    entryNumber = x;

                body.Children.Add(new BinOpNode(position, BinaryOperation.Assignment, new IdentifierNode(position, entry), new NumberNode(position, entryNumber, true)));
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            parser.ExpectToken(TokenType.RBrace, "}");

            return new EnumNode(position, name, body);
        }

        private static AstNode parseTuple(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "tuple");
            string name = "";
            if(parser.MatchToken(TokenType.Identifier))
            {
                name = parser.ExpectToken(TokenType.Identifier).Value.ToString();
            }
            bool useParen = parser.AcceptToken(TokenType.LParen, "(");

            AstNode body = new CodeBlock(position);
            while (useParen ? !parser.MatchToken(TokenType.RParen) : !parser.MatchToken(TokenType.EndOfLine))
            {
                body.Children.Add(parseExpression(parser));
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            if(useParen) parser.ExpectToken(TokenType.RParen);
            return new TupleNode(position, name, body);
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

        private static AstNode parseExpression(Parser parser)
        {
            return parseAssignment(parser);
        }

        private static AstNode parseAssignment(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = parseConditional(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Assignment ||
                   parser.CurrentToken().TokenClass == TokenType.OpAssign)
            {
                if (!left.CanBeModified)
                    throw new ParseException("Trying to assign a read-only expression (try using == instead of =)", parser.CurrentToken().Position);
                if (parser.AcceptToken(TokenType.Assignment))
                {
                    AstNode right = parseConditional(parser);
                    left = new BinOpNode(position, BinaryOperation.Assignment, left, right);
                }
                else if (parser.AcceptToken(TokenType.OpAssign))
                {
                    var assigntype = GetBinaryOp(parser.PreviousToken().Value.ToString());
                    var right = parseConditional(parser);
                    left = new BinOpNode(position, BinaryOperation.Assignment, assigntype, left, right);
                }
                else
                    break;
            }

            return left;
        }

        private static AstNode parseConditional(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = parseLogicalOr(parser);

            while (parser.PreviousToken(-1).TokenClass != TokenType.Dot && parser.AcceptToken(TokenType.Operation, "?"))
            {
                AstNode ifbody = null;
                if (!parser.MatchToken(TokenType.Colon))
                    ifbody = parseConditional(parser);
                AstNode elsebody = null;
                if (parser.AcceptToken(TokenType.Colon))
                    elsebody = parseConditional(parser);
                left = new ConditionalOpNode(position, left, ifbody, elsebody);
            }

            return left;
        }

        private static AstNode parseLogicalOr(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = parseLogicalAnd(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Comparison || parser.CurrentToken().TokenClass == TokenType.Operation)
                if (parser.AcceptToken(TokenType.Comparison, "||"))
                {
                    var right = parseLogicalAnd(parser);
                    left = new BinOpNode(position, BinaryOperation.LogicalOr, left, right);
                }
                else if (parser.AcceptToken(TokenType.Operation, "??"))
                {
                    var right = parseLogicalAnd(parser);
                    left = new BinOpNode(position, BinaryOperation.NullCoalescing, left, right);
                }
                else
                    break;

            return left;
        }

        private static AstNode parseLogicalAnd(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = parseEquality(parser);

            while (parser.AcceptToken(TokenType.Comparison, "&&"))
            {
                var right = parseEquality(parser);
                left = new BinOpNode(position, BinaryOperation.LogicalAnd, left, right);
            }

            return left;
        }


        private static AstNode parseEquality(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = parseIn(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Comparison || parser.CurrentToken().TokenClass == TokenType.Operation)
                if (parser.AcceptToken(TokenType.Comparison, "=="))
                {
                    var right = parseIn(parser);
                    left = new BinOpNode(position, BinaryOperation.Equals, left, right);
                }
                else if (parser.AcceptToken(TokenType.Comparison, "!="))
                {
                    var right = parseIn(parser);
                    left = new BinOpNode(position, BinaryOperation.NotEqualTo, left, right);
                }
                else
                    break;

            return left;
        }

        private static AstNode parseIn(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = ParseComparison(parser);

            while (parser.AcceptToken(TokenType.Identifier, "in"))
            {
                var right = ParseComparison(parser);
                left = new BinOpNode(position, BinaryOperation.In, left, right);
            }

            return left;
        }

        private static AstNode ParseComparison(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = parseOr(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Comparison || parser.CurrentToken().Value.ToString() == "is")
                if (parser.AcceptToken(TokenType.Comparison, "<"))
                {
                    var right = parseOr(parser);
                    left = new BinOpNode(position, BinaryOperation.LessThan, left, right);
                }
                else if (parser.AcceptToken(TokenType.Comparison, ">"))
                {
                    var right = parseOr(parser);
                    left = new BinOpNode(position, BinaryOperation.GreaterThan, left, right);
                }
                else if (parser.AcceptToken(TokenType.Comparison, "<="))
                {
                    var right = parseOr(parser);
                    left = new BinOpNode(position, BinaryOperation.LesserOrEqual, left, right);
                }
                else if (parser.AcceptToken(TokenType.Comparison, ">="))
                {
                    var right = parseOr(parser);
                    left = new BinOpNode(position, BinaryOperation.GreaterOrEqual, left, right);
                }
                else if (parser.AcceptToken(TokenType.Comparison, "<=>"))
                {
                    var right = parseOr(parser);
                    left = new BinOpNode(position, BinaryOperation.CombinedComparison, left, right);
                }
                else if (parser.AcceptToken(TokenType.Identifier, "is"))
                {
                    AstNode right = parseOr(parser);
                    left = new BinOpNode(position, BinaryOperation.Is, left, right);
                }
                else
                    break;

            return left;
        }

        private static AstNode parseOr(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = parseXor(parser);

            while (parser.AcceptToken(TokenType.Operation, "|"))
            {
                var right = parseXor(parser);
                left = new BinOpNode(position, BinaryOperation.BitwiseOr, left, right);
            }

            return left;
        }

        private static AstNode parseXor(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = parseAnd(parser);

            while (parser.AcceptToken(TokenType.Operation, "^"))
            {
                var right = parseAnd(parser);
                left = new BinOpNode(position, BinaryOperation.Xor, left, right);
            }

            return left;
        }

        private static AstNode parseAnd(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = parseBitShift(parser);

            while (parser.AcceptToken(TokenType.Operation, "&"))
            {
                var right = parseBitShift(parser);
                left = new BinOpNode(position, BinaryOperation.BitwiseAnd, left, right);
            }

            return left;
        }

        private static AstNode parseBitShift(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = ParseAdditive(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Operation)
            {
                if (parser.AcceptToken(TokenType.Operation, "<<"))
                {
                    var right = ParseAdditive(parser);
                    left = new BinOpNode(position, BinaryOperation.BitshiftLeft, left, right);
                }
                else if (parser.AcceptToken(TokenType.Operation, ">>"))
                {
                    var right = ParseAdditive(parser);
                    left = new BinOpNode(position, BinaryOperation.BitshiftRight, left, right);
                }
                else
                    break;
            }

            return left;
        }

        private static AstNode ParseAdditive(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = parseMultiplicative(parser);

            while (parser.CurrentToken().TokenClass == TokenType.Operation)
            {
                if (parser.AcceptToken(TokenType.Operation, "+"))
                {
                    AstNode right = parseMultiplicative(parser);
                    left = new BinOpNode(position, BinaryOperation.Addition, left, right);
                }
                else if (parser.AcceptToken(TokenType.Operation, "-"))
                {
                    AstNode right = parseMultiplicative(parser);
                    left = new BinOpNode(position, BinaryOperation.Subtraction, left, right);
                }
                else
                    break;
            }
            return left;
        }

        private static AstNode parseMultiplicative(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = ParseExponent(parser);
            while(parser.CurrentToken().TokenClass == TokenType.Operation || parser.CurrentToken().TokenClass == TokenType.Lambda)
            {
                if (parser.AcceptToken(TokenType.Operation, "*"))
                {
                    AstNode right = ParseExponent(parser);
                    left = new BinOpNode(position, BinaryOperation.Multiplication, left, right);
                }
                else if (parser.AcceptToken(TokenType.Operation, "/"))
                {
                    AstNode right = ParseExponent(parser);
                    left = new BinOpNode(position, BinaryOperation.Division, left, right);
                }
                else if (parser.AcceptToken(TokenType.Operation, "%"))
                {
                    AstNode right = ParseExponent(parser);
                    left = new BinOpNode(position, BinaryOperation.Modulus, left, right);
                }
                else if (parser.AcceptToken(TokenType.Lambda, "=>"))
                {
                    AstNode body = ParseStatement(parser);
                    if (!(body is CodeBlock))
                        body = new ReturnNode(body.Position, body);

                    if (parser.MatchToken(TokenType.EndOfLine))
                        parser.ExpectToken(TokenType.EndOfLine);
                    return new LambdaFuncNode(position, new List<string> {left.ToString()}, body);
                }
                else break;
            }
            return left;
        }


        private static AstNode ParseExponent(Parser parser)
        {
            int position = parser.codePosition;

            AstNode left = ParseUnary(parser);

            if (parser.AcceptToken(TokenType.Operation, "**"))
            {
                AstNode right = ParseExponent(parser);
                return new BinOpNode(position, BinaryOperation.Pow, left, right);
            }
            else if (parser.AcceptToken(TokenType.Operation, "//"))
            {
                AstNode right = ParseExponent(parser);
                return new BinOpNode(position, BinaryOperation.Root, left, right);
            }
            else
                return left;
        }

        private static AstNode ParseUnary(Parser parser)
        {
            int position = parser.codePosition;

            if (parser.AcceptToken(TokenType.UnaryOperation, "!"))
                return new UnaryOpNode(position, UnaryOperation.Not, ParseUnary(parser));
            else if (parser.AcceptToken(TokenType.MentalOperation, "++"))
                return new IncDecNode(position, "++", ParseFunctionCall(parser), true);
            else if (parser.AcceptToken(TokenType.MentalOperation, "--"))
                return new IncDecNode(position, "--", ParseFunctionCall(parser), true);
            else if (parser.AcceptToken(TokenType.Operation, "-"))
                return new UnaryOpNode(position, UnaryOperation.Negate, ParseUnary(parser));
            else if (parser.AcceptToken(TokenType.UnaryOperation, "~"))
                return new UnaryOpNode(position, UnaryOperation.Complement, ParseUnary(parser));
            else
                return ParsePostfixIncDec(parser);
        }

        private static AstNode ParsePostfixIncDec(Parser parser)
        {
            int position = parser.codePosition;

            var left = ParseFunctionCall(parser);
            if (parser.AcceptToken(TokenType.MentalOperation, "++"))
                return new IncDecNode(position, "++", left, false);
            else if (parser.AcceptToken(TokenType.MentalOperation, "--"))
                return new IncDecNode(position, "--", left, false);
            else
                return left;
        }

        private static AstNode ParseFunctionCall(Parser parser)
        {
            return ParseFunctionCall(parser, ParseTerm(parser));
        }

        private static AstNode ParseFunctionCall(Parser parser, AstNode left)
        {
            while (true)
            {
                int position = parser.codePosition;

                if (parser.MatchToken(TokenType.LParen))
                {
                    var parser1 = parser;
                    left = new FunctionCallNode(parser1.PreviousToken().Position, left, parseArgList(parser1));
                }
                else if (parser.MatchToken(TokenType.LBracket))
                {
                    var parser1 = parser;
                    left = new ArrayGetNode(position, left, ParseArrayIndexer(parser1));
                }
                else if (parser.AcceptToken(TokenType.Dot, ".") || (parser.MatchToken(TokenType.Operation, "?") && parser.PreviousToken(-1).TokenClass == TokenType.Dot))
                {
                    bool checknull = false;
                    if(parser.AcceptToken(TokenType.Operation, "?"))
                    {
                        checknull = true;
                        parser.AcceptToken(TokenType.Dot, ".");
                    }
                    bool dictaccess = parser.AcceptToken(TokenType.Operation, "&");
                    Token ident = parser.ExpectToken("Expected member name", TokenType.Identifier);
                    if(!checknull) checknull = parser.AcceptToken(TokenType.Operation, "?");

                    left = new MemberAccessNode(position, left, (dictaccess ? "&" : "") + ident.Value, checknull);
                }
                else
                    return left;
            }
        }

        private static AstNode ParseTerm(Parser parser)
        {
            int position = parser.codePosition;
            var curt = parser.CurrentToken();
            switch (curt.TokenClass)
            {
                case TokenType.Number:
                    return new NumberNode(position, Convert.ToDouble(parser.ExpectToken(TokenType.Number).Value),
                        parser.PreviousToken().Value is int);
                case TokenType.LParen:
                {
                    AstNode statement = parseArgList(parser);
                    if(parser.AcceptToken(TokenType.Lambda))
                        {
                            AstNode body = ParseStatement(parser);
                            if (!(body is CodeBlock))
                                body = new ReturnNode(body.Position, body);

                            if (parser.MatchToken(TokenType.EndOfLine))
                                parser.ExpectToken(TokenType.EndOfLine);

                            return new LambdaFuncNode(statement.Position, statement.Children.Select(x => ((IdentifierNode)x).Identifier).ToList(), body);
                        }
                        else
                        {
                            if(statement.Children.Count > 1)
                            {
                                throw new ParseException("Expected ) [RParen]", statement.Children[1].Position);
                            }
                        }
                    return statement.Children[0];
                }
                case TokenType.LBracket:
                {
                    AstNode statement = ParseArrayInitializer(parser);
                    return statement;
                }
                case TokenType.String:
                    return new StringNode(position, parser.ExpectToken(TokenType.String).Value.ToString());
                case TokenType.Char:
                    return new CharNode(position, Convert.ToChar(parser.ExpectToken(TokenType.Char).Value));
                case TokenType.Identifier:
                    if (parser.PreviousToken(-1).Value.ToString() == ":" && parser.PreviousToken().TokenClass == TokenType.EndOfLine)
                    {
                        var t = new LabelNode(parser.codePosition, parser.ExpectToken(TokenType.Identifier).Value.ToString(),
                            parser.codePosition);
                        parser.ExpectToken(TokenType.Colon);
                        return t;
                    }
                    switch (curt.Value.ToString())
                    {
                        case "lambda":
                            return parser.PreviousToken(-1).TokenClass != TokenType.LParen
                                ? new IdentifierNode(position, parser.ExpectToken(TokenType.Identifier).Value.ToString())
                                : parseLambda(parser);
                        case "new":
                            return ParseInstance(parser);
                        case "tuple":
                            if (parser.PreviousToken(-1).TokenClass == TokenType.LParen
                                ||
                                (parser.PreviousToken(-1).TokenClass == TokenType.Identifier &&
                                 parser.PreviousToken(-2).TokenClass == TokenType.LParen)) return parseTuple(parser);
                            break;
                    }
                    return new IdentifierNode(position, parser.ExpectToken(TokenType.Identifier).Value.ToString());
                case TokenType.RBrace:
                case TokenType.RBracket:
                case TokenType.RParen:
                    throw new ParseException(
                        "Unexpected closing " + parser.CurrentToken().Value + " [" + parser.CurrentToken().TokenClass +
                        "]", parser.codePosition);

                default:
                    throw new ParseException("Unexpected " + curt.Value + " [" + curt.TokenClass + "]", position);
            }
        }

        #endregion

        #region Arrays

        public static ArrayIndexerNode ParseArrayIndexer(Parser parser)
        {
            var ret = new ArrayIndexerNode(parser.codePosition);
            parser.ExpectToken(TokenType.LBracket);

            while (!parser.MatchToken(TokenType.RBracket))
            {
                if (!parser.AcceptToken(TokenType.Colon))
                    ret.Children.Add(parseExpression(parser));
            }
            if (ret.Children.Count == 2 && ret.Children[1].ToString() == ":")
                throw new ParseException("Expected slice number", parser.codePosition);
            parser.ExpectToken("Unterminated array indexer", TokenType.RBracket);

            return ret;
        }

        public static ArrayInitializerNode ParseArrayInitializer(Parser parser)
        {
            var ret = new ArrayInitializerNode(parser.codePosition);
            parser.ExpectToken(TokenType.LBracket);
            ret.IsDictionary = false;

            while (!parser.MatchToken(TokenType.RBracket))
            {
                if (parser.AcceptToken(TokenType.Colon))
                {
                    if(parser.MatchToken(TokenType.RBracket))
                    {
                        ret.IsDictionary = true;
                        break;
                    }
                    else throw new ParseException("Expected ]", parser.codePosition);
                }
                var ct1 = parseExpression(parser);
                if (parser.AcceptToken(TokenType.Colon))
                {
                    ret.IsDictionary = true;
                    ret.AddItem(ct1, parseExpression(parser));
                }
                else
                    ret.AddItem(ct1);
                
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }
            parser.ExpectToken("Unterminated array initializer", TokenType.RBracket);

            return ret;
        }

        #endregion

        #region Keywords

        private static AstNode parseBreak(Parser parser)
        {
            int position = parser.codePosition;
            parser.ExpectToken(TokenType.Identifier, "break");
            parser.ExpectToken(TokenType.EndOfLine);
            return new BreakNode(position);
        }

        private static AstNode parseContinue(Parser parser)
        {
            int position = parser.codePosition;
            parser.ExpectToken(TokenType.Identifier, "continue");
            parser.ExpectToken(TokenType.EndOfLine);
            return new ContinueNode(position);
        }

        #endregion

        private static AstNode ParseIdentifier(Parser parser)
        {
            return new IdentifierNode(parser.codePosition, parser.ExpectToken(TokenType.Identifier).Value.ToString());
        }

        private static AstNode ParseInstance(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "new");
            if (parser.AcceptToken(TokenType.LBrace))
            {
                var ret = new ObjectInitializerNode(position);
                while (!parser.MatchToken(TokenType.RBrace))
                {
                    var k = ParseIdentifier(parser);
                    parser.ExpectToken(TokenType.Colon);
                    var v = parseExpression(parser);
                    ret.AddItem((IdentifierNode)k, v);
                    if (!parser.AcceptToken(TokenType.Comma))
                        break;
                }
                parser.ExpectToken("Unterminated object initializer", TokenType.RBrace);
                return ret;
            }
            else
            {
                var target = parser.ExpectToken(TokenType.Identifier).Value.ToString();
                AstNode result = new IdentifierNode(parser.codePosition, target);
                while (parser.AcceptToken(TokenType.Dot, "."))
                {
                    result = new MemberAccessNode(parser.codePosition, result,
                        parser.ExpectToken(TokenType.Identifier).Value.ToString());
                }

                return new InstanceNode(position,
                    new FunctionCallNode(parser.codePosition, result, parseArgList(parser)));
            }
        }

        private static AstNode parseLambda(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "lambda");
            parser.ExpectToken(TokenType.LParen);

            List<string> result = new List<string>();
            while (parser.MatchToken(TokenType.Identifier))
            {
                result.Add(parser.ExpectToken(TokenType.Identifier).Value.ToString());
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            parser.ExpectToken(TokenType.RParen);
            AstNode body = ParseStatement(parser);

            if (parser.MatchToken(TokenType.EndOfLine))
                parser.ExpectToken(TokenType.EndOfLine);

            return new LambdaFuncNode(position, result, body);
        }

        public static ThreadNode parseThread(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "thread");
            AstNode node = ParseStatement(parser);

            return new ThreadNode(position, node);
        }

        public static UncheckedNode parseUnchecked(Parser parser)
        {
            int position = parser.codePosition;

            parser.ExpectToken(TokenType.Identifier, "unchecked");
            AstNode node = ParseStatement(parser);

            return new UncheckedNode(position, node);
        }
    }
}