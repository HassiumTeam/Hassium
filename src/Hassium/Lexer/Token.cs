using System;

namespace Hassium
{
    public enum TokenType
    {
        Identifier,
        Bracket,
        String,
        Number,
        Parentheses,
        Comma,
        Operation,
        Comparison,
        Store,
        Exception,
        EndOfLine,
        Not,
        Xor,
        Bitshift

    }
    public class Token
    {
        public TokenType TokenClass { get; private set; }
        public string Value { get; private set; }

        public Token(TokenType type, string value)
        {
            this.TokenClass = type;
            this.Value = value;
        }
    }
}

