using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class DoNode : AstNode
    {
        public AstNode Predicate
        {
            get { return Children[0]; }
        }

        public AstNode DoBody
        {
            get { return Children[1]; }
        }

        public DoNode(int position, AstNode predicate, AstNode doBody) : base(position)
        {
            Children.Add(predicate);
            Children.Add(doBody);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

