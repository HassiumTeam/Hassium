using System;
using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class FunctionCallNode: AstNode
    {
        public AstNode Target { get { return Children[0]; } }
        public ArgumentListNode Parameters { get { return (ArgumentListNode)Children[1]; } }
        public List<BinaryOperationNode> InitialAttributes { get; private set; }

        public FunctionCallNode(SourceLocation location, AstNode target, ArgumentListNode parameters, List<BinaryOperationNode> initialAttributes)
        {
            this.SourceLocation = location;
            Children.Add(target);
            Children.Add(parameters);
            InitialAttributes = initialAttributes;
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

