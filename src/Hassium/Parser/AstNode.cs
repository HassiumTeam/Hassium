using System;
using System.Collections.Generic;

namespace Hassium.Parser
{
    public abstract class AstNode
    {
        public List<AstNode> Children = new List<AstNode>();
        public SourceLocation SourceLocation;
        public abstract void Visit(IVisitor visitor);
        public abstract void VisitChildren(IVisitor visitor);
    }
}

