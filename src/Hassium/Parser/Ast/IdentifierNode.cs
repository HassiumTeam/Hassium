using System;

namespace Hassium.Parser
{
    public class IdentifierNode: AstNode
    {
        public string Identifier { get; private set; }
        public IdentifierNode(string identifier, SourceLocation location)
        {
            Identifier = identifier;
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

