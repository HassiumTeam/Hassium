using System;

namespace Hassium
{
    public class FunctionNode: AstNode
    {
        public IFunction Value { get; private set; }

        public FunctionNode(IFunction value)
        {
            this.Value = value;
        }
    }
}

