using System;

namespace Hassium
{
    public class ThreadNode: AstNode
    {
        public AstNode Node
        {
            get
            {
                return this.Children[0];
            }
        }

        public ThreadNode(AstNode node)
        {
            this.Children.Add(node);
        }

        public static ThreadNode Parse(Parser.Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "thread");
            AstNode node = StatementNode.Parse(parser);

            return new ThreadNode(node);
        }
    }
}