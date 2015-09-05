using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ArrayIndexerNode : AstNode
    {
        public ArrayIndexerNode(int position) : base(position)
        {
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}