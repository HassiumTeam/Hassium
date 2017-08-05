namespace Hassium.Compiler.Parser.Ast
{
    public class ExpressionStatementNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public AstNode Expression { get; private set; }

        public ExpressionStatementNode(SourceLocation location, AstNode expression)
        {
            SourceLocation = location;

            Expression = expression;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Expression.Visit(visitor);
        }
    }
}
