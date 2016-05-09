using System;

namespace Hassium.Parser
{
    public class KeyValuePairNode: AstNode
    {
        public AstNode Left { get { return Children[0]; } }
        public AstNode Right { get { return Children[1]; } }
        public KeyValuePairNode(AstNode left, AstNode right, SourceLocation location)
        {
            Children.Add(left);
            Children.Add(right);
            this.SourceLocation = location;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode child in Children)
                child.Visit(visitor);
        }
    }
}

