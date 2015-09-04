using Hassium.Interpreter;
using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class StatementNode: AstNode
    {
        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}

