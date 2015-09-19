using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class UncheckedNode : AstNode
    {
        public AstNode Node
        {
            get { return Children[0]; }
        }

        public UncheckedNode(int position, AstNode node) : base(position)
        {
            Children.Add(node);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}