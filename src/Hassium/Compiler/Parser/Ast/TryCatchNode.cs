using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class TryCatchNode: AstNode
    {
        public AstNode TryBody { get { return Children[0]; } }
        public AstNode CatchBody { get { return Children[1]; } }

        public TryCatchNode(SourceLocation location, AstNode tryBody, AstNode catchBody)
        {
            this.SourceLocation = location;
            Children.Add(tryBody);
            Children.Add(catchBody);
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

