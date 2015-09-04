using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ArrayGetNode: AstNode
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

        public ArrayGetNode(int position, AstNode target, AstNode arguments) : base(position)
        {
            Children.Add(target);
            Children.Add(arguments);
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}

