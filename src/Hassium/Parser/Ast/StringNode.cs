using System;

namespace Hassium.Parser
{
    public class StringNode: AstNode
    {
        public string String { get; private set; }

        public StringNode(string value, SourceLocation location)
        {
            String = value;
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

