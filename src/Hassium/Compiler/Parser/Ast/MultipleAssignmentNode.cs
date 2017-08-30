using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class MultipleAssignmentNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public List<AstNode> Targets { get; private set; }
        public AstNode Value { get; private set; }

        public MultipleAssignmentNode(SourceLocation location, List<AstNode> targets, AstNode value)
        {
            SourceLocation = location;

            Targets = targets;
            Value = value;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (var target in Targets)
                target.Visit(visitor);
        }
    }
}
