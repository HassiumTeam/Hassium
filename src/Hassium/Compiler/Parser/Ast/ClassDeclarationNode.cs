using Hassium.Runtime;

using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class ClassDeclarationNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public string Name { get; private set; }

        public AstNode Body { get; private set; }
        public List<AstNode> Inherits { get; private set; }

        public DocStrAttribute DocStr { get; set; }

        public ClassDeclarationNode(SourceLocation location, string name, AstNode body)
        {
            SourceLocation = location;

            Name = name;

            Body = body;
            Inherits = new List<AstNode>();
        }
        public ClassDeclarationNode(SourceLocation location, string name, AstNode body, List<AstNode> inherits)
        {
            SourceLocation = location;

            Name = name;

            Body = body;
            Inherits = inherits;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (var inherit in Inherits)
                inherit.Visit(visitor);
            Body.Visit(visitor);
        }
    }
}
