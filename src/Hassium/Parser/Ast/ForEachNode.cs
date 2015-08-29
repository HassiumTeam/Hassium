using System;
using Hassium.Parser.Ast;

namespace Hassium
{
    public class ForEachNode : AstNode
    {
        public AstNode Needle
        {
            get
            {
                return Children[0];
            }
        }

        public AstNode Haystack
        {
            get
            {
                return Children[1];
            }
        }

        public AstNode Body
        {
            get
            {
                return Children[2];
            }
        }

        public ForEachNode(AstNode needle, AstNode haystack, AstNode body)
        {
            Children.Add(needle);
            Children.Add(haystack);
            Children.Add(body);
        }

        public static AstNode Parse(Parser.Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "foreach");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode needle = null;
            if (parser.CurrentToken().Value.ToString() == "[") needle = ArrayInitializerNode.Parse(parser);
            else needle = IdentifierNode.Parse(parser);
            parser.ExpectToken(TokenType.Identifier, "in");
            AstNode haystack = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode forBody = StatementNode.Parse(parser);

            return new ForEachNode(needle, haystack, forBody);
        }
    }
}

