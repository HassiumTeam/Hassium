namespace Hassium.Compiler.Lexer
{
    public class Token
    {
        public SourceLocation SourceLocation { get; private set; }

        public TokenType TokenType { get; private set; }
        public string Value { get; private set; }

        public Token(SourceLocation location, TokenType tokenType, string value)
        {
            SourceLocation = location;

            TokenType = tokenType;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1}", TokenType, Value);
        }
    }
}
