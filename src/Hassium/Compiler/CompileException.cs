using System;

namespace Hassium.Compiler
{
    public class CompileException : Exception
    {
        public SourceLocation SourceLocation { get; private set; }
        public new string Message { get; private set; }

        public CompileException(SourceLocation location, string messageFormat, params object[] args)
        {
            SourceLocation = location;
            Message = string.Format(messageFormat, args);
        }
    }
}

