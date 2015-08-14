using System;
using System.Collections.Generic;

namespace Hassium
{
    public class Parser
    {
        private List<Token> tokens = new List<Token>();
        private int position = 0;

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public void Parse()
        {

        }

        public bool MatchToken(string clazz)
        {
            return tokens[position].Operator == clazz;
        }

        public bool MatchToken(string clazz, string value)
        {
            return tokens[position].Operator == clazz && tokens[position].Value == value;
        }

        public bool AcceptToken(string clazz)
        {
            if (MatchToken(clazz))
            {
                position++;
                return true;
            }

            return false;
        }

        public bool AcceptToken(string clazz, string value)
        {
            if (MatchToken(clazz, value))
            {
                position++;
                return true;
            }

            return false;
        }

        public Token ExpectToken(string clazz)
        {
            if (!MatchToken(clazz))
            {
                return new Token("EXCEPTION", "Tokens did not match");
            }

            return tokens[position++];
        }

        public Token ExpectToken(string clazz, string value)
        {
            if (!MatchToken(clazz, value))
            {
                return new Token("EXCEPTION", "Tokens did not match");
            }

            return tokens[position++];
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

