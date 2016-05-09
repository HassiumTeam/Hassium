using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class TupleNode: AstNode
    {
        public TupleNode(SourceLocation location)
        {
            this.SourceLocation = location;
        }

        public static TupleNode Parse(Parser parser, AstNode initialExpression)
        {
            TupleNode tuple = new TupleNode(parser.Location);
            tuple.Children.Add(initialExpression);
            while (!parser.MatchToken(TokenType.RightParentheses))
            {
                tuple.Children.Add(ExpressionNode.Parse(parser));
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }
            parser.ExpectToken(TokenType.RightParentheses);
            return tuple;
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

