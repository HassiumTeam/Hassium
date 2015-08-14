using System;

namespace Hassium
{
    public enum TokenType
    {
        String,
        Number,
        Variable,
        Parentheses,
        Comma,
        Operation,
        Comparison,
        Store,
        Exception,
        Function,
        EndOfLine

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

