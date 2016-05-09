using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class PropertyNode: AstNode
    {
        public string Identifier { get; private set; }
        public AstNode GetBody { get { return Children[0]; } }
        public AstNode SetBody { get { return Children[1]; } }
        public PropertyNode(string identifier, AstNode getBody, SourceLocation location, AstNode setBody = null)
        {
            Identifier = identifier;
            Children.Add(getBody);
            if (setBody != null)
                Children.Add(setBody);
            this.SourceLocation = location;
        }

        public static PropertyNode Parse(Parser parser)
        {
            string identifier = parser.ExpectToken(TokenType.Identifier).Value;
            parser.ExpectToken(TokenType.LeftBrace);
            parser.ExpectToken(TokenType.Identifier, "get");
            parser.ExpectToken(TokenType.LeftBrace);
            AstNode getBody = StatementNode.Parse(parser);
            parser.AcceptToken(TokenType.Semicolon);
            parser.ExpectToken(TokenType.RightBrace);
            if (!parser.AcceptToken(TokenType.Identifier, "set"))
                return new PropertyNode(identifier, getBody, parser.Location);
            parser.ExpectToken(TokenType.LeftBrace);
            AstNode setBody = StatementNode.Parse(parser);
            parser.AcceptToken(TokenType.Semicolon);
            parser.ExpectToken(TokenType.RightBrace);
            parser.ExpectToken(TokenType.RightBrace);
            return new PropertyNode(identifier, getBody, parser.Location, setBody);
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

