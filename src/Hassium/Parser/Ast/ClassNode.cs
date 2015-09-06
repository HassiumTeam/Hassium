using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ClassNode: AstNode
    {
        public string Name { get; private set; }

        public AstNode Body
        {
            get
            {
                return Children[0];
            }
        }

        public string Extends { get; private set; }

        public ClassNode(int position, string name, AstNode body, string extends = "") : base(position)
        {
            Children.Add(body);
            Name = name;
            Extends = extends;
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

