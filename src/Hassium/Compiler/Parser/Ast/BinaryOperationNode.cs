using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Compiler.Parser.Ast
{
    public class BinaryOperationNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public BinaryOperation BinaryOperation { get; private set; }

        public AstNode Left { get; private set; }
        public AstNode Right { get; private set; }

        public BinaryOperationNode(SourceLocation location, BinaryOperation operation, AstNode left, AstNode right)
        {
            SourceLocation = location;

            BinaryOperation = operation;

            Left = left;
            Right = right;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Left.Visit(visitor);
            Right.Visit(visitor);
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
        BitwiseXor,
        Division,
        EqualTo,
        GreaterThan,
        GreaterThanOrEqual,
        IntegerDivision,
        Is,
        LesserThan,
        LesserThanOrEqual,
        LogicalAnd,
        LogicalOr,
        Modulus,
        Multiplication,
        NotEqualTo,
        NullCoalescing,
        Power,
        Subtraction,
        Swap
    }
}
