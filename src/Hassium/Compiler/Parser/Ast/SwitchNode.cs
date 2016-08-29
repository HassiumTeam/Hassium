using System;
using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class SwitchNode: AstNode
    {
        public AstNode Expression { get { return Children[0]; } }
        public List<Case> Cases { get; private set; }
        public AstNode DefaultCase { get { return Children[1]; } }

        public SwitchNode(SourceLocation location, AstNode expression, List<Case> cases, AstNode defaultCase)
        {
            this.SourceLocation = location;
            Children.Add(expression);
            Children.Add(defaultCase);
            Cases = cases;
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

    public class Case
    {
        public BinaryOperation Operation { get; private set; }
        public List<AstNode> Expressions { get; private set; }
        public AstNode Body { get; private set; }

        public Case(BinaryOperation operation, List<AstNode> expressions, AstNode body)
        {
            Operation = operation;
            Expressions = expressions;
            Body = body;
        }
    }
}

