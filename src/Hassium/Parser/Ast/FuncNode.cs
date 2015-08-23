using System;
using System.Collections.Generic;

namespace Hassium
{
    public class FuncNode: AstNode
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }

        public AstNode Body
        {
            get
            {
                return this.Children[0];
            }
        }

        public FuncNode(string name, List<string> paramaters, AstNode body)
        {
            this.Parameters = paramaters;
            this.Name = name;
            this.Children.Add(body);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier);
            string name = parser.ExpectToken(TokenType.Identifier).Value;
            parser.ExpectToken(TokenType.Parentheses, "(");

            List<string> result = new List<string>();
            while (parser.MatchToken(TokenType.Identifier))
            {
                result.Add(parser.ExpectToken(TokenType.Identifier).Value);
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode body = StatementNode.Parse(parser);

            return new FuncNode(name, result, body);
        }
    }
}

