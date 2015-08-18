using System;

namespace Hassium
{
    public enum BinaryOperation
    {
        Assignment,
        Addition,
        Subtraction,
        Division,
        Multiplication,
        Equals,
        LessThan,
        GreaterThan,
        NotEqualTo,
        LesserOrEqual,
        GreaterOrEqual,
        And,
        Or,
        Xor,
        BitshiftLeft,
        BitshiftRight,
        Modulus
    }

    public class BinOpNode : AstNode
    {
        public BinaryOperation BinOp { get; set; }
        public AstNode Left
        {
            get 
            {
                return this.Children [0];
            }
        }

        public AstNode Right
        {
            get
            {
                return this.Children [1];
            }
        }

        public BinOpNode(BinaryOperation type, AstNode left, AstNode right)
        {
            this.BinOp = type;
            this.Children.Add(left);
            this.Children.Add(right);
        }
    }
}

