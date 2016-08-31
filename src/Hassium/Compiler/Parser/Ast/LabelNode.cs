using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hassium.Compiler.Parser.Ast
{
    public class LabelNode: AstNode
    {
        public string Identifier { get; private set; }

        public LabelNode(SourceLocation location, string identifier)
        {
            this.SourceLocation = location;
            Identifier = identifier;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode child in Children)
                child.Visit(visitor);
        }
    }
}
