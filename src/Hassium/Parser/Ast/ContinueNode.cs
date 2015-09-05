using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ContinueNode : AstNode
    {
        public ContinueNode(int codePos) : base(codePos)
        {
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}
