using System.Collections.Generic;

namespace Hassium.Compiler.Parser.Ast
{
    public class SwitchNode : AstNode
    {
        public override SourceLocation SourceLocation { get; set; }

        public List<Case> Cases { get; private set; }
        public AstNode Default { get; private set; }
        public AstNode Value { get; private set; }

        public SwitchNode(SourceLocation location, List<Case> cases, AstNode _default, AstNode value)
        {
            SourceLocation = location;

            Cases = cases;
            Default = _default;
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

    public class Case 
    {
        public BinaryOperation BinaryOperation { get; private set; }

        public AstNode C { get; private set; }
        public AstNode Stmt { get; private set; }

        public Case(BinaryOperation binop, AstNode c, AstNode stmt)
        {
            BinaryOperation = binop;
            C = c;
            Stmt = stmt;
        }
    }
}
