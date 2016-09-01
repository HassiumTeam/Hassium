using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class UsingNode: AstNode
    {
        public AstNode Body { get { return Children[0]; } }
        public AstNode Expression { get { return Children[1]; } }

        public UsingNode(SourceLocation location, AstNode body, AstNode expression)
        {
            this.SourceLocation = location;
            Children.Add(body);
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
