using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class TraitNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public string Name { get; private set; }

        public Dictionary<string, AstNode> Attributes { get; private set; }

        public TraitNode(SourceLocation location, string name)
        {
            SourceLocation = location;

            Name = name;

            Attributes = new Dictionary<string, AstNode>();
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            
        }
    }
}
