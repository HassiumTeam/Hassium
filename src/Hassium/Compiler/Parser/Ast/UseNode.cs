using System;
using System.Collections.Generic;
using System.Text;

namespace Hassium.Compiler.Parser.Ast
{
    public class UseNode: AstNode
    {
        public List<string> Parts { get; private set; }

        public UseNode(SourceLocation location, List<string> parts)
        {
            this.SourceLocation = location;
            Parts = parts;
        }

        public string GetName()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Parts.Count - 1; i++)
                sb.AppendFormat("{0}/", Parts[i]);
            sb.Append(Parts[Parts.Count - 1]);
            return sb.ToString();
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

