using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Hassium
{
    public class Lexer
    {
        private string code = "";
        private int position = 0;
        private List<Token> result = new List<Token>();

        public Lexer(string code)
        {
            this.code = code;
        }

        public List<Token> Tokenize()
        {
            whiteSpaceMonster();

            while (peekChar() != -1)
            {
                if (char.IsLetterOrDigit((char)peekChar()))
                    result.Add(scanData());
                else if ((char)(peekChar()) == '\"')
                    result.Add(scanString());
                else if ((char)(peekChar()) == '$')
                    scanComment();
                else if (((char)(peekChar()) == '>' || (char)(peekChar()) == '<') && (char)(peekChar(1)) == '=')
                    result.Add(new Token(TokenType.Comparison, ((char)readChar()).ToString() + ((char)readChar()).ToString()));
                else if (((char)(peekChar()) == '<' && (char)(peekChar(1)) == '<') || ((char)(peekChar()) == '>' && (char)(peekChar(1)) == '>'))
                    result.Add(new Token(TokenType.Bitshift, ((char)readChar()).ToString() + ((char)readChar()).ToString()));
                else if ((char)(peekChar()) == ';')
                    result.Add(new Token(TokenType.EndOfLine, ((char)readChar()).ToString()));
                else if ((char)(peekChar()) == '(' || (char)(peekChar()) == ')')
                    result.Add(new Token(TokenType.Parentheses, ((char)readChar()).ToString()));
                else if ((char)(peekChar()) == '{' || (char)(peekChar()) == '}')
                    result.Add(new Token(TokenType.Brace, ((char)readChar()).ToString()));
                else if ((char) (peekChar()) == '[' || (char) (peekChar()) == ']')
                    result.Add(new Token(TokenType.Bracket, ((char) readChar()).ToString()));
                else if ((char) (peekChar()) == ',')
                    result.Add(new Token(TokenType.Comma, ((char) readChar()).ToString()));
                else if ((char)(peekChar()) == '~')
                    result.Add(new Token(TokenType.Complement, ((char)readChar()).ToString()));
                else if ("+-/*".Contains((((char) peekChar()).ToString())))
                    result.Add(new Token(TokenType.Operation, ((char) readChar()).ToString()));
                else if ("=<>".Contains((((char) peekChar()).ToString())))
                    result.Add(new Token(TokenType.Comparison, ((char) readChar()).ToString()));
                else if ((char) (peekChar()) == '!' && (char) (peekChar(1)) == '=')
                    result.Add(new Token(TokenType.Comparison,
                        ((char) readChar()).ToString() + ((char) readChar()).ToString()));
                else if ((char) (peekChar()) == '%')
                    result.Add(new Token(TokenType.Modulus, ((char) readChar()).ToString()));
                else if ((char) (peekChar()) == ':' && (char) (peekChar(1)) == '=')
                    result.Add(new Token(TokenType.Store,
                        ((char) readChar()).ToString() + ((char) readChar()).ToString()));
                else if ((char) (peekChar()) == '!' && (char) (peekChar(1)) != '=')
                    result.Add(new Token(TokenType.Not, ((char) readChar()).ToString()));
                else if ((char) (peekChar()) == '&' && (char) (peekChar(1)) == '&')
                    result.Add(new Token(TokenType.Comparison,
                        ((char) readChar()).ToString() + ((char) readChar()).ToString()));
                else if ((char) (peekChar()) == '|' && (char) (peekChar(1)) == '|')
                    result.Add(new Token(TokenType.Comparison,
                        ((char) readChar()).ToString() + ((char) readChar()).ToString()));
                else if ((char) (peekChar()) == '^')
                    result.Add(new Token(TokenType.Xor, ((char) readChar()).ToString()));
                else
                {
                    result.Add(new Token(TokenType.Exception,
                        "Unexpected " + ((char) peekChar()).ToString() + " encountered"));
                    readChar();
                }

                whiteSpaceMonster();
            }

            return result;
        }

        private void scanComment()
        {
            readChar();
            while(peekChar() != '$' && peekChar() != -1)
            {
                readChar();
            }
            readChar();
        }

        private Token scanString()
        {
            readChar();
            var finalstr = "";
            var escaping = false;
            var unicode = false;
            var curuni = "";

            while ((escaping || peekChar() != '\"') && peekChar() != -1)
            {
                var curch = (char)readChar();
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
                    if (curch == '\\') finalstr += '\\';
                    if (curch == 'n') finalstr += '\n';
                    if (curch == 'r') finalstr += '\r';
                    if (curch == 't') finalstr += '\t';
                    if (curch == '"') finalstr += '"';
                    if (curch == 'x') unicode = true;

                    escaping = false;
                }
                else
                {
                    if (curch == '\\') escaping = true;
                    else finalstr += (curch).ToString();
                }
            }

            readChar();

            return new Token(TokenType.String, finalstr);
        }

        private Token scanData()
        {
            var finaldata = "";
            double temp = 0;
            int temp1 = 0;
            while ((char.IsLetterOrDigit((char)peekChar()) && peekChar() != -1) ||
                   ".-_".Contains("" + (char)(peekChar())))
            {
                finaldata += ((char)readChar()).ToString();
            }
            if (finaldata.StartsWith("0x", StringComparison.InvariantCultureIgnoreCase))
            {
                if (finaldata.Length == 2) throw new Exception("Invalid hex number: " + finaldata);
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
            while(char.IsWhiteSpace((char)peekChar())) readChar();
        }

        private int peekChar()
        {
            if (position < code.Length)
                return code[position];
            else
                return -1;
        }
        private int peekChar(int n)
        {
            if (position + n < code.Length)
                return code[position + n];
            else
                return -1;
        }

        private int readChar()
        {
            if (position < code.Length)
                return code[position++];
            else
                return -1;
        }
    }
}

