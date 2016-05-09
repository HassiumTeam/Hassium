using System;

namespace Hassium.Parser
{
    public class FunctionCallNode: AstNode
    {
        public AstNode Target { get { return Children[0]; } }
        public AstNode Arguments { get { return Children[1]; } }
        public bool IsConstructorCall { get; set; }

        public FunctionCallNode(AstNode target, ArgListNode arguments, SourceLocation location)
        {
            Children.Add(target);
            Children.Add(arguments);
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

