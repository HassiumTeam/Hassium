﻿using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class DictionaryDeclarationNode: AstNode
    {
        public DictionaryDeclarationNode(SourceLocation location)
        {
            this.SourceLocation = location;
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
