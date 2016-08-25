using System;

namespace Hassium.Compiler.Scanner
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

        public override string ToString()
        {
            return string.Format("[Token: TokenType={0}, Value={1}, SourceLocation={2}]", TokenType, Value, SourceLocation);
        }
    }
}

