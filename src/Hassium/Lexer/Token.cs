namespace Hassium.Lexer
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
        MentalOperation,
        Lambda,
        Dot,
        Extend
    }

    public class Token
    {
        public TokenType TokenClass { get; private set; }
        public object Value { get; private set; }
        public int Position { get; private set; }

        public Token(TokenType type, object value, int pos = -1)
        {
            TokenClass = type;
            Value = value;
            Position = pos;
        }
    }
}

