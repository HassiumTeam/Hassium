namespace Hassium.Parser.Ast
{
    public class MemberAccess : AstNode
    {
        public AstNode Left
        {
            get
            {
                return this.Children[0];
            }
        }

        public string Member
        {
            private set;
            get;
        }

        public MemberAccess(int position, AstNode left, string identifier) : base(position)
        {
            this.Children.Add(left);
            this.Member = identifier;
        }
    }
}

