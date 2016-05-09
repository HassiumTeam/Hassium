using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class ForeachNode: AstNode
    {
        public string Identifier { get; private set; }
        public AstNode Expression { get { return Children[0]; } }
        public AstNode Body { get { return Children[1]; } }
        public ForeachNode(string identifier, AstNode expression, AstNode body, SourceLocation location)
        {
            Identifier = identifier;
            Children.Add(expression);
            Children.Add(body);
            this.SourceLocation = location;
        }

        public static ForeachNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "foreach");
            parser.ExpectToken(TokenType.LeftParentheses);
            string identifier = parser.ExpectToken(TokenType.Identifier).Value;
            parser.ExpectToken(TokenType.Identifier, "in");
            AstNode expression = ExpressionNode.Parse(parser);
            parser.ExpectToken(TokenType.RightParentheses);
            AstNode body = StatementNode.Parse(parser);

            return new ForeachNode(identifier, expression, body, parser.Location);
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

