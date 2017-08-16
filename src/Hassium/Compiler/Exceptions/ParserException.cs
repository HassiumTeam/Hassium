using System;

namespace Hassium.Compiler.Exceptions
{
    public class ParserException : Exception
    {
        public SourceLocation SourceLocation { get; private set; }
        public new string Message { get { return string.Format("Parser error at [{0}]! Message: {1}", SourceLocation, messageString); } }

        private string messageString;

        public ParserException(SourceLocation location, string msgf, params object[] args)
        {
            SourceLocation = location;

            messageString = args.Length == 0 ? msgf : string.Format(msgf, args);
        }
    }
}
