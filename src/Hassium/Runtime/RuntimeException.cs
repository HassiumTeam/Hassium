using System;

namespace Hassium.Runtime
{
    public class RuntimeException : Exception
    {
        public SourceLocation SourceLocation { get; private set; }
        public RuntimeException(string message, SourceLocation location) : base (message)
        {
            this.SourceLocation = location;
        }
    }
}

