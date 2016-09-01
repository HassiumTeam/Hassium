using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class ExtendNode: AstNode
    {
        public string Name { get; private set; }

        public ExtendNode(SourceLocation location, string name)
        {
            this.SourceLocation = location;
            Name = name;
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

