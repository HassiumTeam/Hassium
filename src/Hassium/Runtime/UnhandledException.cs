using Hassium.Compiler;

using System;

namespace Hassium.Runtime
{
    public class UnhandledException : Exception
    {
        public string CallStack { get; private set; }
        public new string Message { get; private set; }
        public SourceLocation SourceLocation { get; private set; }

        public UnhandledException(SourceLocation location, string callstack, string message)
        {
            CallStack = callstack;
            Message = message;
            SourceLocation = location;
        }
    }
}
