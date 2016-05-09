using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class ReturnNode: AstNode
    {
        public AstNode Expression { get { return Children[0]; } }
        public ReturnNode(AstNode expression, SourceLocation location)
        {
            Children.Add(expression);
            this.SourceLocation = location;
        }

        public static ReturnNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "return");
            AstNode expression = ExpressionNode.Parse(parser);

            return new ReturnNode(expression, parser.Location);
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

