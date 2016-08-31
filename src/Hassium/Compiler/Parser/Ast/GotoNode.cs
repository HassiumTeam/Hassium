using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hassium.Compiler.Parser.Ast
{
    public class GotoNode: AstNode
    {
        public AstNode Expression { get { return Children[0]; } }

        public GotoNode(SourceLocation location, AstNode expression)
        {
            this.SourceLocation = location;
            Children.Add(expression);
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
