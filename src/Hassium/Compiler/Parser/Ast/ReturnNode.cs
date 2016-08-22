using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class ReturnNode: AstNode
    {
        public AstNode Value { get { return Children[0]; } }

        public ReturnNode(SourceLocation location, AstNode value)
        {
            Children.Add(value);
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

