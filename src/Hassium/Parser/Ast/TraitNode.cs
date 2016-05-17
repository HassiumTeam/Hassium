using System;
using System.Collections.Generic;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class TraitNode: AstNode
    {
        public class Trait
        {
            public string Type { get; private set; }
            public string Name { get; private set; }
            public Trait(string type, string name)
            {
                Type = type;
                Name = name;
            }
        }

        public string Name { get; private set; }
        public List<Trait> Traits { get; private set; }
        public TraitNode(List<Trait> traits, string name, SourceLocation location)
        {
            Traits = traits;
            Name = name;
            this.SourceLocation = location;
        }

        public static TraitNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "trait");
            string name = parser.ExpectToken(TokenType.Identifier).Value;
            parser.ExpectToken(TokenType.LeftBrace);
            List<Trait> traits = new List<Trait>();
            while (!parser.AcceptToken(TokenType.RightBrace))
            {
                traits.Add(new Trait(parser.ExpectToken(TokenType.Identifier).Value, parser.ExpectToken(TokenType.Identifier).Value));
                parser.AcceptToken(TokenType.Semicolon);
            }

            return new TraitNode(traits, name, parser.Location);
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

