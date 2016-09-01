using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class StringNode: AstNode
    {
        public string String { get; private set; }

        public StringNode(SourceLocation location, string val)
        {
            this.SourceLocation = location;
            String = val;
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

