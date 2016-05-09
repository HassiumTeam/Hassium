using System;

namespace Hassium.Parser
{
    public class Int64Node: AstNode
    {
        public Int64 Number { get; private set; }
        public Int64Node(Int64 number, SourceLocation location)
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

