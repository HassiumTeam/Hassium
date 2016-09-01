using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class BreakNode: AstNode
    {
        public BreakNode(SourceLocation location)
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

