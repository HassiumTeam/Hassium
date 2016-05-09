using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class RaiseNode: AstNode
    {
        public AstNode Expression { get { return Children[0]; } }
        public RaiseNode(AstNode expression, SourceLocation location)
        {
            Children.Add(expression);
            this.SourceLocation = location;
        }

        public static RaiseNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "raise");
            AstNode expression = ExpressionNode.Parse(parser);

            return new RaiseNode(expression, parser.Location);
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

