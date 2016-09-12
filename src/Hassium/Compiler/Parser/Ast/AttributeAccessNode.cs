using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class AttributeAccessNode: AstNode
    {
        public AstNode Left { get { return Children[0]; } }
        public string Right { get; private set; }

        public AttributeAccessNode(SourceLocation location, AstNode left, string right)
        {
            this.SourceLocation = location;
            Children.Add(left);
            Right = right;
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

