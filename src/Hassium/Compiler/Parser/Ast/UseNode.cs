namespace Hassium.Compiler.Parser.Ast
{
    public class UseNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public string Class { get; private set; }
        public string Module { get; private set; }
        
        public UseNode(SourceLocation location, string clazz, string module)
        {
            SourceLocation = location;

            Class = clazz;
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
