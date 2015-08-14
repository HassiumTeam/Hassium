using System;
using System.Collections.Generic;
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
                {
                    result.Add(scanData());
                }
                else if ((char)(peekChar()) == '\"')
                {
                    result.Add(scanString());
                }
                else if ((char)(peekChar()) == '$')
                {
                    scanComment();
                }
                else if ((char)(peekChar()) == '(' || (char)(peekChar()) == ')')
                {
                    result.Add(new Token("PARENTHESES", ((char)readChar()).ToString()));
                }
                else if ((char)(peekChar()) == ',')
                {
                    result.Add(new Token("COMMA", ((char)readChar()).ToString()));
                }
                else if ("+-/*".Contains((((char)peekChar()).ToString())))
                {
                    result.Add(new Token("OPERATION", ((char)readChar()).ToString()));
                }
                else if ("<>!=".Contains((((char)peekChar()).ToString())))
                {
                    result.Add(new Token("COMPARISON", ((char)readChar()).ToString()));
                }
                else if ((char)peekChar() == '#')
                {
                    result.Add(new Token("STORE", ((char)readChar()).ToString()));
                }
                else
                {
                    result.Add(new Token("EXCEPTION", "Unexpected " + ((char)peekChar()).ToString() + " encountered"));
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
            string result = "";

            while (peekChar() != '\"' && peekChar() != -1)
                result += ((char)readChar()).ToString();

            readChar();

            return new Token("STRING", result);
        }

        private Token scanData()
        {
            string result = "";
            while (char.IsLetterOrDigit((char)peekChar()) && peekChar() != -1)
                result += ((char)readChar()).ToString();
            if (StaticData.Functions.ContainsKey(result))
                return new Token("FUNCTION", result);
            if (Regex.IsMatch(result, @"^\d+$"))
                return new Token("NUMBER", result);
            if (StaticLexerData.Vars.Contains(result))
                return new Token("VARIABLE", result);
            if (this.result[this.result.Count - 1].Operator == "STORE")
            {
                StaticLexerData.Vars.Add(result);
                return new Token("VARIABLE", result);
            }
            return new Token("EXCEPTION", "Unrecognized type " + result);
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

        private int readChar()
        {
            if (position < code.Length)
                return code[position++];
            else
                return -1;
        }
    }
}

