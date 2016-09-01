using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class CharNode: AstNode
    {
        public char Char { get; private set; }
        public CharNode(SourceLocation location, char c)
        {
            this.SourceLocation = location;
            Char = c;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode child in Children)
                child.Visit(visitor);
        }
    }
}

