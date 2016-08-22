using System;
using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class TraitNode: AstNode
    {
        public string Name { get; private set; }
        public List<Trait> Traits { get; private set; }

        public TraitNode(SourceLocation location, string name, List<Trait> traits)
        {
            this.SourceLocation = location;
            Name = name;
            Traits = traits;
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

    public class Trait
    {
        public string Name { get; private set; }
        public string Type { get; private set; }

        public Trait(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}

