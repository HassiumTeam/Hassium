using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class UseNode: AstNode
    {
        public AstNode Target { get { return Children[0]; } }
        public UseNode(AstNode target, SourceLocation location)
        {
            Children.Add(target);
        }

        public static UseNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "use");
            AstNode target = ExpressionNode.Parse(parser);

            return new UseNode(target, parser.Location);
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

