using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class UseNode: AstNode
    {
        public string Path { get; private set; }
        public string Name { get; private set; }
        public bool Global { get; private set; }
        public bool IsModule { get; private set; }
        public bool IsLibrary { get; private set; }

        public UseNode(int position, string path, string name, bool global, bool module, bool library = false) : base(position)
        {
            Path = path;
            Name = name;
            Global = global;
            IsModule = module;
            IsLibrary = library;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

