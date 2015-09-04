namespace Hassium.Parser.Ast
{
    public class MemberAccess : AstNode
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

        public MemberAccess(int position, AstNode left, string identifier) : base(position)
        {
            Children.Add(left);
            Member = identifier;
        }
    }
}

