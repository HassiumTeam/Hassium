using System;
using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class ArgumentListNode: AstNode
    {
        public ArgumentListNode(SourceLocation location, List<AstNode> args)
        {
            this.SourceLocation = location;
            foreach (AstNode ast in args)
                Children.Add(ast);
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

