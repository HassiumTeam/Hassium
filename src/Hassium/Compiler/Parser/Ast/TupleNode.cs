using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class TupleNode: AstNode
    {
        public TupleNode(SourceLocation location)
        {
            this.SourceLocation = location;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode node in Children)
                node.Visit(visitor);
        }
    }
}

