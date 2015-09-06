using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ImportNode: AstNode
    {
        public string Path { get; private set; }
        public string Name { get; private set; }
        public bool Global { get; private set; }

        public ImportNode(int position, string path, string name, bool global) : base(position)
        {
            Path = path;
            Name = name;
            Global = global;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

