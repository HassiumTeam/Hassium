using System;
using System.Collections.Generic;

namespace Hassium
{
    public class Parser: AstNode
    {
        private List<Token> tokens = new List<Token>();
        private int position = 0;

        public bool EndOfStream
        {
            get
            {
                return this.tokens.Count <= position;
            }
        }

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }

        public AstNode Parse()
        {
            CodeBlock block = new CodeBlock();
            while (!EndOfStream)
            {
                block.Children.Add(StatementNode.Parse(this));
            }
            return block;
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



    }
}

