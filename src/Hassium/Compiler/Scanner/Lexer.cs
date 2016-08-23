using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Hassium.Compiler.Scanner
{
    public class Lexer
    {
        private SourceLocation location;
        private string code;
        private int position;
        private List<Token> result;

        public List<Token> Scan(string source)
        {
            location = new SourceLocation(0, 0);
            code = source;
            position = 0;
            result = new List<Token>();
            char c;

            while (peekChar() != -1)
            {
                whiteSpace();
                if (char.IsLetter((char)peekChar()))
                    scanIdentifier();
                else if (char.IsDigit((char)peekChar()))
                    scanNumber();
                else
                {
                    switch ((char)peekChar())
                    {
                        case '\"':
                            scanString(false);
                            break;
                        case '@':
                            readChar();
                            scanString(true);
                            break;
                        case '\'':
                            readChar();
                            add(TokenType.Char, ((char)readChar()).ToString());
                            readChar();
                            break;
                        case '?':
                            add(TokenType.Question, ((char)readChar()).ToString());
                            break;
                        case '(':
                            add(TokenType.OpenParentheses, ((char)readChar()).ToString());
                            break;
                        case ')':
                            add(TokenType.CloseParentheses, ((char)readChar()).ToString());
                            break;
                        case '{':
                            add(TokenType.OpenBracket, ((char)readChar()).ToString());
                            break;
                        case '}':
                            add(TokenType.CloseBracket, ((char)readChar()).ToString());
                            break;
                        case '[':
                            add(TokenType.OpenSquare, ((char)readChar()).ToString());
                            break;
                        case ']':
                            add(TokenType.CloseSquare, ((char)readChar()).ToString());
                            break;
                        case ',':
                            add(TokenType.Comma, ((char)readChar()).ToString());
                            break;
                        case '.':
                            add(TokenType.Dot, ((char)readChar()).ToString());
                            break;
                        case ':':
                            add(TokenType.Colon, ((char)readChar()).ToString());
                            break;
                        case ';':
                            add(TokenType.Semicolon, ((char)readChar()).ToString());
                            break;
                        case '+':
                        case '-':
                        case '*':
                        case '/':
                        case '%':
                            c = (char)readChar();
                            if ((char)peekChar() == c)
                                add(TokenType.Operation, c + "" + (char)readChar());
                            else if ((char)peekChar() == '=')
                                add(TokenType.Assignment, c + "" + (char)readChar());
                            else
                                add(TokenType.Operation, c.ToString());
                            break;
                        case '>':
                        case '<':
                            c = (char)readChar();
                            if ((char)peekChar() == '=')
                                add(TokenType.Comparison, c + "" + (char)readChar());
                            else if ((char)peekChar() == c)
                                add(TokenType.Operation, c + "" + (char)readChar());
                            else
                                add(TokenType.Comparison, c.ToString());
                            break;
                        case '!':
                            readChar();
                            if ((char)peekChar() == '=')
                                add(TokenType.Comparison, "!" + (char)readChar());
                            else
                                add(TokenType.Operation, "!");
                            break;
                        case '&':
                            c = (char)readChar();
                            if ((char)peekChar() == c)
                                add(TokenType.Operation, c + "" + (char)readChar());
                            else
                                add(TokenType.Operation, c.ToString());
                            break;
                        case '|':
                            c = (char)readChar();
                            if ((char)peekChar() == c)
                                add(TokenType.Operation, c + "" + (char)readChar());
                            else
                                add(TokenType.Operation, c.ToString());
                            break;
                        case '=':
                            readChar();
                            if ((char)peekChar() == '=')
                                add(TokenType.Comparison, "=" + (char)readChar());
                            else
                                add(TokenType.Assignment, "=");
                            break;
                        case '#':
                            scanSingleLineComment();
                            break;
                        case '$':
                            scanMultiLineComment();
                            break;
                        default:
                            if (peekChar() == -1)
                                break;
                            Console.WriteLine("Unknown char, {0}, {1}!", peekChar(), (char)readChar());
                            break;
                    }
                }
            }
            return result;
        }

        private void scanNumber()
        {
            var str = new StringBuilder();
            var sep = false;
            while (peekChar() != -1 &&
                (char.IsDigit((char) peekChar()) || "abcdefABCDEF".Contains(((char)peekChar()).ToString()) || "xo-._".Contains(((char)peekChar()).ToString())))
            {
                var cchar = (char)readChar();
                if(cchar == '_')
                {
                    if (sep) break;
                    sep = true;
                }
                else
                {
                    sep = false;
                    str.Append(cchar);
                }
            }
            var final = str.ToString();
            var bname = "";
            var bsize = 0;
            if(final.StartsWith("0x"))
            {
                bname = "hex";
                bsize = 16;
            }
            else if(final.StartsWith("0b"))
            {
                bname = "binary";
                bsize = 2;
            }
            else if(final.StartsWith("0o"))
            {
                bname = "octal";
                bsize = 8;
            }
            if(bname != "")
            {
                try
                {
                    add(TokenType.Integer, Convert.ToInt64(final.Substring(2), bsize).ToString());
                }
                catch
                {
                    throw new CompileException(location, "Invalid {0} number {1}!", bname, final);
                }
            }
            else
            {
                try
                {
                    add(TokenType.Integer, long.Parse(final, NumberStyles.Any, CultureInfo.InvariantCulture).ToString());
                }
                catch
                {
                    try
                    {
                        add(TokenType.Float,
                            double.Parse(final, NumberStyles.Any, CultureInfo.InvariantCulture).ToString());
                    }
                    catch
                    {
                        throw new CompileException(location, "Invalid number: {0}!", final);
                    }
                }
            }
        }

        private void scanString(bool isVerbatim)
        {
            var str = new StringBuilder();
            readChar();
            while ((char)peekChar() != '\"' && peekChar() != -1)
            {
                char ch = (char)readChar();
                if (ch == '\\' && !isVerbatim)
                    str.Append(scanEscapeCode((char)readChar()));
                else if (ch == '#' && !isVerbatim && peekChar() == '{')
                {
                    readChar();
                    if (peekChar() == '}')
                    {
                        readChar();
                        continue;
                    }
                    result.Add(new Token(TokenType.String, str.ToString(), location));
                    str.Clear();
                    result.Add(new Token(TokenType.Operation, "+", location));
                    result.Add(new Token(TokenType.OpenParentheses, "(", location));
                    while (peekChar() != -1 && peekChar() != '}')
                        readChar();
                    readChar();
                    result.Add(new Token(TokenType.CloseParentheses, ")", location));
                    result.Add(new Token(TokenType.Operation, "+", location));
                    continue;
                }
                else
                    str.Append(ch);
            }
            readChar();

            add(TokenType.String, str.ToString());
        }

        private char scanEscapeCode(char escape)
        {
            switch (escape)
            {
                case '\\':
                    return '\\';
                case '"':
                    return '\"';
                case '\'':
                    return '\'';
                case 'a':
                    return '\a';
                case 'b':
                    return '\b';
                case 'f':
                    return '\f';
                case 'n':
                    return '\n';
                case 'r':
                    return '\r';
                case 't':
                    return '\t';
                case 'v':
                    return '\v';
                case '#':
                    return '#';
                default:
                    throw new CompileException(location, "Unknown escape sequence {0}!", escape);
            }
        }

        private void scanIdentifier()
        {
            StringBuilder sb = new StringBuilder();
            while ((char.IsLetterOrDigit((char)peekChar()) || (char)peekChar() == '_') && peekChar() != -1)
                sb.Append((char)readChar());
            string val = sb.ToString();
            add(val == "is" ? TokenType.Operation : TokenType.Identifier, val);
        }

        private void scanSingleLineComment()
        {
            readChar(); // #
            while (peekChar() != -1 && (char)peekChar() != '\n')
                readChar();
        }
        private void scanMultiLineComment()
        {
            readChar(); // $
            while (peekChar() != -1 && (char)peekChar() != '&')
                readChar();
            readChar(); // $
        }

        private void whiteSpace()
        {
            while (char.IsWhiteSpace((char)peekChar()))
                readChar();
        }

        private int peekChar(int n = 0)
        {
            return position + n < code.Length ? code[position + n] : -1;
        }
        private int readChar()
        {
            if (peekChar() == '\n')
                location = new SourceLocation(location.Row + 1, 0);
            else
                location = new SourceLocation(location.Row, location.Column + 1);
            return position < code.Length ? code[position++] : -1;
        }

        private void add(TokenType tokenType, string value)
        {
            result.Add(new Token(tokenType, value, location));
        }
    }
}

