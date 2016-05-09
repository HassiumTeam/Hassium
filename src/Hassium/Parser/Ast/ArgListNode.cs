using System;
using System.Collections.Generic;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class ArgListNode: AstNode
    {
        public ArgListNode(SourceLocation location)
        {
            this.SourceLocation = location;
        }

        public static ArgListNode Parse(Parser parser)
        {
            ArgListNode ret = new ArgListNode(parser.Location);
            parser.ExpectToken(TokenType.LeftParentheses);

            while (!parser.MatchToken(TokenType.RightParentheses))
            {
                ret.Children.Add(ExpressionNode.Parse(parser));
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            parser.ExpectToken(TokenType.RightParentheses);

            return ret;
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

