using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class TryNode: AstNode
    {
        public AstNode Body
        {
            get
            {
                return Children[0];
            }
        }

        public AstNode CatchBody
        {
            get
            {
                return Children[1];
            }
        }

        public AstNode FinallyBody
        {
            get
            {
                if (Children.Count < 3) return null;
                return Children[2];
            }
        }

        public TryNode(int position, AstNode body, AstNode catchBody) : this(position, body, catchBody, null)
        {
        }

        public TryNode(int position, AstNode body, AstNode catchBody, AstNode finallyBody) : base(position)
        {
            Children.Add(body);
            Children.Add(catchBody);
            if(finallyBody != null) Children.Add(finallyBody);
        }

        public static AstNode Parse(Hassium.Parser.Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "try");
            AstNode tryBody = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.Identifier, "catch");
            AstNode catchBody = StatementNode.Parse(parser);

            if (parser.AcceptToken(TokenType.Identifier, "finally"))
            {
                AstNode finallyBody = StatementNode.Parse(parser);
                return new TryNode(pos, tryBody, catchBody, finallyBody);
            }

            return new TryNode(pos, tryBody, catchBody);
        }
    }
}

