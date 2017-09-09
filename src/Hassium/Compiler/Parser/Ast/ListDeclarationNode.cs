using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class ListDeclarationNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public List<AstNode> Elements { get; private set; }

        public ListDeclarationNode(SourceLocation location, List<AstNode> elements)
        {
            SourceLocation = location;

            Elements = elements;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (var element in Elements)
                element.Visit(visitor);
        }
    }
}
