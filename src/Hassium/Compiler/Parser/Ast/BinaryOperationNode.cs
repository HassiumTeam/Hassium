using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class BinaryOperationNode: AstNode
    {
        public BinaryOperation BinaryOperation { get; private set; }
        public AstNode Left { get { return Children[0]; } }
        public AstNode Right { get { return Children[1]; } }

        public BinaryOperationNode(SourceLocation location, BinaryOperation operation, AstNode left, AstNode right)
        {
            this.SourceLocation = location;
            BinaryOperation = operation;
            Children.Add(left);
            Children.Add(right);
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
        Addition,
        Assignment,
        BitshiftLeft,
        BitshiftRight,
        BitwiseAnd,
        BitwiseOr,
        Division,
        EqualTo,
        GreaterThan,
        GreaterThanOrEqual,
        IntegerDivision,
        Is,
        LogicalAnd,
        LogicalOr,
        LesserThan,
        LesserThanOrEqual,
        Modulus,
        Multiplication,
        NotEqualTo,
        Power,
        Subraction,
    }
}

