using System;

namespace Hassium.Parser
{
    public class ExpressionStatementNode: AstNode
    {
        public AstNode Body { get { return Children[0]; } }
        public ExpressionStatementNode(AstNode body, SourceLocation location)
        {
            Children.Add(body);
            this.SourceLocation = location;
        }

        public static AstNode Parse(Parser parser)
        {
            AstNode expression = ExpressionNode.Parse(parser);
            if (expression is FunctionCallNode)
                return new ExpressionStatementNode(expression, parser.Location);
            else if (expression is BinaryOperationNode)
            {
                if (((BinaryOperationNode)expression).BinaryOperation == BinaryOperation.Assignment)
                    return new ExpressionStatementNode(expression, parser.Location);
            }
            else if (expression is UnaryOperationNode)
            {
                if (((UnaryOperationNode)expression).UnaryOperation != UnaryOperation.Not)
                    return new ExpressionStatementNode(expression, parser.Location);
            }
            return expression;
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

