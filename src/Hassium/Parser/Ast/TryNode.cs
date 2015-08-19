using System;

namespace Hassium
{
    public class TryNode: AstNode
    {
        public AstNode Body
        {
            get
            {
                return this.Children[0];
            }
        }

        public AstNode CatchBody
        {
            get
            {
                return this.Children[1];
            }
        }

        public TryNode(AstNode body, AstNode catchBody)
        {
            this.Children.Add(body);
            this.Children.Add(catchBody);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "try");
            AstNode tryBody = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.Identifier, "catch");
            AstNode catchBody = StatementNode.Parse(parser);

            return new TryNode(tryBody, catchBody);
        }
    }
}

