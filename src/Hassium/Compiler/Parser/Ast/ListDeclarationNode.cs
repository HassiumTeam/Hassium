using System;
using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class ListDeclarationNode: AstNode
    {
        public List<AstNode> InitialValues { get; private set; }
        public ListDeclarationNode(SourceLocation location, List<AstNode> initial)
        {
            this.SourceLocation = location;
            InitialValues = initial;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode node in Children)
                node.Visit(visitor);
        }
    }
}

