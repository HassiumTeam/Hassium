// Copyright (c) 2015, HassiumTeam (JacobMisirian, zdimension) All rights reserved.
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//  * Redistributions of source code must retain the above copyright notice, this list
//    of conditions and the following disclaimer.
// 
//  * Redistributions in binary form must reproduce the above copyright notice, this
//    list of conditions and the following disclaimer in the documentation and/or
//    other materials provided with the distribution.
// Neither the name of the copyright holder nor the names of its contributors may be
// used to endorse or promote products derived from this software without specific
// prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
// EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
// OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
// SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
// CONTRACT ,STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH
// DAMAGE.

using System;
using System.Linq;
using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    [Serializable]
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
        Dot,
        Is,
        In,
        Range,
        Swap
    }

    [Serializable]
    public class BinOpNode : AstNode
    {
        public BinaryOperation BinOp { get; set; }
        public BinaryOperation AssignOperation { get; set; }
        public bool IsOpAssign { get; set; }

        public AstNode Left
        {
            get { return Children[0]; }
        }

        public AstNode Right
        {
            get { return Children[1]; }
        }

        public BinOpNode(int position, BinaryOperation type, AstNode left, AstNode right) : base(position)
        {
            BinOp = type;
            AssignOperation = type;
            Children.Add(left);
            Children.Add(right);
        }

        public BinOpNode(int position, BinaryOperation type, BinaryOperation assign, AstNode left, AstNode right)
            : base(position)
        {
            BinOp = type;
            AssignOperation = assign;
            IsOpAssign = true;
            Children.Add(left);
            Children.Add(right);
        }

        public override string ToString()
        {
            return "[BinOp { " + Left + " } " + BinOp + " { " + Right + "} ]";
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}