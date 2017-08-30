using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Compiler.Parser.Ast
{
    public class TupleNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public List<AstNode> Elements { get; private set; }

        public TupleNode(SourceLocation location)
        {
            SourceLocation = location;

            Elements = new List<AstNode>();
        }
        public TupleNode(SourceLocation location, List<AstNode> elements)
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
