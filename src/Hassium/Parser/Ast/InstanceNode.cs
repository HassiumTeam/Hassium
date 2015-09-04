using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class InstanceNode : AstNode
    {
        public AstNode Target
        {
            get { return Children[0]; }
        }

        public InstanceNode(int position, AstNode value) : base(position)
        {
            Children.Add(value);
        }

        public static AstNode Parse(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "new");
            var target = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.EndOfLine);
            return new InstanceNode(pos, target);
        }
    }
}
