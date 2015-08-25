using System;

namespace Hassium
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

        public ThreadNode(AstNode node)
        {
            Children.Add(node);
        }

        public static ThreadNode Parse(Parser.Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "thread");
            AstNode node = StatementNode.Parse(parser);

            return new ThreadNode(node);
        }
    }
}