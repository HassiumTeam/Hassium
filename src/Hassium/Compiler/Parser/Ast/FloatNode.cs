using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class FloatNode: AstNode
    {
        public double Number { get; private set; }
        public FloatNode(SourceLocation location, double number)
        {
            this.SourceLocation = location;
            Number = number;
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

