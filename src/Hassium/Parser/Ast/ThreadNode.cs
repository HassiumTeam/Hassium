using Hassium.Interpreter;
using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class ThreadNode: AstNode
    {
        public AstNode Node
        {
            get
            {
                return Children[0];
            }
        }

        public ThreadNode(int position, AstNode node) : base(position)
        {
            Children.Add(node);
        }

        public static ThreadNode Parse(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "thread");
            AstNode node = Parser.ParseStatement(parser);

            return new ThreadNode(pos, node);
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
    }
}