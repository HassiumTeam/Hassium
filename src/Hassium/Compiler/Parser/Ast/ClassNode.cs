using System;
using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class ClassNode: AstNode
    {
        public string Name { get; private set; }
        public List<string> Inherits { get; private set; }
        public AstNode Body { get { return Children[0]; } }

        public ClassNode(SourceLocation location, string name, List<string> inherits, AstNode body)
        {
            this.SourceLocation = location;
            Name = name;
            Inherits = inherits;
            Children.Add(body);
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

