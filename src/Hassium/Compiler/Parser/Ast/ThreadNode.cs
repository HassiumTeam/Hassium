namespace Hassium.Compiler.Parser.Ast
{
    public class ThreadNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public AstNode Body { get; private set; }
        public bool DoImmediately { get; private set; }

        public ThreadNode(SourceLocation location, AstNode body, bool doImmedaitely = false)
        {
            SourceLocation = location;

            Body = body;
            DoImmediately = doImmedaitely;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }

        public override void VisitChildren(IVisitor visitor)
        {
            Body.Visit(visitor);
        }
    }
}
