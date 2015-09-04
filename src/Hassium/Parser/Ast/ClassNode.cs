using Hassium.Interpreter;
using Hassium.Lexer;

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

        public ClassNode(int position, string name, AstNode body) : base(position)
        {
            Children.Add(body);
            Name = name;
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}

