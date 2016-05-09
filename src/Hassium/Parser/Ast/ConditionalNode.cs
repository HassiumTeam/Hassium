using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class ConditionalNode: AstNode
    {
        public AstNode Predicate { get { return Children[0]; } }
        public AstNode Body { get { return Children[1]; } } 
        public AstNode ElseBody { get { return Children[2]; } }
        public ConditionalNode(AstNode predicate, AstNode body, SourceLocation location, AstNode elseBody = null)
        {
            Children.Add(predicate);
            Children.Add(body);
            if (elseBody != null)
                Children.Add(elseBody);
            this.SourceLocation = location;
        }

        public static ConditionalNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "if");
            parser.ExpectToken(TokenType.LeftParentheses);
            AstNode predicate = ExpressionNode.Parse(parser);
            parser.ExpectToken(TokenType.RightParentheses);
            AstNode body = StatementNode.Parse(parser);
            AstNode elseBody = null;
            if (parser.AcceptToken(TokenType.Identifier, "else"))
                elseBody = StatementNode.Parse(parser);

            return new ConditionalNode(predicate, body, parser.Location, elseBody);
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

