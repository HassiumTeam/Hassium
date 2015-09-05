using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class TryNode: AstNode
    {
        public AstNode Body
        {
            get
            {
                return Children[0];
            }
        }

        public AstNode CatchBody
        {
            get
            {
                return Children[1];
            }
        }

        public AstNode FinallyBody
        {
            get
            {
                return Children.Count < 3 ? null : Children[2];
            }
        }

        public TryNode(int position, AstNode body, AstNode catchBody) : this(position, body, catchBody, null)
        {
        }

        public TryNode(int position, AstNode body, AstNode catchBody, AstNode finallyBody) : base(position)
        {
            Children.Add(body);
            Children.Add(catchBody);
            if(finallyBody != null) Children.Add(finallyBody);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}

