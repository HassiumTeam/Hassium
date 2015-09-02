using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class ClassNode: AstNode
    {
        public string Name { get; private set; }

        public AstNode Body
        {
            get
            {
                return this.Children[0];
            }
        }

        public ClassNode(int position, string name, AstNode body) : base(position)
        {
            this.Children.Add(body);
            this.Name = name;
        }

        public static AstNode Parse(Hassium.Parser.Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "class");
            string name = parser.ExpectToken(TokenType.Identifier).Value.ToString();
            AstNode body = StatementNode.Parse(parser);

            return new ClassNode(pos, name, body);
        }
    }
}

