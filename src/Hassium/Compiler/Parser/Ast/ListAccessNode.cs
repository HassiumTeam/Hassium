using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class ListAccessNode: AstNode
    {
        public AstNode Target { get { return Children[0]; } }
        public AstNode Element { get { return Children[1]; } }

        public ListAccessNode(SourceLocation location, AstNode target, AstNode element)
        {
            this.SourceLocation = location;
            Children.Add(target);
            Children.Add(element);
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

