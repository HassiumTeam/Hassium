using System;

namespace Hassium
{
    public class StringNode: AstNode
    {
        public string Value { get; private set; }

        public StringNode(string value)
        {
            this.Value = value;
        }
    }
}

