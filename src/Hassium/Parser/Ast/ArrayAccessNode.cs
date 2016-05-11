using System;

namespace Hassium.Parser
{
    public class ArrayAccessNode: AstNode
    {
        public AstNode Target { get { return Children[0]; } }
        public AstNode Expression { get { return Children[1]; } }
        public ArrayAccessNode(AstNode target, AstNode expression, SourceLocation location)
        {
            Children.Add(target);
            Children.Add(expression);
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

