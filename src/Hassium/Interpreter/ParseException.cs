using System;
using Hassium.Parser;

namespace Hassium.Interpreter
{
    public class ParseException : Exception
    {
        public AstNode Node { get; private set; }
        public int Position { get; private set; }


        public ParseException(string message, AstNode node) : this(message, node.Position)
        {
            Node = node;
        }

        public ParseException(string message, int position) : base(message)
        {
            Position = position;
        }
    }
}
