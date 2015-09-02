namespace Hassium.Parser.Ast
{
    public class NumberNode: AstNode
    {
        public double Value { private set; get; }

        public NumberNode (int position, double value) : base(position)
        {
            Value = value;
        }
    }
}

