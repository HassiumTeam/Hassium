using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ForEachNode : AstNode
    {
        public AstNode Needle
        {
            get
            {
                return Children[0];
            }
        }

        public AstNode Haystack
        {
            get
            {
                return Children[1];
            }
        }

        public AstNode Body
        {
            get
            {
                return Children[2];
            }
        }

        public ForEachNode(int position, AstNode needle, AstNode haystack, AstNode body) : base(position)
        {
            Children.Add(needle);
            Children.Add(haystack);
            Children.Add(body);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

