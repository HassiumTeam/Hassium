using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ArrayGetNode : AstNode
    {
        public AstNode Target
        {
            get { return Children[0]; }
        }

        public AstNode Arguments
        {
            get { return Children[1]; }
        }

        public AstNode Count
        {
            get { return Children[2]; }
        }

        public ArrayGetNode(int position, AstNode target, AstNode arguments) : base(position)
        {
            Children.Add(target);
            Children.Add(arguments);
            if (arguments.Children.Count == 2) Children.Add(arguments.Children[1]);
            else Children.Add(new NumberNode(position, 1, true));
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}