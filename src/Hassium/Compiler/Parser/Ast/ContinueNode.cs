using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class ContinueNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public ContinueNode(SourceLocation location)
        {
            SourceLocation = location;
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
