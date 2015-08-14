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

        public AstNode Parse()
        {
            return ExpressionNode.Parse(this);
        }

        public bool MatchToken(TokenType clazz)
        {
            return position < tokens.Count && tokens [position].TokenClass == clazz;
        }

        public bool MatchToken(TokenType clazz, string value)
        {
            return position < tokens.Count && tokens[position].TokenClass == clazz && tokens[position].Value == value;
        }

        public bool AcceptToken(TokenType clazz)
        {
            if (MatchToken(clazz))
            {
                position++;
                return true;
            }

            return false;
        }

        public bool AcceptToken(TokenType clazz, string value)
        {
            if (MatchToken(clazz, value))
            {
                position++;
                return true;
            }

            return false;
        }

        public Token ExpectToken(TokenType clazz)
        {
            if (!MatchToken(clazz))
            {
                return new Token(TokenType.Exception, "Tokens did not match");
            }

            return tokens[position++];
        }

        public Token ExpectToken(TokenType clazz, string value)
        {
            if (!MatchToken(clazz, value))
            {
                return new Token(TokenType.Exception, "Tokens did not match");
            }

            return tokens[position++];
        }

        public object EvaluateNode (AstNode node)
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

        public double InterpretBinaryOp (BinOpNode node)
        {
            switch (node.BinOp) 
            {
                case BinaryOperation.Addition:
                    return (double)(EvaluateNode (node.Left)) + (double)(EvaluateNode (node.Right));
                    case BinaryOperation.Subtraction:
                    return (double)(EvaluateNode (node.Left)) - (double)(EvaluateNode (node.Right));
                    case BinaryOperation.Division:
                    return (double)(EvaluateNode (node.Left)) / (double)(EvaluateNode (node.Right));
                    case BinaryOperation.Multiplication:
                    return (double)(EvaluateNode (node.Left)) * (double)(EvaluateNode (node.Right));
            }   
            // Raise error
            return -1;
        }

    }
}

