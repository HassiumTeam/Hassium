using System;

namespace Hassium
{
    public class InternalException : Exception
    {
        public new string Message { get; private set; }
        public InternalException(string message) : base (message)
        {
            Message = message;
        }
    }
}

