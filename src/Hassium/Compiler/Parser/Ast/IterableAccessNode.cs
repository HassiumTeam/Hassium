namespace Hassium.Compiler.Parser.Ast
{
    public class IterableAccessNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public AstNode Target { get; private set; }
        public AstNode Index { get; private set; }

        public IterableAccessNode(SourceLocation location, AstNode target, AstNode index)
        {
            SourceLocation = location;

            Target = target;
            Index = index;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Target.Visit(visitor);
            Index.Visit(visitor);
        }
    }
}
