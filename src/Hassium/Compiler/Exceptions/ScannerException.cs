using System;

namespace Hassium.Compiler.Exceptions
{
    public class ScannerException : Exception
    {
        public SourceLocation SourceLocation { get; private set; }
        public new string Message { get { return string.Format("Scanner error at {0}! Message: {1}", SourceLocation, messageString); } }

        private string messageString;

        public ScannerException(SourceLocation location, string msgf, params object[] args)
        {
            SourceLocation = location;

            messageString = args.Length == 0 ? msgf : string.Format(msgf, args);
        }
    }
}
