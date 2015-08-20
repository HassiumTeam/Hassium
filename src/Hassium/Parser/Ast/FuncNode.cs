using System;

namespace Hassium
{
    public class FuncNode: AstNode
    {
        public string Name { get; private set; }
        public AstNode Arguments
        {
            get
            {
                return this.Children[0];
            }
        }
        public AstNode Body
        {
            get
            {
                return this.Children[1];
            }
        }

        public FuncNode(string name, AstNode arguments, AstNode body)
        {
            this.Name = name;
            this.Children.Add(arguments);
            this.Children.Add(body);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier);
            string name = parser.ExpectToken(TokenType.Identifier).Value;
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode arguments = ArgListNode.Parse(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode body = StatementNode.Parse(parser);

            return new FuncNode(name, arguments, body);
        }
    }
}

