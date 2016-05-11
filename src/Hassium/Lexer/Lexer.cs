using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Hassium.Lexer
{
    public class Lexer
    {
        private List<Token> result;
        private int position;
        private string code;
        private SourceLocation location;

        public List<Token> Scan(string source)
        {
            result = new List<Token>();
            position = 0;
            code = source;
            location = new SourceLocation(1, 0);

            whiteSpace();
            while (position < code.Length)
            {
                scanToken();
            }

            return result;
        }

        private void scanToken()
        {
            char orig;
            if (char.IsLetter((char)peekChar()) || (char)peekChar() == '_')
                result.Add(scanIdentifier());
            else if (char.IsDigit((char)peekChar()))
                result.Add(scanNumber());
            else
            {
                switch ((char)peekChar())
                {
                    case '@':
                        if (peekChar() == '"')
                        {
                            readChar();
                            result.Add(scanString(true));
                        }
                        break;
                    case '\"':
                        result.Add(scanString(false));
                        break;
                    case '\'':
                        readChar();
                        result.Add(new Token(TokenType.Char, ((char)readChar()).ToString(), location));
                        readChar();
                        break;
                    case ';':
                        result.Add(new Token(TokenType.Semicolon, ";", location));
                        readChar();
                        break;
                    case ':':
                        result.Add(new Token(TokenType.Colon, ":", location));
                        readChar();
                        break;
                    case ',':
                        result.Add(new Token(TokenType.Comma, ",", location));
                        readChar();
                        break;
                    case '(':
                        result.Add(new Token(TokenType.LeftParentheses, "(", location));
                        readChar();
                        break;
                    case ')':
                        result.Add(new Token(TokenType.RightParentheses, ")", location));
                        readChar();
                        break;
                    case '{':
                        result.Add(new Token(TokenType.LeftBrace, "{", location));
                        readChar();
                        break;
                    case '}':
                        result.Add(new Token(TokenType.RightBrace, "}", location));
                        readChar();
                        break;
                    case '.':
                        result.Add(new Token(TokenType.BinaryOperation, ".", location));
                        readChar();
                        break;
                    case '?':
                        readChar();
                        if (peekChar() == '?')
                        {
                            readChar();
                            result.Add(new Token(TokenType.BinaryOperation, "??", location));
                        }
                        else
                            result.Add(new Token(TokenType.Question, "?", location));
                        break;
                    case '+':
                    case '-':
                        orig = (char)readChar();
                        if ((char)peekChar() == orig)
                            result.Add(new Token(TokenType.UnaryOperation, orig.ToString() + (char)readChar(), location));
                        else if ((char)peekChar() == '=')
                            result.Add(new Token(TokenType.Assignment, orig.ToString() + (char)readChar(), location));
                        else
                            result.Add(new Token(TokenType.BinaryOperation, orig.ToString(), location));
                        break;
                    case '*':
                    case '/':
                        orig = (char)readChar();
                        if ((char)peekChar() == orig)
                            result.Add(new Token(TokenType.BinaryOperation, orig.ToString() + (char)readChar(), location));
                        else if ((char)peekChar() == '=')
                            result.Add(new Token(TokenType.Assignment, orig.ToString() + (char)readChar(), location));
                        else
                            result.Add(new Token(TokenType.BinaryOperation, orig.ToString(), location));
                        break;
                    case '%':
                    case '^':
                        orig = (char)readChar();
                        if ((char)peekChar() == '=')
                            result.Add(new Token(TokenType.Assignment, orig.ToString() + (char)readChar(), location));
                        else
                            result.Add(new Token(TokenType.BinaryOperation, orig.ToString(), location));
                        break;
                    case '|':
                        readChar();
                        if ((char)peekChar() == '|')
                        {
                            readChar();
                            result.Add(new Token(TokenType.BinaryOperation, "||", location));
                        }
                        else if ((char)peekChar() == '=')
                        {
                            readChar();
                            result.Add(new Token(TokenType.Assignment, "|=", location));
                        }
                        else
                            result.Add(new Token(TokenType.BinaryOperation, "|", location));
                        break;
                    case '&':
                        readChar();
                        if ((char)peekChar() == '&')
                        {
                            readChar();
                            result.Add(new Token(TokenType.BinaryOperation, "&&", location));
                        }
                        else if ((char)peekChar() == '=')
                        {
                            readChar();
                            result.Add(new Token(TokenType.Assignment, "&=", location));
                        }
                        else
                            result.Add(new Token(TokenType.BinaryOperation, "&", location));
                        break;
                    case '=':
                        readChar();
                        if ((char)peekChar() == '=')
                        {
                            readChar();
                            result.Add(new Token(TokenType.Comparison, "==", location));
                        }
                        else
                            result.Add(new Token(TokenType.Assignment, "=", location));
                        break;
                    case '!':
                        readChar();
                        if ((char)peekChar() == '=')
                        {
                            readChar();
                            result.Add(new Token(TokenType.Comparison, "!=", location));
                        }
                        else
                            result.Add(new Token(TokenType.UnaryOperation, "!", location));
                        break;
                    case '<':
                        readChar();
                        if(peekChar() == '<')
                        {
                            readChar();
                            result.Add(new Token(TokenType.BinaryOperation, "<<", location));
                        }
                        else if ((char)peekChar() == '=')
                        {
                            readChar();
                            result.Add(new Token(TokenType.Comparison, "<=", location));
                        }
                        else if ((char)peekChar() == '-' && (char)peekChar(1) == '>')
                        {
                            readChar(); readChar();
                            result.Add(new Token(TokenType.BinaryOperation, "<->", location));
                        }
                        else
                            result.Add(new Token(TokenType.Comparison, "<", location));
                        break;
                    case '>':
                        readChar();
                        if (peekChar() == '>')
                        {
                            readChar();
                            result.Add(new Token(TokenType.BinaryOperation, ">>", location));
                        }
                        else if ((char)peekChar() == '=')
                        {
                            readChar();
                            result.Add(new Token(TokenType.Comparison, ">=", location));
                        }
                        else
                            result.Add(new Token(TokenType.Comparison, ">", location));
                        break;
                    case '[':
                        readChar();
                        result.Add(new Token(TokenType.LeftSquare, "[", location));
                        break;
                    case ']':
                        readChar();
                        result.Add(new Token(TokenType.RightSquare, "]", location));
                        break;
                    case '#':
                        scanSingleComment();
                        break;
                    case '$':
                        scanMultilineComment();
                        break;
                    case '~':
                        readChar();
                        result.Add(new Token(TokenType.UnaryOperation, "~", location));
                        break;
                    default:
                        throw new ParserException("Caught unknown char in lexer: " + readChar(), location);
                }
            }
            whiteSpace();
        }

        private void whiteSpace()
        {
            while (char.IsWhiteSpace((char)peekChar()) && peekChar() != -1)
                readChar();
        }

        private Token scanIdentifier()
        {
            string str = "";
            while (char.IsLetterOrDigit((char)peekChar()) || (char)peekChar() == '_' && peekChar() != -1)
                str += (char)readChar();
            return new Token(TokenType.Identifier, str, location);
        }

        private Token scanNumber()
        {
            var str = new StringBuilder();
            var sep = false;
            while (peekChar() != -1 &&
                   (char.IsDigit((char) peekChar()) || "abcdefABCDEF".Contains((char)peekChar()) || "xo._".Contains((char) peekChar())))
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
                    return new Token(TokenType.Int64, Convert.ToInt64(final.Substring(2), bsize).ToString(), location);
                }
                catch
                {
                    throw new ParserException("Invalid " + bname + " number: " + final, location);
                }
            }
            else
            {
                try
                {
                    return new Token(TokenType.Int64, long.Parse(final, NumberStyles.Any, CultureInfo.InvariantCulture).ToString(), location);
                }
                catch
                {
                    try
                    {
                        return new Token(TokenType.Double,
                            double.Parse(final, NumberStyles.Any, CultureInfo.InvariantCulture).ToString(), location);
                    }
                    catch
                    {
                        throw new ParserException("Invalid number: " + final, location);
                    }
                }
            }
        }

        private Token scanString(bool isVerbatim)
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
                    result.Add(new Token(TokenType.BinaryOperation, "+", location));
                    result.Add(new Token(TokenType.LeftParentheses, "(", location));
                    while (peekChar() != -1 && peekChar() != '}')
                        scanToken();
                    readChar();
                    result.Add(new Token(TokenType.RightParentheses, ")", location));
                    result.Add(new Token(TokenType.BinaryOperation, "+", location));
                    continue;
                }
                else
                    str.Append(ch);
            }
            readChar();

            return new Token(TokenType.String, str.ToString(), location);
        }

        private void scanSingleComment()
        {
            readChar();
            while (peekChar() != -1 && peekChar() != '\n')
                readChar();
        }

        private void scanMultilineComment()
        {
            readChar();
            while(peekChar() != -1 && peekChar() != '$')
                readChar();
            readChar();
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
                    throw new ParserException("Unknown escape code \\" + escape, location);
            }
        }

        private int peekChar(int n = 0)
        {
            return position + n < code.Length ? code[position + n] : -1;
        }
        private int readChar()
        {
            if (position >= code.Length)
                return -1;
            if (peekChar() == '\n')
                location = new SourceLocation(location.Line + 1, 0);
            else
                location = new SourceLocation(location.Line, location.Letter + 1);

            return code[position++];
        }
    }
}

