namespace Hassium.Compiler.Parser.Ast
{
    public class StringNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public string String { get; private set; }

        public StringNode(SourceLocation location, string value)
        {
            SourceLocation = location;

            String = value;
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
