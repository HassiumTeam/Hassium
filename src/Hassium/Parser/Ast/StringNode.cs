using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class StringNode: AstNode
    {
        public string Value { get; private set; }

        public StringNode(int position, string value) : base(position)
        {
            Value = value;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}

