using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class SwitchNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public Dictionary<AstNode, AstNode> Cases { get; private set; }
        public AstNode Default { get; private set; }
        public AstNode Value { get; private set; }

        public SwitchNode(SourceLocation location, Dictionary<AstNode, AstNode> cases, AstNode _default, AstNode value)
        {
            SourceLocation = location;

            Cases = cases;
            Default = _default;
            Value = value;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }

        public override void VisitChildren(IVisitor visitor)
        {
            Value.Visit(visitor);
            foreach (var c in Cases)
            {
                c.Key.Visit(visitor);
                c.Value.Visit(visitor);
            }
        }
    }
}
