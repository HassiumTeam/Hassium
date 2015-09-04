using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class ReturnNode: AstNode
    {
        public AstNode Value
        {
            get {
                return Children.Count == 0 ? null : Children[0];
            }
        }

        public ReturnNode(int position, AstNode value) : base(position)
        {
            if(value != null) Children.Add(value);
        }

        public static AstNode Parse(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "return");
            if (parser.AcceptToken(TokenType.EndOfLine))
            {
                parser.ExpectToken(TokenType.EndOfLine);
                return new ReturnNode(pos, null);
            }
            else return new ReturnNode(pos, StatementNode.Parse(parser));
        }
    }
}

