using System;

namespace Hassium.Compiler.Exceptions
{
    public class CompilerException : Exception
    {
        public SourceLocation SourceLocation { get; private set; }
        public new string Message { get { return string.Format("Compiler error at [{0}]! Message: {1}", SourceLocation, messageString); } }

        private string messageString;

        public CompilerException(SourceLocation location, string msgf, params object[] args)
        {
            SourceLocation = location;

            messageString = args.Length == 0 ? msgf : string.Format(msgf, args);
        }
    }
}
