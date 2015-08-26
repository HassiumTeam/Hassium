using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

namespace Hassium
{
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

        public List<Token> Tokenize()
        {
            whiteSpaceMonster();

            while (HasChar())
            {
                var current = PeekChar();
                var next1 = HasChar() ? PeekChar(1) : '\0';
                var next2 = HasChar(1) ? PeekChar(2) : '\0';

                if (char.IsLetterOrDigit(current))
                    Add(scanData());
                else if (current == '@' && next1 == '"')
                    Add(scanString(true));
                else if (current == '"')
                    Add(scanString());
                else if (current == '$')
                    scanComment();
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
                else if (current == '=')
                    Add(new Token(TokenType.Comparison, ReadChar()));
                else if (current == '!' && next1 == '=')
                    Add(new Token(TokenType.Comparison, ReadChar() + "" + ReadChar()));
                else if ((current == '<' || current == '>') && next1 == '=')
                    Add(new Token(TokenType.Comparison, ReadChar() + "" + ReadChar()));
                else if (current == '<' || current == '>')
                    Add(new Token(TokenType.Comparison, ReadChar()));
                else if (current == '&' && next1 == '&')
                    Add(new Token(TokenType.Comparison, ReadChar() + "" + ReadChar()));
                else if (current == '|' && next1 == '|')
                    Add(new Token(TokenType.Comparison, ReadChar() + "" + ReadChar()));
                else if (current == '*' && next1 == '*' && next2 != '=')
                    Add(new Token(TokenType.Operation, ReadChar() + "" + ReadChar()));
                else if (current == '/' && next1 == '/' && next2 != '=')
                    Add(new Token(TokenType.Operation, ReadChar() + "" + ReadChar()));
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

                else if ("+-*/%".Contains(current) && next1 == '=')
                    Add(new Token(TokenType.OpAssign, ReadChar() + "" + ReadChar()));
                else if ("&|^".Contains(current) && next1 == '=')
                    Add(new Token(TokenType.OpAssign, ReadChar() + "" + ReadChar()));


                else
                {
                    result.Add(new Token(TokenType.Exception,
                        "Unexpected " + PeekChar().ToString() + " encountered"));
                    ReadChar();
                }

                whiteSpaceMonster();
            }

            return result;
        }

        private void scanComment()
        {
            ReadChar();
            while(PeekChar() != '$' && HasChar())
            {
                ReadChar();
            }
            ReadChar();
        }

        private Token scanString(bool verbatim = false)
        {
            ReadChar();
            if (verbatim) ReadChar();
            var finalstr = "";
            var escaping = false;
            var unicode = false;
            var curuni = "";

            while ((escaping || PeekChar() != '\"') && HasChar())
            {
                var curch = ReadChar();
                if (unicode)
                {
                    if (char.IsLetter(curch) || "abcdefABCDEF".Contains(curch + "")) curuni += curch;
                    else
                    {
                        unicode = false;
                        finalstr += char.ConvertFromUtf32(int.Parse(curuni, NumberStyles.HexNumber));
                        curuni = "";
                    }
                }
                if (escaping)
                {
                    switch (curch)
                    {
                        case '\\':
                            finalstr += '\\';
                            break;
                        case 'n':
                            finalstr += '\n';
                            break;
                        case 'r':
                            finalstr += '\r';
                            break;
                        case 't':
                            finalstr += '\t';
                            break;
                        case '"':
                            finalstr += '"';
                            break;
                        case 'x':
                            unicode = true;
                            break;
                        default:
                            finalstr += curch;
                            break;
                    }

                    escaping = false;
                }
                else
                {
                    if (curch == '\\' && !verbatim) escaping = true;
                    else finalstr += (curch).ToString();
                }
            }

            ReadChar();

            return new Token(TokenType.String, finalstr);
        }

        private Token scanData()
        {
            var finaldata = "";
            double temp = 0;
            while ((char.IsLetterOrDigit(PeekChar()) && HasChar()) || "._".Contains(PeekChar()))
            {
                finaldata += ReadChar().ToString();
            }
            if (finaldata.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
            {
                if (finaldata.Length == 2) throw new Exception("Invalid hex number: " + finaldata);
                var temp1 = 0;
                if (int.TryParse(finaldata.Substring(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out temp1))
                {
                    return new Token(TokenType.Number, temp1.ToString());
                }
                else
                {
                    throw new Exception("Invalid hex number: " + finaldata);
                }
            }
            if (finaldata.StartsWith("0o", StringComparison.InvariantCultureIgnoreCase))
            {
                if (finaldata.Length == 2) throw new Exception("Invalid octal number: " + finaldata);
                try
                {
                    return new Token(TokenType.Number, Convert.ToInt32(finaldata.Substring(2), 8).ToString());
                }
                catch
                {
                    throw new Exception("Invalid octal number: " + finaldata);
                }
            }
            if (finaldata.StartsWith("0b", StringComparison.InvariantCultureIgnoreCase))
            {
                if (finaldata.Length == 2) throw new Exception("Invalid binary number: " + finaldata);
                try
                {
                    return new Token(TokenType.Number, Convert.ToInt32(finaldata.Substring(2), 2).ToString());
                }
                catch
                {
                    throw new Exception("Invalid binary number: " + finaldata);
                }
            }
            return double.TryParse(finaldata, out temp) ? new Token(TokenType.Number, finaldata) : new Token(TokenType.Identifier, finaldata);
        }

        private void whiteSpaceMonster()
        {
            while(char.IsWhiteSpace(PeekChar())) ReadChar();
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
            if (position >= code.Length)
                throw new IndexOutOfRangeException();
            
            return code[position++];
        }
    }
}

