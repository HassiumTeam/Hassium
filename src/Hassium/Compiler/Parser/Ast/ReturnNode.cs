using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Compiler.Parser.Ast
{
    public class ReturnNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public AstNode Value { get; private set; }

        public ReturnNode(SourceLocation location, AstNode value)
        {
            SourceLocation = location;

            Value = value;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Value.Visit(visitor);
        }
    }
}
