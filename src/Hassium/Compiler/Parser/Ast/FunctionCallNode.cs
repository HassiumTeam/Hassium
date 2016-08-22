using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class FunctionCallNode: AstNode
    {
        public AstNode Target { get { return Children[0]; } }
        public ArgumentListNode Parameters { get { return (ArgumentListNode)Children[1]; } }

        public FunctionCallNode(SourceLocation location, AstNode target, ArgumentListNode parameters)
        {
            this.SourceLocation = location;
            Children.Add(target);
            Children.Add(parameters);
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

