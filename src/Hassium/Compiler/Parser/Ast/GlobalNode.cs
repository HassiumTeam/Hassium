using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class GlobalNode: AstNode
    {
        public string Variable { get; private set; }
        public AstNode Value { get { return Children[0]; } }

        public GlobalNode(SourceLocation location, string variable, AstNode value)
        {
            this.SourceLocation = location;
            Variable = variable;
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

