using System;

namespace Hassium
{
    public enum TokenType
    {
        Brace,
        Bracket,
        Identifier,
        String,
        Number,
        Parentheses,
        Comma,
        Operation,
        OpAssign,
        Comparison,
        Assignment,
        Exception,
        EndOfLine,
        UnaryOperation,
        MentalOperation

    }
    public class Token
    {
        public TokenType TokenClass { get; private set; }
        public object Value { get; private set; }

        public Token(TokenType type, object value)
        {
            TokenClass = type;
            Value = value;
        }
    }
}

