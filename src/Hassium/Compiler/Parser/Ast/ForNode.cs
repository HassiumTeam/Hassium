namespace Hassium.Compiler.Parser.Ast
{
    public class ForNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public AstNode InitialStatement { get; private set; }
        public AstNode Condition { get; private set; }
        public AstNode RepeatStatement { get; private set; }

        public AstNode Body { get; private set; }

        public ForNode(SourceLocation location, AstNode initialStatement, AstNode condition, AstNode repeatStatement, AstNode body)
        {
            SourceLocation = location;

            InitialStatement = initialStatement;
            Condition = condition;
            RepeatStatement = repeatStatement;

            Body = body;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            InitialStatement.Visit(visitor);
            Condition.Visit(visitor);
            RepeatStatement.Visit(visitor);

            Body.Visit(visitor);
        }
    }
}
