using System;

namespace Hassium
{
    public class GetMemberNode : AstNode
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

        public GetMemberNode(AstNode left, string identifier)
        {
            this.Children.Add(left);
            this.Member = identifier;
        }
    }
}

