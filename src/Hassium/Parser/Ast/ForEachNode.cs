using System;

namespace Hassium
{
    public class ForEachNode : AstNode
    {
        public AstNode Needle
        {
            get
            {
                return this.Children[0];
            }
        }

        public AstNode Haystack
        {
            get
            {
                return this.Children[1];
            }
        }

        public AstNode Body
        {
            get
            {
                return this.Children[2];
            }
        }

        public ForEachNode(AstNode needle, AstNode haystack, AstNode body)
        {
            this.Children.Add(needle);
            this.Children.Add(haystack);
            this.Children.Add(body);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "foreach");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode needle = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.Identifier, "in");
            AstNode haystack = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode forBody = StatementNode.Parse(parser);

            return new ForEachNode(needle, haystack, forBody);
        }
    }
}

