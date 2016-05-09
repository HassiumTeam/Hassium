using System;
using System.Collections.Generic;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class SwitchNode: AstNode
    {
        public AstNode Predicate { get; private set; }
        public SwitchNode(AstNode predicate, List<CaseNode> cases, SourceLocation location)
        {
            Predicate = predicate;
            foreach (CaseNode node in cases)
                Children.Add(node);
            this.SourceLocation = location;
        }

        public static SwitchNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "switch");
            parser.ExpectToken(TokenType.LeftParentheses);
            AstNode predicate = ExpressionNode.Parse(parser);
            parser.ExpectToken(TokenType.RightParentheses);
            List<CaseNode> cases = new List<CaseNode>();
            parser.ExpectToken(TokenType.LeftBrace);
            while (parser.MatchToken(TokenType.Identifier, "case"))
                cases.Add(CaseNode.Parse(parser));
            parser.ExpectToken(TokenType.RightBrace);

            return new SwitchNode(predicate, cases, parser.Location);
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

