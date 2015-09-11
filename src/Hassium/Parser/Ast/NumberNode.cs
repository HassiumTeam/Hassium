using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class NumberNode: AstNode
    {
        public double Value { private set; get; }
        public bool IsInt { get; private set; }

        public NumberNode (int position, double value, bool isint) : base(position)
        {
            Value = value;
            IsInt = isint;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

