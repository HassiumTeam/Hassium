using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Hassium.Interpreter;

namespace Hassium.Lexer
{
    /// <summary>
    /// Lexer.
    /// </summary>
    public class Lexer
    {
        private string code;
        public string Trimmed = "";
        private int position;
        private List<Token> result = new List<Token>();

        public Lexer(string code)
        {
            this.code = code;
        }

        private void Add(Token token)
        {
            result.Add(new Token(token.TokenClass, token.Value, position - token.Value.ToString().Length));
        }

        /// <summary>
        /// Tokenize this instance.
        /// </summary>
        public List<Token> Tokenize()
        {
            EatWhiteSpaces();
            
            while (HasChar())
            {
                ReadToken();
            }

            return result;
        }

        private void ReadToken()
        {
            EatWhiteSpaces();

            var current = PeekChar();
            var next1 = HasChar() ? PeekChar(1) : '\0';
            var next2 = HasChar(1) ? PeekChar(2) : '\0';

            if ("0123456789".Contains(current))
            {
                Add(ScanNumber());
                try
                {
                    Trimmed += result[result.Count - 1].Value.ToString();
                }
                catch
                {
                }
            }
            else if (char.IsLetter(current) || "_".Contains(current))
            {
                Add(ScanIdentifier());
                try
                {
                    Trimmed += result[result.Count - 1].Value.ToString() + " ";
                }
                catch
                {
                }
            }
            else
            {
                switch (current)
                {
                    case '@':
                        if (next1 == '"' || next1 == '\'')
                            ScanString(true);
                        break;
                    case '"':
                    case '\'':
                        ScanString();
                        break;
                    case '$':
                        ScanComment(false);
                        break;
                    case '#':
                        ScanComment(true);
                        break;
                    case '+':
                    case '-':
                        if (next1 == current)
                            Add(new Token(TokenType.MentalOperation, "" + ReadChar() + ReadChar()));
                        else
                            switch (next1)
                            {
                                case '=':
                                    Add(new Token(TokenType.OpAssign, "" + ReadChar() + ReadChar()));
                                    break;
                                default:
                                    Add(new Token(TokenType.Operation, ReadChar()));
                                    break;
                            }
                        break;
                    case '%':
                        Add(new Token(TokenType.Operation, ReadChar()));
                        break;
                    case '*':
                    case '/':
                        if (next1 == current)
                            Add(new Token(TokenType.Operation, "" + ReadChar() + ReadChar()));
                        else
                            switch (next1)
                            {
                                case '=':
                                    Add(new Token(TokenType.OpAssign, "" + ReadChar() + ReadChar()));
                                    break;
                                default:
                                    Add(new Token(TokenType.Operation, ReadChar()));
                                    break;
                            }
                        break;
                    case '.':
                        Add(new Token(TokenType.Dot, ReadChar()));
                        break;
                    case ';':
                    case '\n':
                        Add(new Token(TokenType.EndOfLine, ReadChar()));
                        break;
                    case ',':
                        Add(new Token(TokenType.Comma, ReadChar()));
                        break;
                    case '(':
                    case ')':
                        Add(new Token(TokenType.Parentheses, ReadChar()));
                        break;
                    case '[':
                    case ']':
                        Add(new Token(TokenType.Bracket, ReadChar()));
                        break;
                    case '{':
                    case '}':
                        Add(new Token(TokenType.Brace, ReadChar()));
                        break;
                    case ':':
                        Add(new Token(TokenType.Identifier, ReadChar()));
                        break;
                    case '=':
                        if (next1 == '>')
                            Add(new Token(TokenType.Lambda, "" + ReadChar() + ReadChar()));
                        else if (next1 == '=')
                            Add(new Token(TokenType.Comparison, "" + ReadChar() + ReadChar()));
                        else
                            Add(new Token(TokenType.Assignment, "" + ReadChar()));
                        break;
                    case '!':
                        Add(next1 == '='
                            ? new Token(TokenType.Comparison, "" + ReadChar() + ReadChar())
                            : new Token(TokenType.UnaryOperation, ReadChar()));
                        break;
                    case '~':
                        Add(new Token(TokenType.UnaryOperation, ReadChar()));
                        break;
                    case '&':
                    case '|':
                        Add(next1 == current
                            ? new Token(TokenType.Comparison, "" + ReadChar() + ReadChar())
                            : new Token(TokenType.Operation, ReadChar()));
                        break;
                    case '^':
                        Add(new Token(TokenType.Operation, ReadChar()));
                        break;
                    case '?':
                        Add(next1 == '?'
                            ? new Token(TokenType.Operation, "" + ReadChar() + ReadChar())
                            : new Token(TokenType.Operation, ReadChar()));
                        break;
                    case '<':
                    case '>':
                        if (next1 == current)
                            Add(new Token(TokenType.Operation, ReadChar() + "" + ReadChar()));
                        else if (next1 == '=')
                        {
                            if (current == '<' && next2 == '>')
                                Add(new Token(TokenType.Comparison,
                                    "" + ReadChar() + ReadChar() + ReadChar()));
                            else
                                Add(new Token(TokenType.Comparison, "" + ReadChar() + ReadChar()));
                        }
                        else
                            Add(new Token(TokenType.Comparison, ReadChar()));
                        break;
                    default:
                        result.Add(new Token(TokenType.Exception,
                            "Unexpected " + PeekChar() + " encountered at position " + position));
                        ReadChar();
                        break;
                }

                try
                {
                    Trimmed += result[result.Count - 1].Value.ToString();
                }
                catch
                {
                }
            }

            EatWhiteSpaces();
        }

