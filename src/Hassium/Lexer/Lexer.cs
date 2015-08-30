using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace Hassium
{
    /// <summary>
    /// Lexer.
    /// </summary>
    public class Lexer
    {
        private string code;
        private int position;
        private List<Token> result = new List<Token>();

        public Lexer(string code)
        {
            this.code = code;
        }

        private void Add(Token token)
        {
            result.Add(token);
        }

        /// <summary>
        /// Tokenize this instance.
        /// </summary>
        public List<Token> Tokenize()
        {
            EatWhiteSpaces();
            
            while (HasChar())
            {
                EatWhiteSpaces();

                var current = PeekChar();
                var next1 = HasChar() ? PeekChar(1) : '\0';
                var next2 = HasChar(1) ? PeekChar(2) : '\0';

<<<<<<< HEAD
                if (current == '.')
                    Add(new Token(TokenType.Dot, current));
                else if (char.IsLetterOrDigit(current))
                    Add(scanData());
                else if (current == '@' && next1 == '"')
                    Add(scanString(true));
                else if (current == '"')
                    Add(scanString());
                else if (current == '$')
                    scanComment();
                else if (current == '#')
                    singleComment();
                else if (current == '+' && next1 == '+')
                    Add(new Token(TokenType.MentalOperation, ReadChar() + "" + ReadChar()));
                else if (current == '-' && next1 == '-')
                    Add(new Token(TokenType.MentalOperation, ReadChar() + "" + ReadChar()));
                else if (current == ';')
                    Add(new Token(TokenType.EndOfLine, ReadChar()));
                else if (current == ',')
                    Add(new Token(TokenType.Comma, ReadChar()));
                else if (current == '(' || current == ')')
                    Add(new Token(TokenType.Parentheses, ReadChar()));
                else if (current == '[' || current == ']')
                    Add(new Token(TokenType.Bracket, ReadChar()));
                else if (current == '{' || current == '}')
                    Add(new Token(TokenType.Brace, ReadChar()));
                else if (current == ':' && next1 == '=')
                    Add(new Token(TokenType.Assignment, ReadChar() + "" + ReadChar()));
                else if(current == ':')
                    Add(new Token(TokenType.Identifier, ReadChar()));
                else if(current == '=' && next1 == '>')
                    Add(new Token(TokenType.Lambda, ReadChar() + "" + ReadChar()));
                else if (current == '=')
                    Add(new Token(TokenType.Comparison, ReadChar()));
                else if (current == '!' && next1 == '=')
                    Add(new Token(TokenType.Comparison, ReadChar() + "" + ReadChar()));
                
                else if (current == '&' && next1 == '&')
                    Add(new Token(TokenType.Comparison, ReadChar() + "" + ReadChar()));
                else if (current == '|' && next1 == '|')
                    Add(new Token(TokenType.Comparison, ReadChar() + "" + ReadChar()));
                else if (current == '?' && next1 == '?')
                    Add(new Token(TokenType.Operation, ReadChar() + "" + ReadChar()));
                else if(current == '?')
                    Add(new Token(TokenType.Operation, ReadChar()));
                else if (current == '*' && next1 == '*' && next2 != '=')
                    Add(new Token(TokenType.Operation, ReadChar() + "" + ReadChar()));
                else if (current == '/' && next1 == '/' && next2 != '=')
                    Add(new Token(TokenType.Operation, ReadChar() + "" + ReadChar()));
                else if(current == '<' && next1 == '=' && next2 == '>')
                    Add(new Token(TokenType.Comparison, ReadChar() + "" + ReadChar() + "" + ReadChar()));
                else if (current == '>' && next1 == '>' && next2 != '=')
                    Add(new Token(TokenType.Operation, ReadChar() + "" + ReadChar()));
                else if (current == '<' && next1 == '<' && next2 != '=')
                    Add(new Token(TokenType.Operation, ReadChar() + "" + ReadChar()));
                else if ("+-*/%".Contains(current) && !("/*=".Contains(next1)))
                    Add(new Token(TokenType.Operation, ReadChar()));
                else if ("&|^".Contains(current) && next1 != '=')
                    Add(new Token(TokenType.Operation, ReadChar()));

                else if ("~!-".Contains(current))
                    Add(new Token(TokenType.UnaryOperation, ReadChar()));
                else if (current == '*' && next1 == '*' && next2 == '=')
                    Add(new Token(TokenType.OpAssign, ReadChar() + "" + ReadChar() + "" + ReadChar()));
                else if (current == '/' && next1 == '/' && next2 == '=')
                    Add(new Token(TokenType.OpAssign, ReadChar() + "" + ReadChar() + "" + ReadChar()));
                else if (current == '>' && next1 == '>' && next2 == '=')
                    Add(new Token(TokenType.OpAssign, ReadChar() + "" + ReadChar() + "" + ReadChar()));
                else if (current == '<' && next1 == '<' && next2 == '=')
                    Add(new Token(TokenType.OpAssign, ReadChar() + "" + ReadChar() + "" + ReadChar()));

                else if ((current == '<' || current == '>') && next1 == '=')
                    Add(new Token(TokenType.Comparison, ReadChar() + "" + ReadChar()));
                else if (current == '<' || current == '>')
                    Add(new Token(TokenType.Comparison, ReadChar()));

                else if ("+-*/%".Contains(current) && next1 == '=')
                    Add(new Token(TokenType.OpAssign, ReadChar() + "" + ReadChar()));
                else if ("&|^".Contains(current) && next1 == '=')
                    Add(new Token(TokenType.OpAssign, ReadChar() + "" + ReadChar()));

=======
                if("0123456789.".Contains(current))
                {
                    Add(ScanNumber());
                    continue;
                }
                if(char.IsLetter(current) || "_".Contains(current))
                {
                    Add(ScanIdentifier());
                    continue;
                }
>>>>>>> master

                switch (current)
                {
                    case '@':
                        if (next1 == '"' || next1 == '\'')
                            Add(ScanString(true));
                        break;
                    case '"':
                    case '\'':
                        Add(ScanString());
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
                    case ';':
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
                        if (next1 == '=')
                            Add(new Token(TokenType.Assignment, "" + ReadChar() + ReadChar()));
                        else
                            Add(new Token(TokenType.Identifier, ReadChar()));
                        break;
                    case '=':
                        if (next1 == '>')
                            Add(new Token(TokenType.Lambda, "" + ReadChar() + ReadChar()));
                        else
                            Add(new Token(TokenType.Comparison, ReadChar()));
                        break;
                    case '!':
                        if (next1 == '=')
                            Add(new Token(TokenType.Comparison, "" + ReadChar() + ReadChar()));
                        else
                            Add(new Token(TokenType.UnaryOperation, ReadChar()));
                        break;
                    case '~':
                        Add(new Token(TokenType.UnaryOperation, ReadChar()));
                        break;
                    case '&':
                    case '|':
                        if (next1 == current)
                            Add(new Token(TokenType.Comparison, "" + ReadChar() + ReadChar()));
                        else
                            Add(new Token(TokenType.Operation, ReadChar()));
                        break;
                    case '^':
                        Add(new Token(TokenType.Operation, ReadChar()));
                        break;
                    case '?':
                        if (next1 == '?')
                            Add(new Token(TokenType.Operation, "" + ReadChar() + ReadChar()));
                        else
                            Add(new Token(TokenType.Operation, ReadChar()));
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
                            "Unexpected " + Functions.StringFunctions.AddSlashes(new object[] { PeekChar().ToString() }) + " encountered at position " + position));
                        ReadChar();
                        break;
                }

                EatWhiteSpaces();
            }

            return result;
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
        private Token ScanString(bool isVerbatim = false)
        {
            var quote = ReadChar();
            if (isVerbatim) ReadChar();
            StringBuilder stringBuilder = new StringBuilder();
            var isEscaping = false;
            var isUnicode = false;
            var currentUnicodeChar = "";

            while (HasChar() && (isEscaping || PeekChar() != quote))
            {
                var currentChar = ReadChar();
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
            else throw new Exception("Unfinished string at position " + position);

            return new Token(TokenType.String, stringBuilder);
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
            while (HasChar() && (char.IsDigit(PeekChar()) || IsHexChar(PeekChar()) || "xo.".Contains(PeekChar())))
            {
                stringBuilder.Append(ReadChar().ToString());
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
                if (finalNumber.Length == 2) throw new Exception("Invalid " + baseName + " number: " + finalNumber);
                try
                {
                    return new Token(TokenType.Number, Convert.ToInt32(finalNumber.Substring(2), baseSize).ToString());
                }
                catch
                {
                    throw new Exception("Invalid " + baseName + " number: " + finalNumber);
                }
            }
            else
            {
                double temp = 0;
                if(double.TryParse(finalNumber, out temp))
                {
                    return new Token(TokenType.Number, temp);
                }
                else
                {
                    throw new Exception("Invalid number: " + finalNumber);
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
            var finalId = stringBuilder.ToString();
            if (finalId.Contains('.')) throw new Exception("Invalid character in Identifier: . (period)");
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

