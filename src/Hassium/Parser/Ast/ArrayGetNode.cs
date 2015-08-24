namespace Hassium
{
    public class ArrayGetNode: AstNode
    {
        public AstNode Target
        {
            get
            {
                return this.Children[0];
            }
        }

        public AstNode Arguments
        {
            get
            {
                return this.Children[1];
            }
        }

        public ArrayGetNode(AstNode target, AstNode arguments)
        {
            Children.Add(target);
            Children.Add(arguments);
        }
    }

    public class ArrayIndexerNode : AstNode
    {
        public static ArrayIndexerNode Parse(Parser.Parser parser)
        {
            var ret = new ArrayIndexerNode();
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

