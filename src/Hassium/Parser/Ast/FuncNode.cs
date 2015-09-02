using System.Collections.Generic;
using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class FuncNode: AstNode
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }

        public AstNode Body
        {
            get
            {
                return Children[0];
            }
        }

        public FuncNode(int position, string name, List<string> parameters, AstNode body)
        {
            Parameters = parameters;
            Name = name;
            Children.Add(body);
        }

        public static AstNode Parse(Hassium.Parser.Parser parser)
        {
            int pos = parser.codePos;

            parser.ExpectToken(TokenType.Identifier, "func");
            string name = parser.ExpectToken(TokenType.Identifier).Value.ToString();
            parser.ExpectToken(TokenType.Parentheses, "(");

            List<string> result = new List<string>();
            while (parser.MatchToken(TokenType.Identifier))
            {
                result.Add(parser.ExpectToken(TokenType.Identifier).Value.ToString());
                if (!parser.AcceptToken(TokenType.Comma))
                    break;
            }

            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode body = StatementNode.Parse(parser);

            return new FuncNode(pos, name, result, body);
        }

        public static explicit operator LambdaFuncNode(FuncNode funcNode)
        {
            return new LambdaFuncNode(funcNode.Position, funcNode.Parameters, funcNode.Body);
        }
    }
}

