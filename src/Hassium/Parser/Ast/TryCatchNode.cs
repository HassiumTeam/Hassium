using System;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class TryCatchNode: AstNode
    {
        public AstNode TryBody { get { return Children[0]; } }
        public AstNode CatchBody { get { return Children[1]; } }
        
        public TryCatchNode(AstNode tryBody, AstNode catchBody, SourceLocation location)
        {
            Children.Add(tryBody);
            Children.Add(catchBody);
            this.SourceLocation = location;
        }

        public static TryCatchNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "try");
            parser.ExpectToken(TokenType.LeftBrace);
            AstNode tryBody = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.RightBrace);
            parser.ExpectToken(TokenType.Identifier, "catch");
            parser.ExpectToken(TokenType.LeftBrace);
            AstNode catchBody = StatementNode.Parse(parser);
            parser.ExpectToken(TokenType.RightBrace);

            return new TryCatchNode(tryBody, catchBody, parser.Location);
        }

        public override void Visit(IVisitor visitor)
        {
            visitor.Accept(this);
        }
        public override void VisitChildren(IVisitor visitor)
        {
            foreach (AstNode child in Children)
                child.Visit(visitor);
        }
    }
}

