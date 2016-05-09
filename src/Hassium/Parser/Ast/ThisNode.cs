using System;

namespace Hassium.Parser
{
    public class ThisNode: AstNode
    {
        public ThisNode(SourceLocation location)
        {
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

