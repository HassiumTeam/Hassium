using System;
using System.Collections.Generic;
using System.Globalization;

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
                char orig;
                if (char.IsLetter((char)peekChar()) || (char)peekChar() == '_')
                    result.Add(scanIdentifier());
                else if (char.IsDigit((char)peekChar()))
                    result.Add(scanNumber());
                else
                {
                    switch ((char)peekChar())
                    {
                        case '\"':
                            result.Add(scanString());
                            break;
                        case '\'':
                            readChar();
                            result.Add(new Token(TokenType.Char, ((char)readChar()).ToString(), location));
                            readChar();
                            break;
                        case ';':
                            result.Add(new Token(TokenType.Semicolon, string.Empty, location));
                            readChar();
                            break;
                        case ':':
                            result.Add(new Token(TokenType.Colon, string.Empty, location));
                            readChar();
                            break;
                        case ',':
                            result.Add(new Token(TokenType.Comma, string.Empty, location));
                            readChar();
                            break;
                        case '(':
                            result.Add(new Token(TokenType.LeftParentheses, string.Empty, location));
                            readChar();
                            break;
                        case ')':
                            result.Add(new Token(TokenType.RightParentheses, string.Empty, location));
                            readChar();
                            break;
                        case '{':
                            result.Add(new Token(TokenType.LeftBrace, string.Empty, location));
                            readChar();
                            break;
                        case '}':
                            result.Add(new Token(TokenType.RightBrace, string.Empty, location));
                            readChar();
                            break;
                        case '.':
                            result.Add(new Token(TokenType.BinaryOperation, ".", location));
                            readChar();
                            break;
                        case '?':
                            result.Add(new Token(TokenType.Question, "?", location));
                            readChar();
                            break;
                        case '+':
                        case '-':
                            orig = (char)readChar();
                            if ((char)peekChar() == orig)
                                result.Add(new Token(TokenType.UnaryOperation, orig.ToString() + ((char)readChar()).ToString(), location));
                            else if ((char)peekChar() == '=')
                                result.Add(new Token(TokenType.Assignment, orig.ToString() + ((char)readChar()).ToString(), location));
                            else
                                result.Add(new Token(TokenType.BinaryOperation, orig.ToString(), location));
                            break;
                        case '*':
                        case '/':
                            orig = (char)readChar();
                            if ((char)peekChar() == orig)
                                result.Add(new Token(TokenType.BinaryOperation, orig.ToString() + ((char)readChar()).ToString(), location));
                            else
                                result.Add(new Token(TokenType.BinaryOperation, orig.ToString(), location));
                            break;
                        case '%':
                        case '^':
                            orig = (char)readChar();
                            if ((char)peekChar() == '=')
                                result.Add(new Token(TokenType.Assignment, orig.ToString() + ((char)readChar()).ToString(), location));
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
                            if ((char)peekChar() == '=')
                            {
                                readChar();
                                result.Add(new Token(TokenType.Comparison, "<=", location));
                            }
                            else if ((char)peekChar() == '-' && (char)peekChar(1) == '>')
                            {
                                readChar();readChar();
                                result.Add(new Token(TokenType.BinaryOperation, "<->", location));
                            }
                            else
                                result.Add(new Token(TokenType.Comparison, "<", location));
                            break;
                        case '>':
                            readChar();
                            if ((char)peekChar() == '=')
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
                        default:
                            throw new ParserException("Caught unknown char in lexer: " + readChar(), location);
                    }
                }
                whiteSpace();
            }

            return result;
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
            string data = "";
            while (char.IsDigit((char)peekChar()) || (char)peekChar() == '.' && peekChar() != -1)
                data += (char)readChar();
            try
            {
                return new Token(TokenType.Int64, Convert.ToInt64(data, CultureInfo.InvariantCulture).ToString(), location);
            }
            catch
            {
                return new Token(TokenType.Double, Convert.ToDouble(data, CultureInfo.InvariantCulture).ToString(), location);
            }
        }

        private Token scanString()
        {
            string str = "";
            readChar();
            while ((char)peekChar() != '\"' && peekChar() != -1)
            {
                char ch = (char)readChar();
                if (ch == '\\')
                    str += scanEscapeCode((char)readChar());
                else
                    str += ch;
            }
            readChar();

            return new Token(TokenType.String, str, location);
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

