using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class ArgumentListNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public List<AstNode> Arguments { get; private set; }

        public ArgumentListNode(SourceLocation location, List<AstNode> arguments)
        {
            SourceLocation = location;

            Arguments = arguments;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (var arg in Arguments)
                arg.Visit(visitor);
        }
    }
}
