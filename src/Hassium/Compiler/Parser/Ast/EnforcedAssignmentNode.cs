namespace Hassium.Compiler.Parser.Ast
{
    public class EnforcedAssignmentNode : AstNode
    {
        public override SourceLocation SourceLocation { get; }

        public string Variable { get; private set; }

        public AstNode Type { get; private set; }
        public AstNode Value { get; private set; }

        public EnforcedAssignmentNode(SourceLocation location, AstNode type, string variable, AstNode value)
        {
            SourceLocation = location;

            Variable = variable;

            Type = type;
            Value = value;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            Type.Visit(visitor);
            Value.Visit(visitor);
        }
    }
}
