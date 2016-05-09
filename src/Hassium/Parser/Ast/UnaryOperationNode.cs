using System;

namespace Hassium.Parser
{
    public class UnaryOperationNode: AstNode
    {
        public UnaryOperation UnaryOperation { get; private set; }
        public AstNode Body { get { return Children[0]; } }
        public UnaryOperationNode(UnaryOperation unaryOperation, AstNode body, SourceLocation location)
        {
            UnaryOperation = unaryOperation;
            Children.Add(body);
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

    public enum UnaryOperation
    {
        Not,
        PostDecrement,
        PostIncrement,
        PreDecrement,
        PreIncrement
    }
}

