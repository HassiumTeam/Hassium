using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class IntegerNode: AstNode
    {
        public long Number { get; private set; }
        public IntegerNode(SourceLocation location, long num)
        {
            Number = num;
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

