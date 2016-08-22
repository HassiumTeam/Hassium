using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class IfNode: AstNode
    {
        public AstNode Predicate { get { return Children[0]; } }
        public AstNode Body { get { return Children[1]; } }
        public AstNode ElseBody { get { return Children[2]; } }

        public IfNode(SourceLocation location, AstNode predicate, AstNode body)
        {
            this.SourceLocation = location;
            Children.Add(predicate);
            Children.Add(body);
        }
        public IfNode(SourceLocation location, AstNode predicate, AstNode body, AstNode elseBody)
        {
            this.SourceLocation = location;
            Children.Add(predicate);
            Children.Add(body);
            Children.Add(elseBody);
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

