using System;

namespace Hassium
{
    public class ReturnNode: AstNode
    {
        public AstNode Value
        {
            get { return this.Children[0]; }
        }

        public ReturnNode(AstNode value)
        {
            this.Children.Add(value);
        }

        public static AstNode Parse(Parser.Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "return");
            if (parser.AcceptToken(TokenType.EndOfLine))
            {
                parser.ExpectToken(TokenType.EndOfLine);
                return new ReturnNode(null);
            }
            else return new ReturnNode(StatementNode.Parse(parser));
        }
    }
}

