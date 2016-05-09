using System;
using System.Collections.Generic;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class EnumNode: AstNode
    {
        public string Name { get; private set; }
        public EnumNode(string name, List<IdentifierNode> members, SourceLocation location)
        {
            Name = name;
            foreach (IdentifierNode child in members)
                Children.Add(child);
            this.SourceLocation = location;
        }

        public static EnumNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "enum");
            string name = parser.ExpectToken(TokenType.Identifier).Value;
            parser.ExpectToken(TokenType.LeftBrace);
            List<IdentifierNode> members = new List<IdentifierNode>();
            while (parser.MatchToken(TokenType.Identifier))
            {
                members.Add(new IdentifierNode(parser.ExpectToken(TokenType.Identifier).Value, parser.Location));
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }
            parser.ExpectToken(TokenType.RightBrace);

            return new EnumNode(name, members, parser.Location);
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode child in Children)
                child.Visit(visitor);
        }
    }
}

