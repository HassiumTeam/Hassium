using System;

namespace Hassium.Compiler.Parser.Ast
{
    public class PropertyNode: AstNode
    {
        public string Variable { get; private set; }
        public AstNode GetBody { get { return Children[0]; } }
        public AstNode SetBody { get { return Children[1]; } }
        public bool IsPrivate { get; set; }

        public PropertyNode(SourceLocation location, string variable, AstNode getBody, AstNode setBody)
        {
            this.SourceLocation = location;
            Variable = variable;
            Children.Add(getBody);
            Children.Add(setBody);
            IsPrivate = false;
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

