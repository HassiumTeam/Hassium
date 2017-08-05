namespace Hassium.Compiler.Parser.Ast
{
    public class TryCatchNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public AstNode TryBody { get; private set; }
        public AstNode CatchBody { get; private set; }

        public TryCatchNode(SourceLocation location, AstNode tryBody, AstNode catchBody)
        {
            SourceLocation = location;

            TryBody = tryBody;
            CatchBody = catchBody;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            TryBody.Visit(visitor);
            CatchBody.Visit(visitor);
        }
    }
}
