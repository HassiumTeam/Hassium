using System;
using System.Collections.Generic;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class CaseNode: AstNode
    {
        public AstNode Body { get; private set; }
        public CaseNode(List<AstNode> predicates, AstNode body, SourceLocation location)
        {
            foreach (AstNode node in predicates)
                Children.Add(node);
            Body = body;
            this.SourceLocation = location;
        }

        public static CaseNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "case");
            List<AstNode> predicates = new List<AstNode>();
            while (!parser.MatchToken(TokenType.LeftBrace))
            {
                parser.AcceptToken(TokenType.Comma);
                predicates.Add(ExpressionNode.Parse(parser));
            }
            AstNode body = StatementNode.Parse(parser);

            return new CaseNode(predicates, body, parser.Location);
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

