﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Compiler.Parser.Ast
{
    public class WhileNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public AstNode Condition { get; private set; }
        public AstNode Body { get; private set; }

        public WhileNode(SourceLocation location, AstNode condition, AstNode body)
        {
            SourceLocation = location;

            Condition = condition;
            Body = body;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Condition.Visit(visitor);
            Body.Visit(visitor);
        }
    }
}
