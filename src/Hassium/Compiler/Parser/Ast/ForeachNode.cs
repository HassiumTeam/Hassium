namespace Hassium.Compiler.Parser.Ast
{
    public class ForeachNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public string Variable { get; private set; }

        public AstNode Expression { get; private set; }
        public AstNode Body { get; private set; }

        public ForeachNode(SourceLocation location, string variable, AstNode expression, AstNode body)
        {
            SourceLocation = location;

            Variable = variable;

            Expression = expression;
            Body = body;
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
