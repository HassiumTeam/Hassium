using System;

namespace Hassium.Parser
{
    public class CharNode: AstNode
    {
        public Char Char { get; private set; }
        public CharNode(string ch, SourceLocation location)
        {
            Char = Convert.ToChar(ch);
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

