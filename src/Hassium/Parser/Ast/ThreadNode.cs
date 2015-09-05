using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ThreadNode: AstNode
    {
        public AstNode Node
        {
            get
            {
                return Children[0];
            }
        }

        public ThreadNode(int position, AstNode node) : base(position)
        {
            Children.Add(node);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}