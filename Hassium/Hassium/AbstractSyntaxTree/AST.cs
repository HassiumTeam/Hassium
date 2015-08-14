using System;
using System.Collections.Generic;

namespace Hassium
{
    public class AST
    {
        private List<Token> tokens = new List<Token>();
        private List<Token> result = new List<Token>();

        public AST(List<Token> tokens)
        {
            AstNode myAst = new BinOpNode(BinaryOperation.Addition, new NumberNode(2), new BinOpNode(BinaryOperation.Subtraction, new NumberNode(1), new NumberNode(2)));
            Console.WriteLine(EvaluateNode(myAst).ToString());
        }

        public List<Token> Generate()
        {
            return new List<Token>();
        }

        private object EvaluateNode (AstNode node)
        {
            if (node is NumberNode)
            {
                return ((NumberNode)node).Value;
            }
            else if (node is BinOpNode)
            {
                return InterpretBinaryOp((BinOpNode)node);
            }

            return 0;
        }

        public int InterpretBinaryOp (BinOpNode node)
        {
            switch (node.BinOp) 
            {
                    case BinaryOperation.Addition:
                    return (int)(EvaluateNode (node.Left)) + (int)(EvaluateNode (node.Right));
                    case BinaryOperation.Subtraction:
                    return (int)(EvaluateNode (node.Left)) - (int)(EvaluateNode (node.Right));
                    case BinaryOperation.Division:
                    return (int)(EvaluateNode (node.Left)) / (int)(EvaluateNode (node.Right));
                    case BinaryOperation.Multiplication:
                    return (int)(EvaluateNode (node.Left)) * (int)(EvaluateNode (node.Right));
            }   
            // Raise error
            return -1;
        }

    }
}

