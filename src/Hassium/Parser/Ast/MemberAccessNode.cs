using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class MemberAccessNode : AstNode
    {
        public AstNode Left
        {
            get
            {
                return Children[0];
            }
        }

        public string Member
        {
            private set;
            get;
        }

        public MemberAccessNode(int position, AstNode left, string identifier) : base(position)
        {
            Children.Add(left);
            Member = identifier;
        }

        public override string ToString()
        {
            return Left + "." + Member;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

