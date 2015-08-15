using System;

namespace Hassium
{
    public class CodeBlock: AstNode
    {
        public static AstNode Parse(Parser parser)
        {
            CodeBlock block = new CodeBlock();
            parser.ExpectToken(TokenType.Bracket, "{");

            while (!parser.EndOfStream)
            {
                block.Children.Add(StatementNode.Parse(parser));
            }

            parser.ExpectToken(TokenType.Bracket, "}");
            return parser;
        }
    }
}

