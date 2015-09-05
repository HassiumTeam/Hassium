using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class InstanceNode : AstNode
    {
        public AstNode Target
        {
            get { return Children[0]; }
        }

        public InstanceNode(int position, AstNode value) : base(position)
        {
            Children.Add(value);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}
