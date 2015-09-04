namespace Hassium.Parser.Ast
{
    public class MentalNode: AstNode
    {
        public string OpType { get; private set; }

        public string Name { get; private set; }

        public bool IsBefore { get; private set; }

        public MentalNode(int position, string type, string name, bool before) : base(position)
        {
            OpType = type;
            Name = name;
            IsBefore = before;
        }
    }
}

