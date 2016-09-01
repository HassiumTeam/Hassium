using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class ForeachNode: AstNode
    {
        public string Variable { get; private set; }
        public AstNode Target { get { return Children[0]; } }
        public AstNode Body { get { return Children[1]; } }

        public ForeachNode(SourceLocation location, string variable, AstNode target, AstNode body)
        {
            this.SourceLocation = location;
            Variable = variable;
            Children.Add(target);
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

