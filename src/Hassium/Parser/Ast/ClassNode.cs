using System;
using System.Collections.Generic;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class ClassNode: AstNode
    {
        public string Name { get; private set; }
        public AstNode Body { get { return Children[0]; } }
        public List<string> Inherits { get; private set; }
        public ClassNode(string name, AstNode body, List<string> inherits, SourceLocation location)
        {
            Name = name;
            Children.Add(body);
            Inherits = inherits;
            this.SourceLocation = location;
        }

        public static ClassNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "class");
            string name = parser.ExpectToken(TokenType.Identifier).Value;
            List<string> inherits = new List<string>();
            if (parser.AcceptToken(TokenType.Colon))
            {
                inherits.Add(parser.ExpectToken(TokenType.Identifier).Value);
                while (parser.AcceptToken(TokenType.Comma))
                    inherits.Add(parser.ExpectToken(TokenType.Identifier).Value);
            }
            AstNode body = StatementNode.Parse(parser);

            return new ClassNode(name, body, inherits, parser.Location);
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

