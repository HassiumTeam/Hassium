using System.Collections.Generic;
using Hassium.Lexer;
using Hassium.Parser.Ast;

namespace Hassium.Parser
{
    /// <summary>
    /// Parser.
    /// </summary>
    public class Parser
    {
        private List<Token> tokens;
        public int position;
        public int codePos { get { return CurrentToken().Position; } }

        public bool EndOfStream
        {
            get
            {
                return tokens.Count <= position;
            }
        }

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
        }
        /// <summary>
        /// Parse this instance.
        /// </summary>
        public AstNode Parse()
        {
            CodeBlock block = new CodeBlock(0);
            while (!EndOfStream)
            {
                block.Children.Add(StatementNode.Parse(this));
            }
            return block;
        }

        public Token CurrentToken()
        {
            if(position >= tokens.Count) return new Token(TokenType.Identifier, "");
            return tokens[position];
        }

        public Token PreviousToken(int delay = 1)
        {
            return tokens[position - delay];
        }

        public bool MatchToken(TokenType clazz)
        {
            return position < tokens.Count && tokens[position].TokenClass == clazz;
        }

        public bool MatchToken(TokenType clazz, string value)
        {
            return position < tokens.Count && tokens[position].TokenClass == clazz && tokens[position].Value.ToString() == value;
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
            return MatchToken(clazz) ? tokens[position++] : new Token(TokenType.Exception, "Tokens did not match");
        }

        public Token ExpectToken(TokenType clazz, string value)
        {
            return MatchToken(clazz, value) ? tokens[position++] : new Token(TokenType.Exception, "Tokens did not match");
        }
    }
}

