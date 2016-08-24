﻿using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class IdentifierNode: AstNode
    {
        public string Identifier { get; private set; }
        public IdentifierNode(SourceLocation location, string identifier)
        {
            this.SourceLocation = location;
            Identifier = identifier;
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
