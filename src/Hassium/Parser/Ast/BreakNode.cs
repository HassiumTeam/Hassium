using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class BreakNode : AstNode
    {
        public BreakNode(int codePos) : base(codePos)
        {
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}
