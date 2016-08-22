using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class UnaryOperationNode: AstNode
    {
        public UnaryOperation UnaryOperation { get; private set; }
        public AstNode Target { get { return Children[0]; } }

        public UnaryOperationNode(SourceLocation location, AstNode target, UnaryOperation unaryOperation)
        {
            this.SourceLocation = location;
            Children.Add(target);
            UnaryOperation = unaryOperation;
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

    public enum UnaryOperation
    {
        Dereference,
        LogicalNot,
        BitwiseNot,
        PostDecrement,
        PostIncrement,
        PreDecrement,
        PreIncrement,
        Reference,
        Negate
    }
}

