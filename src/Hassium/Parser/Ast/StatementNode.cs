using Hassium.Interpreter;
using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class StatementNode: AstNode
    {
        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

