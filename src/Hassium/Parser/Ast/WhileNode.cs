using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class WhileNode: AstNode
    {
        public AstNode Predicate { get { return Children[0]; } }
        public AstNode Body { get { return Children[1]; } }
        public WhileNode(AstNode predicate, AstNode body, SourceLocation location)
        {
            Children.Add(predicate);
            Children.Add(body);
            this.SourceLocation = location;
        }

        public static WhileNode Parse(Parser parser, bool until = false)
        {
            parser.ExpectToken(TokenType.Identifier);
            parser.ExpectToken(TokenType.LeftParentheses);
            AstNode predicate = until ? new UnaryOperationNode(UnaryOperation.Not, ExpressionNode.Parse(parser), parser.Location) : ExpressionNode.Parse(parser);
            parser.ExpectToken(TokenType.RightParentheses);
            AstNode body = StatementNode.Parse(parser);

            return new WhileNode(predicate, body, parser.Location);
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

