using System;

namespace Hassium
{
    public class FunctionCallNode: AstNode
    {
        public AstNode Target
        {
            get
            {
                return this.Children[0];
            }
        }
        public AstNode Arguments
        {
            get
            {
                return this.Children[1];
            }
        }

        public FunctionCallNode(AstNode target, ArgListNode arguments)
        {
            this.Children.Add(target);
            this.Children.Add(arguments);
        }
    }
}

