using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class RaiseNode: AstNode
    {
        public AstNode Expression { get { return Children[0]; } }

        public RaiseNode(SourceLocation location, AstNode expression)
        {
            this.SourceLocation = location;
            Children.Add(expression);
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

