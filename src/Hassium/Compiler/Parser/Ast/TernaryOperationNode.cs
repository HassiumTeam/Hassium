using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class TernaryOperationNode: AstNode
    {
        public AstNode Predicate { get { return Children[0]; } }
        public AstNode TrueStatement { get { return Children[1]; } }
        public AstNode FalseStatement { get { return Children[2]; } }

        public TernaryOperationNode(SourceLocation location, AstNode predicate, AstNode trueStatement, AstNode falseStatement)
        {
            this.SourceLocation = location;
            Children.Add(predicate);
            Children.Add(trueStatement);
            Children.Add(falseStatement);
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

