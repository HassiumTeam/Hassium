using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hassium.Parser.Ast
{
    public class LambdaFuncNode : AstNode
    {
        public List<string> Parameters { get; private set; }

        public AstNode Body
        {
            get
            {
                return Children[0];
            }
        }

        public LambdaFuncNode(List<string> paramaters, AstNode body)
        {
            Parameters = paramaters;
            Children.Add(body);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "lambda");
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

            if (parser.AcceptToken(TokenType.EndOfLine)) parser.ExpectToken(TokenType.EndOfLine);

            return new LambdaFuncNode(result, body);
        }

        public static explicit operator FuncNode (LambdaFuncNode funcNode)
        {
            return new FuncNode("", funcNode.Parameters, funcNode.Body);
        }
    }
}