        /// <summary>
        /// Scans Comment
        /// </summary>
        private void ScanComment(bool oneLine)
        {
            ReadChar();
            while (HasChar() && PeekChar() != (oneLine ? '\n' : '$'))
                ReadChar();

            if (!oneLine && HasChar()) ReadChar();
        }
        /// <summary>
        /// Scans the string.
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="isVerbatim">If set to <c>true</c> the string is verbatim (no escape sequences).</param>
        private void ScanString(bool isVerbatim = false)
        {
            var quote = ReadChar();
            if (isVerbatim) quote = ReadChar();
            StringBuilder stringBuilder = new StringBuilder();
            var isEscaping = false;
            var isUnicode = false;
            var currentUnicodeChar = "";

            while (HasChar() && (isEscaping || PeekChar() != quote))
            {
                var currentChar = ReadChar();
                if(currentChar == '#' && !isEscaping && !isVerbatim && PeekChar() == '{')
                {
                    ReadChar();
                    if (PeekChar() == '}')
                    {
                        ReadChar();
                        continue;
                    }
                    Add(new Token(TokenType.String, stringBuilder.ToString()));
                    stringBuilder.Clear();
                    isEscaping = false;
                    isUnicode = false;
                    currentUnicodeChar = "";
                    Add(new Token(TokenType.Operation, '+'));
                    Add(new Token(TokenType.Parentheses, '('));
                    while (HasChar() && PeekChar() != '}')
                        ReadToken();
                    ReadChar();
                    Add(new Token(TokenType.Parentheses, ')'));
                    Add(new Token(TokenType.Operation, '+'));
                    continue;
                }
                if (isUnicode)
                {
                    if (char.IsLetter(currentChar) || IsHexChar(currentChar)) currentUnicodeChar += currentChar;
                    else
                    {
                        isUnicode = false;
                        stringBuilder.Append(char.ConvertFromUtf32(int.Parse(currentUnicodeChar, NumberStyles.HexNumber)));
                        currentUnicodeChar = "";
                    }
                }
                if (isEscaping)
                {
                    switch (currentChar)
                    {
                        case '\\':
                            stringBuilder.Append('\\');
                            break;
                        case 'n':
                            stringBuilder.Append('\n');
                            break;
                        case 'r':
                            stringBuilder.Append('\r');
                            break;
                        case 't':
                            stringBuilder.Append('\t');
                            break;
                        case '"':
                            stringBuilder.Append('"');
                            break;
                        case '#':
                            stringBuilder.Append('#');
                            break;
                        case 'x':
                            isUnicode = true;
                            break;
                        default:
                            stringBuilder.Append(currentChar);
                            break;
                    }

                    isEscaping = false;
                }
                else
                {
                    if (currentChar == '\\' && !isVerbatim) isEscaping = true;
                    else stringBuilder.Append(currentChar);
                }
            }

            if(HasChar()) ReadChar();
            else throw new ParseException("Unfinished string", position);

            Add(new Token(TokenType.String, stringBuilder));
        }
        private static bool IsHexChar(char c)
        {
            return "abcdefABCDEF".Contains(c);
        }
        /// <summary>
        /// Scans a number
        /// </summary>
        /// <returns>The number token</returns>
        private Token ScanNumber()
        {
            var stringBuilder = new StringBuilder();
            bool separator = false;
            while (HasChar() && (char.IsDigit(PeekChar()) || IsHexChar(PeekChar()) || "xo._".Contains(PeekChar())))
            {
                var cchar = ReadChar();
                if (cchar == '_')
                {
                    if (separator) break;
                    separator = true;
                }
                else
                {
                    separator = false;
                    stringBuilder.Append(cchar);
                }
            }
            var finalNumber = stringBuilder.ToString();
            var baseName = "";
            var baseSize = 0;
            if(finalNumber.StartsWith("0x"))
            {
                baseName = "hex";
                baseSize = 16;
            }
            if (finalNumber.StartsWith("0b"))
            {
                baseName = "binary";
                baseSize = 2;
            }
            if (finalNumber.StartsWith("0o"))
            {
                baseName = "octal";
                baseSize = 8;
            }
            if(baseName != "")
            {
                if (finalNumber.Length == 2) throw new ParseException("Invalid " + baseName + " number: " + finalNumber, position);
                try
                {
                    return new Token(TokenType.Number, Convert.ToInt32(finalNumber.Substring(2), baseSize).ToString());
                }
                catch
                {
                    throw new ParseException("Invalid " + baseName + " number: " + finalNumber, position);
                }
            }
            else
            {
                double temp = 0;
                if(double.TryParse(finalNumber, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                {
                    return (temp == Math.Truncate(temp) && !finalNumber.Contains('.')) ? new Token(TokenType.Number, (int)temp) : new Token(TokenType.Number, temp);
                }
                else
                {
                    throw new ParseException("Invalid number: " + finalNumber, position);
                }
            }
        }

        /// <summary>
        /// Scans an identifier
        /// </summary>
        /// <returns>The identifier</returns>
        private Token ScanIdentifier()
        {
            var stringBuilder = new StringBuilder();
            while (HasChar() && (char.IsLetterOrDigit(PeekChar()) || "_".Contains(PeekChar())))
            {
                stringBuilder.Append(ReadChar());
            }
            if(PeekChar() == '`' && "0123456789i".Contains(PeekChar(1)))
            {
                stringBuilder.Append(ReadChar());
                stringBuilder.Append(ReadChar());
            }
            var finalId = stringBuilder.ToString();
            if (finalId.Contains('.')) throw new ParseException("Invalid character in Identifier: . (period)", position);
            return new Token(TokenType.Identifier, finalId);
        }

        private void EatWhiteSpaces()
        {
            while(HasChar() && char.IsWhiteSpace(PeekChar())) ReadChar();
        }

        private char PeekChar(int n = 0)
        {
            return position + n < code.Length ? code[position + n] : '\0';
        }

        private bool HasChar(int number = 0)
        {
            return position + number < code.Length;
        }

        private char ReadChar()
        {
            return code[position++];
        }
    }
}

