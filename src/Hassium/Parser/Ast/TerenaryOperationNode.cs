using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class TerenaryOperationNode: AstNode
    {
        public AstNode Predicate { get { return Children[0]; } }
        public AstNode TrueBody { get { return Children[1]; } }
        public AstNode ElseBody { get { return Children[2]; } }
        public TerenaryOperationNode(AstNode predicate, AstNode trueBody, AstNode elseBody, SourceLocation location)
        {
            Children.Add(predicate);
            Children.Add(trueBody);
            Children.Add(elseBody);
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

