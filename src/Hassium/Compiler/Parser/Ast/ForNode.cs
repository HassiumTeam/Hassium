using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class ForNode: AstNode
    {
        public AstNode StartStatement { get { return Children[0]; } }
        public AstNode Predicate { get { return Children[1]; } }
        public AstNode RepeatStatement { get { return Children[2]; } }
        public AstNode Body { get { return Children[3]; } }

        public ForNode(SourceLocation location, AstNode startStatement, AstNode predicate, AstNode repeatStatement, AstNode body)
        {
            this.SourceLocation = location;
            Children.Add(startStatement);
            Children.Add(predicate);
            Children.Add(repeatStatement);
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

