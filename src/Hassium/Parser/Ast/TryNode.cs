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

        public AstNode FinallyBody
        {
            get
            {
                return this.Children[2];
            }
        }

        public TryNode(AstNode body, AstNode catchBody)
        {
            this.Children.Add(body);
            this.Children.Add(catchBody);
        }

        public TryNode(AstNode body, AstNode catchBody, AstNode finallyBody)
        {
            this.Children.Add(body);
            this.Children.Add(catchBody);
            Children.Add(finallyBody);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "try");
            AstNode tryBody = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.Identifier, "catch");
            AstNode catchBody = StatementNode.Parse(parser);

            if (parser.AcceptToken(TokenType.Identifier, "finally"))
            {
                AstNode finallyBody = StatementNode.Parse(parser);
                return new TryNode(tryBody, catchBody, finallyBody);
            }

            return new TryNode(tryBody, catchBody);
        }
    }
}

