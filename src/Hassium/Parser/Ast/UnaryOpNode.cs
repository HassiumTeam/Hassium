namespace Hassium.Parser.Ast
{
    public enum UnaryOperation
    {
        Not,
        Negate,
        Complement,
    }

    //Class for the urinary operations
    public class UnaryOpNode: AstNode
    {
        public UnaryOperation UnOp { get; set; }

        public AstNode Value
        {
            get
            {
                return Children[0];
            }
        }

        public UnaryOpNode(UnaryOperation type, AstNode value)
        {
            UnOp = type;
            Children.Add(value);
        }
    }
}

