using System;

namespace Hassium
{
    public class CodeBlock: AstNode
    {
        public static AstNode Parse(Parser.Parser parser)
        {
            CodeBlock block = new CodeBlock();
            parser.ExpectToken(TokenType.Brace, "{");

            while (!parser.EndOfStream && !parser.MatchToken(TokenType.Brace, "}"))
            {
                block.Children.Add(StatementNode.Parse(parser));
            }

            parser.ExpectToken(TokenType.Brace, "}");
            return block;
        }
    }
}

