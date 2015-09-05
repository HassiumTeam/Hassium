using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class CodeBlock: AstNode
    {
        public CodeBlock(int codePos) : base(codePos)
        {
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

