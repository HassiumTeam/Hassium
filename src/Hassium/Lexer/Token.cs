using System;

namespace Hassium.Lexer
{
    public class Token
    {
        public TokenType TokenType { get; private set; }
        public string Value { get; private set; }
        public SourceLocation SourceLocation { get; private set; }

        public Token(TokenType tokenType, string value, SourceLocation location)
        {
            TokenType = tokenType;
            Value = value;
            SourceLocation = location;
        }
    }

    public enum TokenType
    {
        Assignment,
        BinaryOperation,
        Char,
        Colon,
        Comma,
        Comparison,
        Dot,
        Double,
        Identifier,
        Int64,
        LeftBrace,
        LeftParentheses,
        LeftSquare,
        Question,
        RightBrace,
        RightParentheses,
        RightSquare,
        Semicolon,
        String,
        UnaryOperation
    }
}

