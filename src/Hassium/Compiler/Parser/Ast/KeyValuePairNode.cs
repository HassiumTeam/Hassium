using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class KeyValuePairNode: AstNode
    {
        public AstNode Key { get { return Children[0]; } }
        public AstNode Value { get { return Children[1]; } }

        public KeyValuePairNode(SourceLocation location, AstNode key, AstNode value)
        {
            this.SourceLocation = location;
            Children.Add(key);
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

