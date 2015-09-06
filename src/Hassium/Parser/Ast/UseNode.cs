using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class UseNode: AstNode
    {
        public string Module { get; private set; }

        public UseNode(int position, string module) : base(position)
        {
            Module = module;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

