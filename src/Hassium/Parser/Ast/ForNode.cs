using System;

namespace Hassium
{
    public class ForNode: AstNode
    {
        public AstNode Left
        {
            get
            {
                return this.Children[0];
            }
        }

        public AstNode Predicate
        {
            get
            {
                return this.Children[1];
            }
        }

        public AstNode Right
        {
            get
            {
                return this.Children[2];
            }
        }

        public AstNode Body
        {
            get
            {
                return this.Children[3];
            }
        }

        public ForNode(AstNode left, AstNode predicate, AstNode right, AstNode body)
        {
            this.Children.Add(left);
            this.Children.Add(predicate);
            this.Children.Add(right);
            this.Children.Add(body);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "for");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode left = StatementNode.Parse(parser);
            AstNode predicate = StatementNode.Parse(parser);
            AstNode right = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode forBody = StatementNode.Parse(parser);

            return new ForNode(left, predicate, right, forBody);
        }
    }
}

