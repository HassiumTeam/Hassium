using System;

namespace Hassium
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

        public ClassNode(string name, AstNode body)
        {
            this.Children.Add(body);
            this.Name = name;
        }

        public static AstNode Parse(Parser.Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "class");
            string name = parser.ExpectToken(TokenType.Identifier).Value.ToString();
            AstNode body = StatementNode.Parse(parser);

            return new ClassNode(name, body);
        }
    }
}

