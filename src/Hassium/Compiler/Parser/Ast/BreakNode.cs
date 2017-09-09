using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class BreakNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public BreakNode(SourceLocation location)
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
