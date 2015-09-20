using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class IncDecNode : AstNode
    {
        public string OpType { get; private set; }

        public string Name { get; private set; }

        public bool IsBefore { get; private set; }

        public IncDecNode(int position, string type, string name, bool before) : base(position)
        {
            OpType = type;
            Name = name;
            IsBefore = before;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}