using Hassium.Lexer;

namespace Hassium.Parser.Ast
{
    public class CodeBlock: AstNode
    {
        public CodeBlock(int codePos) : base(codePos)
        {
        }

        public static AstNode Parse(Parser parser)
        {
            CodeBlock block = new CodeBlock(parser.codePos);
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

