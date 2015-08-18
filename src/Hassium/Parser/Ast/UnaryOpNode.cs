using System;

namespace Hassium
{
    public enum UnaryOperation
    {
        Not
    }

    //Class for the urinary operations
    public class UnaryOpNode: AstNode
    {
        public UnaryOperation UnOp { get; set; }

        public AstNode Value
        {
            get
            {
                return this.Children[0];
            }
        }

        public UnaryOpNode(UnaryOperation type, AstNode value)
        {
            this.UnOp = type;
            this.Children.Add(value);
        }
    }
}

