using System;
using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class EnumNode : AstNode
    {
        public override SourceLocation SourceLocation { get; } 

        public Dictionary<int, string> Attributes { get; private set; }
        public string Name { get; private set; }

        public EnumNode(SourceLocation location, string name)
        {
            SourceLocation = location;

            Attributes = new Dictionary<int, string>();
            Name = name;
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
