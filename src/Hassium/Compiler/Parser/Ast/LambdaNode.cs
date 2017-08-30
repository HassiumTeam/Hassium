using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class LambdaNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public ArgumentListNode Parameters { get; private set; }
        public AstNode Body { get; private set; }

        public LambdaNode(SourceLocation location, ArgumentListNode parameters, AstNode body)
        {
            SourceLocation = location;

            Parameters = parameters;
            Body = body;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Parameters.Visit(visitor);
            Body.Visit(visitor);
        }
    }
}
