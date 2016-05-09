using System;
using System.Collections.Generic;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class Parser
    {
        public int Position { get; set; }
        public List<Token> Tokens { get; private set; }
        public SourceLocation Location { get { return Position < Tokens.Count ? Tokens[Position].SourceLocation : Tokens[Position - 1].SourceLocation; } }

        public AstNode Parse(List<Token> tokens)
        {
            Position = 0;
            Tokens = tokens;
            CodeBlockNode ast = new CodeBlockNode(Location);
            while (Position < Tokens.Count)
                ast.Children.Add(StatementNode.Parse(this));
            return ast;
        }

        public bool MatchToken(TokenType tokenType)
        {
            return Position < Tokens.Count && Tokens[Position].TokenType == tokenType;
        }
        public bool MatchToken(TokenType tokenType, string value)
        {
            return Position < Tokens.Count && Tokens[Position].TokenType == tokenType && Tokens[Position].Value == value;
        }

        public bool AcceptToken(TokenType tokenType)
        {
            bool matches = MatchToken(tokenType);
            if (matches)
                Position++;
            return matches;
        }
        public bool AcceptToken(TokenType tokenType, string value)
        {
            bool matches = MatchToken(tokenType, value);
            if (matches)
                Position++;
            return matches;
        }

        public Token ExpectToken(TokenType tokenType)
        {
            bool matches = MatchToken(tokenType);
            if (!matches)
                throw new ParserException(tokenType + " was expected in parser! Instead got " + GetToken().TokenType + " with value " + GetToken().Value, Location);
            return Tokens[Position++];
        }
        public Token ExpectToken(TokenType tokenType, string value)
        {
            bool matches = MatchToken(tokenType);
            if (!matches)
                throw new ParserException(tokenType + " of value " + value + " was expected in parser!", Location);
            return Tokens[Position++];
        }

        public Token GetToken(int n = 0)
        {
            if (Position < Tokens.Count)
                return Tokens[Position + n];
            else
                return Tokens[Position + n - 1];
        }
    }
}