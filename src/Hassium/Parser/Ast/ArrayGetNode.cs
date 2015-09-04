using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class ArrayGetNode: AstNode
    {
        public AstNode Target
        {
            get
            {
                return Children[0];
            }
        }

        public AstNode Arguments
        {
            get
            {
                return Children[1];
            }
        }

        public ArrayGetNode(int position, AstNode target, AstNode arguments) : base(position)
        {
            Children.Add(target);
            Children.Add(arguments);
        }
    }

    public class ArrayIndexerNode : AstNode
    {
        private ArrayIndexerNode(int position) : base(position)
        {
        }

        public static ArrayIndexerNode Parse(Parser parser)
        {
            var ret = new ArrayIndexerNode(parser.codePos);
            parser.ExpectToken(TokenType.Bracket, "[");

            while (!parser.MatchToken(TokenType.Bracket, "]"))
            {
                ret.Children.Add(ExpressionNode.Parse(parser));
            }
            parser.ExpectToken(TokenType.Bracket, "]");

            return ret;
        }
    }
}

