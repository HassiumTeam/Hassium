using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class EnumNode: AstNode
    {
        public string Name { get; private set; }
        public bool IsPrivate { get; set; }

        public EnumNode(SourceLocation location, string name)
        {
            this.SourceLocation = location;
            Name = name;
            IsPrivate = false;
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

