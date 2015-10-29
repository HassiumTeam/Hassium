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
using System.Globalization;
using System.Linq;
using System.Text;
using Hassium.Interpreter;

namespace Hassium.Lexer
{
    /// <summary>
    ///     Lexer.
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

        public static string Minimize(string code)
        {
            string result = "";
            bool isIdentifier = false;

            for (int index = 0; index < code.Length; index++)
            {
                char c = code[index];

                if ((char.IsLetter(c) || "_".Contains(c)))
                {
                    isIdentifier = true;
                    result += c;
                    continue;
                }
                else if (" \r\n".Contains(c) && !isIdentifier)
                {
                    continue;
                }
                else
                {
                    isIdentifier = false;
                }

                switch (c)
                {
                    case '#':
                        while (code[index++] != '\n')
                        {
                        }
                        continue;
                    case ';':
                        if (index != 0 && !char.IsLetterOrDigit(code[index - 1]))
                        {
                            continue;
                        }
                        break;
                }

                result += c;
            }

            return result;
        }

        /// <summary>
        ///     Tokenize this instance.
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
                add(ScanNumber());
            }
            else if (char.IsLetter(current) || "_".Contains(current))
            {
                add(ScanIdentifier());
            }
            else if ("\r\n".Contains(current))
            {
                add(new Token(TokenType.EndOfLine, ReadChar()));
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
                        ScanString();
                        break;
                    case '\'':
                        ScanChar();
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
                            add(new Token(TokenType.MentalOperation, "" + ReadChar() + ReadChar()));
                        else
                            switch (next1)
                            {
                                case '=':
                                    add(new Token(TokenType.OpAssign, "" + ReadChar() + ReadChar()));
                                    break;
                                default:
                                    add(new Token(TokenType.Operation, ReadChar()));
                                    break;
                            }
                        break;
                    case '%':
                        add(new Token(TokenType.Operation, ReadChar()));
                        break;
                    case '*':
                    case '/':
                        if (next1 == current)
                            add(new Token(TokenType.Operation, "" + ReadChar() + ReadChar()));
                        else
                            switch (next1)
                            {
                                case '=':
                                    add(new Token(TokenType.OpAssign, "" + ReadChar() + ReadChar()));
                                    break;
                                default:
                                    add(new Token(TokenType.Operation, ReadChar()));
                                    break;
                            }
                        break;
                    case '.':
                        add(new Token(TokenType.Dot, ReadChar()));
                        break;
                    case ';':
                    case '\n':
                        add(new Token(TokenType.EndOfLine, ReadChar()));
                        break;
                    case ',':
                        add(new Token(TokenType.Comma, ReadChar()));
                        break;
                    case '(':
                        add(new Token(TokenType.LParen, ReadChar()));
                        break;
                    case ')':
                        add(new Token(TokenType.RParen, ReadChar()));
                        break;
                    case '[':
                        add(new Token(TokenType.LBracket, ReadChar()));
                        break;
                    case ']':
                        add(new Token(TokenType.RBracket, ReadChar()));
                        break;
                    case '{':
                        add(new Token(TokenType.LBrace, ReadChar()));
                        break;
                    case '}':
                        add(new Token(TokenType.RBrace, ReadChar()));
                        break;
                    case ':':
                        add(new Token(TokenType.Colon, ReadChar()));
                        break;
                    case '=':
                        switch (next1)
                        {
                            case '>':
                                add(new Token(TokenType.Lambda, "" + ReadChar() + ReadChar()));
                                break;
                            case '=':
                                add(new Token(TokenType.Comparison, "" + ReadChar() + ReadChar()));
                                break;
                            default:
                                add(new Token(TokenType.Assignment, "" + ReadChar()));
                                break;
                        }
                        break;
                    case '!':
                        add(next1 == '='
                            ? new Token(TokenType.Comparison, "" + ReadChar() + ReadChar())
                            : new Token(TokenType.UnaryOperation, ReadChar()));
                        break;
                    case '~':
                        add(new Token(TokenType.UnaryOperation, ReadChar()));
                        break;
                    case '&':
                    case '|':
                        add(next1 == current
                            ? new Token(TokenType.Comparison, "" + ReadChar() + ReadChar())
                            : new Token(TokenType.Operation, ReadChar()));
                        break;
                    case '^':
                        add(new Token(TokenType.Operation, ReadChar()));
                        break;
                    case '?':
                        add(next1 == '?'
                            ? new Token(TokenType.Operation, "" + ReadChar() + ReadChar())
                            : new Token(TokenType.Operation, ReadChar()));
                        break;
                    case '<':
                    case '>':
                        if(current == '>' && next1 == '>' && next2 == '>')
                        {
                            ScanEcho();
                            break;
                        }

                        if (next1 == current)
                            add(next2 == '='
                            ? new Token(TokenType.OpAssign, "" + ReadChar() + ReadChar() + ReadChar())
                            : new Token(TokenType.Operation, ReadChar() + "" + ReadChar()));
                        else if (next1 == '=')
                        {
                            if (current == '<' && next2 == '>')
                                add(new Token(TokenType.Comparison,
                                    "" + ReadChar() + ReadChar() + ReadChar()));
                            else
                                add(new Token(TokenType.Comparison, "" + ReadChar() + ReadChar()));
                        }
                        else
                            add(new Token(TokenType.Comparison, ReadChar()));
                        break;
                    default:
                        result.Add(new Token(TokenType.Exception,
                            "Unexpected " + PeekChar() + " encountered at position " + position));
                        ReadChar();
                        break;
                }
            }

            EatWhiteSpaces();
        }

        /// <summary>
        ///     Scans Comment
        /// </summary>
        private void ScanComment(bool oneLine)
        {
            ReadChar();
            while (HasChar() && PeekChar() != (oneLine ? '\n' : '$'))
                ReadChar();

            if (!oneLine && HasChar()) ReadChar();
        }

        private void ScanEcho()
        {
            ReadChar();
            ReadChar();
            ReadChar();
            StringBuilder builder = new StringBuilder();

            while(HasChar())
            {
                var current = ReadChar();
                if(current == '<' && PeekChar() == '<' && PeekChar(1) == '<')
                {
                    ReadChar();
                    ReadChar();

                    break;
                }
                builder.Append(current);
            }

            add(new Token(TokenType.Echo, builder.ToString()));
        }

        /// <summary>
        ///     Scans the string.
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="isVerbatim">If set to <c>true</c> the string is verbatim (no escape sequences).</param>
        private void ScanString(bool isVerbatim = false)
        {
            int pos = position;
            var quote = ReadChar();
            if (isVerbatim) quote = ReadChar();
            StringBuilder stringBuilder = new StringBuilder();
            var isEscaping = false;
            var isUnicode = false;
            var currentUnicodeChar = "";

            while (HasChar() && (isEscaping || PeekChar() != quote))
            {
                var currentChar = ReadChar();
                if (currentChar == '#' && !isEscaping && !isVerbatim && PeekChar() == '{')
                {
                    ReadChar();
                    if (PeekChar() == '}')
                    {
                        ReadChar();
                        continue;
                    }
                    add(new Token(TokenType.String, stringBuilder.ToString()));
                    stringBuilder.Clear();
                    isEscaping = false;
                    isUnicode = false;
                    currentUnicodeChar = "";
                    add(new Token(TokenType.Operation, '+'));
                    add(new Token(TokenType.LParen, '('));
                    while (HasChar() && PeekChar() != '}')
                        ReadToken();
                    ReadChar();
                    add(new Token(TokenType.RParen, ')'));
                    add(new Token(TokenType.Operation, '+'));
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

            if (HasChar()) ReadChar();
            else throw new ParseException("Unfinished string", pos);

            add(new Token(TokenType.String, stringBuilder));
        }

        private void ScanChar()
        {
            ReadChar();
            if (PeekChar() == '\\')
            {
                ReadChar();
                char escapeChar = ReadChar();
                switch (escapeChar)
                {
                    case 'n':
                        add(new Token(TokenType.Char, '\n'));
                        break;
                    case '\\':
                        add(new Token(TokenType.Char, '\\'));
                        break;
                    case 'r':
                        add(new Token(TokenType.Char, 'r'));
                        break;
                    case 't':
                        add(new Token(TokenType.Char, '\t'));
                        break;
                    case 'x':
                        add(new Token(TokenType.Char, 'x'));
                        break;
                    case '\'':
                        add(new Token(TokenType.Char, '\''));
                        break;
                    default:
                        throw new ParseException("Unknown escape code " + escapeChar, position);
                }
                
            }
            else
                add(new Token(TokenType.Char, ReadChar()));

            ReadChar();
        }

        private static bool IsHexChar(char c)
        {
            return "abcdefABCDEF".Contains(c);
        }

        /// <summary>
        ///     Scans a number
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
            if (finalNumber.StartsWith("0x"))
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
            if (baseName != "")
            {
                if (finalNumber.Length == 2)
                    throw new ParseException("Invalid " + baseName + " number: " + finalNumber, position);
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
                if (double.TryParse(finalNumber, NumberStyles.Any, CultureInfo.InvariantCulture, out temp))
                {
                    return (temp == Math.Truncate(temp) && !finalNumber.Contains('.'))
                        ? new Token(TokenType.Number, (int) temp)
                        : new Token(TokenType.Number, temp);
                }
                else
                {
                    throw new ParseException("Invalid number: " + finalNumber, position);
                }
            }
        }

        /// <summary>
        ///     Scans an identifier
        /// </summary>
        /// <returns>The identifier</returns>
        private Token ScanIdentifier()
        {
            var stringBuilder = new StringBuilder();
            while (HasChar() && (char.IsLetterOrDigit(PeekChar()) || "_".Contains(PeekChar())))
            {
                stringBuilder.Append(ReadChar());
            }
            if (PeekChar() == '`' && "0123456789i".Contains(PeekChar(1)))
            {
                stringBuilder.Append(ReadChar());
                stringBuilder.Append(ReadChar());
            }
            var finalId = stringBuilder.ToString();
            if (finalId.Contains('.'))
                throw new ParseException("Invalid character in Identifier: . (period)", position);
            return new Token(TokenType.Identifier, finalId);
        }

        private void EatWhiteSpaces()
        {
            while (HasChar() && char.IsWhiteSpace(PeekChar())) ReadChar();
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

        private void add(Token token)
        {
            result.Add(new Token(token.TokenClass, token.Value, position - token.Value.ToString().Length));
        }
    }
}