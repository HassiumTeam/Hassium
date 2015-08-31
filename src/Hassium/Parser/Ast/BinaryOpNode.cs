namespace Hassium.Parser.Ast
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
        LogicalAnd,
        LogicalOr,
        Xor,
        BitshiftLeft,
        BitshiftRight,
        Modulus,
        BitwiseAnd,
        BitwiseOr,
        Pow,
        Root,
        NullCoalescing,
        CombinedComparison,
        Dot
    }

    public class BinOpNode : AstNode
    {
        public BinaryOperation BinOp { get; set; }
        public BinaryOperation AssignOperation { get; set; }
        public bool IsOpAssign { get; set; }
        public AstNode Left
        {
            get 
            {
                return Children [0];
            }
        }

        public AstNode Right
        {
            get
            {
                return Children [1];
            }
        }

        public BinOpNode(BinaryOperation type, AstNode left, AstNode right)
        {
            BinOp = type;
            AssignOperation = type;
            Children.Add(left);
            Children.Add(right);
        }

        public BinOpNode(BinaryOperation type, BinaryOperation assign, AstNode left, AstNode right)
        {
            BinOp = type;
            AssignOperation = assign;
            IsOpAssign = true;
            Children.Add(left);
            Children.Add(right);
        }
    }
}

