using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class ForNode: AstNode
    {
        public AstNode SingleStatement { get { return Children[0]; } }
        public AstNode Predicate { get { return Children[1]; } }
        public AstNode RepeatStatement { get { return Children[2]; } }
        public AstNode Body { get { return Children[3]; } }
        public ForNode(AstNode singleStatement, AstNode predicate, AstNode repeatStatement, AstNode body, SourceLocation location)
        {
            Children.Add(singleStatement);
            Children.Add(predicate);
            Children.Add(repeatStatement);
            Children.Add(body);
            this.SourceLocation = location;
        }

        public static ForNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "for");
            parser.ExpectToken(TokenType.LeftParentheses);
            AstNode singleStatement = ExpressionStatementNode.Parse(parser);
            parser.AcceptToken(TokenType.Semicolon);
            AstNode predicate = ExpressionNode.Parse(parser);
            parser.AcceptToken(TokenType.Semicolon);
            AstNode repeatStatement = ExpressionStatementNode.Parse(parser);
            parser.ExpectToken(TokenType.RightParentheses);
            AstNode body = StatementNode.Parse(parser);

            return new ForNode(singleStatement, predicate, repeatStatement, body, parser.Location);
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

