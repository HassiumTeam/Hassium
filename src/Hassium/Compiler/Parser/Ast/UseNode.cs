namespace Hassium.Compiler.Parser.Ast
{
    public class UseNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public string Module { get; private set; }

        public UseNode(SourceLocation location, string module)
        {
            SourceLocation = location;

            Module = module;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }

        public override void VisitChildren(IVisitor visitor)
        {

        }
    }
}
