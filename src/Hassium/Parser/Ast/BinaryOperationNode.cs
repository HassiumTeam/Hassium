using System;

namespace Hassium.Parser
{
    public class BinaryOperationNode: AstNode
    {
        public BinaryOperation BinaryOperation { get; private set; }
        public AstNode Left { get { return Children[0]; } }
        public AstNode Right { get { return Children[1]; } }
        public BinaryOperationNode(BinaryOperation binaryOperation, AstNode left, AstNode right, SourceLocation location)
        {
            BinaryOperation = binaryOperation;
            Children.Add(left);
            Children.Add(right);
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

    public enum BinaryOperation
    {
        Assignment,
        Addition,
        BitShiftLeft,
        BitShiftRight,
        Subtraction,
        Multiplication,
        Division,
        IntegerDivision,
        LogicalOr,
        LogicalAnd,
        Modulus,
        NullCoalescing,
        EqualTo,
        NotEqualTo,
        GreaterThan,
        GreaterThanOrEqual,
        In,
        Is,
        LesserThan,
        LesserThanOrEqual,
        OR,
        Power,
        Range,
        Swap,
        XOR,
        XAnd
    }
}

