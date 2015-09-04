using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class FunctionCallNode: AstNode
    {
        public AstNode Target
        {
            get
            {
                return Children[0];
            }
        }
        public AstNode Arguments
        {
            get
            {
                return Children[1];
            }
        }

        public FunctionCallNode(int position, AstNode target, AstNode arguments) : base(position)
        {
            Children.Add(target);
            Children.Add(arguments);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

