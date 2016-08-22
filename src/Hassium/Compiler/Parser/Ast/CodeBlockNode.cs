using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class CodeBlockNode: AstNode
    {
        public CodeBlockNode()
        {
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

