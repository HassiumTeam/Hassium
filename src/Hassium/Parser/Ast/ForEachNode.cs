using Hassium.Lexer;

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

        public static AstNode Parse(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "foreach");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode needle = null;
            needle = parser.CurrentToken().Value.ToString() == "[" ? ArrayInitializerNode.Parse(parser) : IdentifierNode.Parse(parser);
            parser.ExpectToken(TokenType.Identifier, "in");
            AstNode haystack = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode forBody = StatementNode.Parse(parser);

            return new ForEachNode(pos, needle, haystack, forBody);
        }
    }
}

