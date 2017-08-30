using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class CharNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public char Char { get; private set; }

        public CharNode(SourceLocation location, string c)
        {
            SourceLocation = location;

            Char = Convert.ToChar(c);
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
