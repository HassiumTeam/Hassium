namespace Hassium.Parser.Ast
{
    public class NumberNode: AstNode
    {
        public double Value { private set; get; }

        public NumberNode (double value)
        {
            Value = value;
        }
    }
}

