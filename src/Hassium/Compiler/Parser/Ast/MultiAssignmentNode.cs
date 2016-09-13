using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hassium.Compiler.Parser.Ast
{
    public class MultiAssignmentNode: AstNode
    {
        public List<BinaryOperationNode> Assignments { get; private set; }

        public MultiAssignmentNode(List<AstNode> left, AstNode value)
        {
            Assignments = new List<BinaryOperationNode>();
            foreach (var ast in left)
                Assignments.Add(new BinaryOperationNode(value.SourceLocation, BinaryOperation.Assignment, ast, value));
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
