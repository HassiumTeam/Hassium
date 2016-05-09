using System;

namespace Hassium.Parser
{
    public class CodeBlockNode: AstNode
    {
        public CodeBlockNode(SourceLocation location)
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

