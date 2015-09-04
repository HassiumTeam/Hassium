using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class WhileNode: AstNode
    {
        public AstNode Predicate
        {
            get
            {
                return Children[0];
            }
        }
        public AstNode Body
        {
            get
            {
                return Children[1];
            }
        }
        public AstNode ElseBody
        {
            get
            {
                return Children[2];
            }
        }

        public WhileNode(int position, AstNode predicate, AstNode body) : this(position, predicate, body, new CodeBlock(position))
        {
        }

        public WhileNode(int position, AstNode predicate, AstNode body, AstNode elseBody) : base(position)
        {
            Children.Add(predicate);
            Children.Add(body);
            Children.Add(elseBody);
        }

        public static AstNode Parse(Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "while");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode predicate = ExpressionNode.Parse(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode whileBody = StatementNode.Parse(parser);
            if (parser.AcceptToken(TokenType.Identifier, "else"))
            {
                AstNode elseBody = StatementNode.Parse(parser);
                return new WhileNode(pos, predicate, whileBody, elseBody);
            }

            return new WhileNode(pos, predicate, whileBody);
        }
    }
}

