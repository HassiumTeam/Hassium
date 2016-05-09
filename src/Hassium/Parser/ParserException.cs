using System;

namespace Hassium
{
    public class ParserException : Exception
    {
        public SourceLocation SourceLocation { get; private set; }
        public ParserException(string message, SourceLocation location) : base(message)
        {
            SourceLocation = location;
        }
    }
}

