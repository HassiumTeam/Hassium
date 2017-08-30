using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class PropertyNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public string Name { get; private set; }

        public AstNode Get_ { get; private set; }
        public AstNode Set_ { get; private set; }

        public PropertyNode(SourceLocation location, string name, AstNode get_, AstNode set_ = null)
        {
            SourceLocation = location;

            Name = name;

            Get_ = get_;
            Set_ = set_;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }

        public override void VisitChildren(IVisitor visitor)
        {
            Get_.Visit(visitor);
            Set_.Visit(visitor);
        }
    }
}