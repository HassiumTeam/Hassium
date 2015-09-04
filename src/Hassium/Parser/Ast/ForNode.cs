using Hassium.Interpreter;
using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class ForNode: AstNode
    {
        public AstNode Left
        {
            get
            {
                return Children[0];
            }
        }

        public AstNode Predicate
        {
            get
            {
                return Children[1];
            }
        }

        public AstNode Right
        {
            get
            {
                return Children[2];
            }
        }

        public AstNode Body
        {
            get
            {
                return Children[3];
            }
        }

        public ForNode(int position, AstNode left, AstNode predicate, AstNode right, AstNode body) : base(position)
        {
            Children.Add(left);
            Children.Add(predicate);
            Children.Add(right);
            Children.Add(body);
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}

