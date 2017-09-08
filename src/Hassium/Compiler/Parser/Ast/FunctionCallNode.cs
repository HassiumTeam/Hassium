using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class FunctionCallNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public Dictionary<string, AstNode> InitialAttributes { get; private set; }
        public AstNode Target { get; private set; }
        public ArgumentListNode Parameters { get; private set; }

        public FunctionCallNode(SourceLocation location, AstNode target, ArgumentListNode parameters, Dictionary<string, AstNode> initialAttributes)
        {
            SourceLocation = location;

            InitialAttributes = initialAttributes;
            Target = target;
            Parameters = parameters;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Target.Visit(visitor);
            Parameters.Visit(visitor);
        }
    }
}
