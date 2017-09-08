namespace Hassium.Compiler.Parser
{
    public abstract class AstNode
    {
        public bool IsPrivate = false;

        public abstract SourceLocation SourceLocation { get; set; }

        public abstract void Visit(IVisitor visitor);
        public abstract void VisitChildren(IVisitor visitor);
    }
}
