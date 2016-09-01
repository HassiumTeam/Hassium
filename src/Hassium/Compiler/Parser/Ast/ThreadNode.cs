using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class ThreadNode: AstNode
    {
        public AstNode Body { get { return Children[0]; } }

        public ThreadNode(SourceLocation location, AstNode body)
        {
            this.SourceLocation = location;
            Children.Add(body);
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

