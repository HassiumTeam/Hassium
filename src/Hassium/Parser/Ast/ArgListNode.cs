using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ArgListNode : AstNode
    {
        public ArgListNode(int position) : base(position)
        {
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}