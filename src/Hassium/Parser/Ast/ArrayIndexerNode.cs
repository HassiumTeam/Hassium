using Hassium.Interpreter;
using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class ArrayIndexerNode : AstNode
    {
        public ArrayIndexerNode(int position) : base(position)
        {
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}