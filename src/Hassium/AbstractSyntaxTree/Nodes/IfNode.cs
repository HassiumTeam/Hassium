using System;

namespace Hassium
{
    public class IfNode: AstNode
    {
        public AstNode Predicate
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
        public AstNode ElseBody
        {
            get
            {
                return this.Children[2];
            }
        }

        public IfNode(AstNode predicate, AstNode body)
        {
         
            this.Children.Add(predicate);
            this.Children.Add(body);
            this.Children.Add(new CodeBlock());
        }

        public IfNode(AstNode predicate, AstNode body, AstNode elseBody)
        {
            this.Children.Add(predicate);
            this.Children.Add(body);
            this.Children.Add(elseBody);
        }

        public static AstNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "if");
            parser.ExpectToken(TokenType.Parentheses, "(");
            AstNode predicate = ExpressionNode.Parse(parser);
            parser.ExpectToken(TokenType.Parentheses, ")");
            AstNode ifBody = StatementNode.Parse(parser);
            if (parser.AcceptToken(TokenType.Identifier, "else"))
            {
                AstNode elseBody = StatementNode.Parse(parser);
                return new IfNode(predicate, ifBody, elseBody);
            }

            return new IfNode(predicate, ifBody);
        }
    }
}

