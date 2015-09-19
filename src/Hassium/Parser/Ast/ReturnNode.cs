using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ReturnNode : AstNode
    {
        public AstNode Value
        {
            get { return Children.Count == 0 ? null : Children[0]; }
        }

        public ReturnNode(int position, AstNode value) : base(position)
        {
            if (value != null) Children.Add(value);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}