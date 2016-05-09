using System;

namespace Hassium.Parser
{
    public class DoubleNode: AstNode
    {
        public double Number { get; private set; }
        public DoubleNode(double number, SourceLocation location)
        {
            Number = number;
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

