using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class LambdaNode: AstNode
    {
        public ArgumentListNode Parameters { get { return (ArgumentListNode)Children[0]; } }
        public AstNode Body { get { return Children[1]; } }

        public LambdaNode(SourceLocation location, ArgumentListNode parameters, AstNode body)
        {
            this.SourceLocation = location;
            Children.Add(parameters);
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

