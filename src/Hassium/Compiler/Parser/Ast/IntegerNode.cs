using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class IntegerNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public long Integer { get; private set; }

        public IntegerNode(SourceLocation location, string i)
        {
            SourceLocation = location;

            Integer = Convert.ToInt64(i);
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
