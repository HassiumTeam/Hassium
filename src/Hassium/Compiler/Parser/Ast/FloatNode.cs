using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class FloatNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public double Float { get; private set; }

        public FloatNode(SourceLocation location, string f)
        {
            SourceLocation = location;

            Float = Convert.ToDouble(f);
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
