using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class TernaryOperationNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public AstNode Condition { get; private set; }
        public AstNode TrueExpression { get; private set; }
        public AstNode FalseExpression { get; private set; }

        public TernaryOperationNode(SourceLocation location, AstNode condition, AstNode trueExpression, AstNode falseExpression)
        {
            SourceLocation = location;

            Condition = condition;
            TrueExpression = trueExpression;
            FalseExpression = falseExpression;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Condition.Visit(visitor);
            TrueExpression.Visit(visitor);
            FalseExpression.Visit(visitor);
        }
    }
}
