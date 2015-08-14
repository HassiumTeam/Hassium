using System;

namespace Hassium
{
    public class NumberNode: AstNode
    {
        public int Value { private set; get; }

        public NumberNode (int value)
        {
            this.Value = value;
        }
    }
}

