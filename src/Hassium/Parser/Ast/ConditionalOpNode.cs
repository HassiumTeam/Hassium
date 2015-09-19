using Hassium.Interpreter;

namespace Hassium.Parser.Ast
{
    public class ConditionalOpNode : AstNode
    {
        public AstNode Predicate
        {
            get { return Children[0]; }
        }

        public AstNode Body
        {
            get { return Children[1]; }
        }

        public AstNode ElseBody
        {
            get { return Children[2]; }
        }

        public ConditionalOpNode(int position, AstNode predicate, AstNode body)
            : this(position, predicate, body, new CodeBlock(position))
        {
        }

        public ConditionalOpNode(int position, AstNode predicate, AstNode body, AstNode elseBody) : base(position)
        {
            Children.Add(predicate);
            Children.Add(body);
            Children.Add(elseBody);
        }

        public override object Visit(IVisitor visitor)
        {
            return visitor.Accept(this);
        }
    }
}