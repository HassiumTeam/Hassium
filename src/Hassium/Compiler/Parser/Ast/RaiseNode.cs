namespace Hassium.Compiler.Parser.Ast
{
    public class RaiseNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public AstNode Exception { get; private set; }

        public RaiseNode(SourceLocation location, AstNode exception)
        {
            SourceLocation = location;

            Exception = exception;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Exception.Visit(visitor);
        }
    }
}
