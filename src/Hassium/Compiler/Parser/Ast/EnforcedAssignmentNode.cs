using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class EnforcedAssignmentNode: AstNode
    {
        public string Type { get; private set; }
        public string Variable { get; private set; }
        public AstNode Value { get { return Children[0]; } }

        public EnforcedAssignmentNode(SourceLocation location, string type, string variable, AstNode value)
        {
            this.SourceLocation = location;
            Type = type;
            Variable = variable;
            Children.Add(value);
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

