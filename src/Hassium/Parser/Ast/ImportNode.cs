using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ImportNode: AstNode
    {
        public string Path { get; private set; }

        public ImportNode(int position, string path) : base(position)
        {
            this.Path = path;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

