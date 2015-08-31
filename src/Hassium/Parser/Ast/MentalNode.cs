namespace Hassium.Parser.Ast
{
    public class MentalNode: AstNode
    {
        public string OpType { get; private set; }

        public string Name { get; private set; }

        public bool IsBefore { get; private set; }

        public MentalNode(string type, string name, bool before)
        {
            this.OpType = type;
            this.Name = name;
            this.IsBefore = before;
        }
    }
}

