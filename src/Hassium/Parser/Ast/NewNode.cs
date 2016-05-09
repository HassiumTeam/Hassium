using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class NewNode: AstNode
    {
        public FunctionCallNode Call { get { return (FunctionCallNode)Children[0]; } }
        public NewNode(FunctionCallNode call, SourceLocation location)
        {
            Children.Add(call);
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

