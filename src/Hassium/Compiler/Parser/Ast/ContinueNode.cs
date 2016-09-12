using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class ContinueNode: AstNode
    {
        public ContinueNode(SourceLocation location)
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

