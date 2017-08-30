using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class UnaryOperationNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public AstNode Target { get; private set; }

        public UnaryOperation UnaryOperation { get; private set; }


        public UnaryOperationNode(SourceLocation location, AstNode target, UnaryOperation operation)
        {
            SourceLocation = location;

            Target = target;

            UnaryOperation = operation;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Target.Visit(visitor);
        }
    }

    public enum UnaryOperation
    {
        BitwiseNot,
        LogicalNot,
        Negate,
        PostDecrement,
        PostIncrement,
        PreDecrement,
        PreIncrement
    }
}