using System;
using System.Collections.Generic;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class LambdaNode: AstNode
    {
        public List<string> Parameters { get; private set; }
        public AstNode Body { get { return Children[0]; } }
        public LambdaNode(List<string> parameters, AstNode body, SourceLocation location)
        {
            Parameters = parameters;
            Children.Add(body);
            this.SourceLocation = location;
        }

        public static LambdaNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "lambda");
            List<string> parameters = new List<string>();
            ArgListNode args = ArgListNode.Parse(parser);
            foreach (AstNode child in args.Children)
                parameters.Add(((IdentifierNode)child).Identifier);
            AstNode body = StatementNode.Parse(parser);

            return new LambdaNode(parameters, body, parser.Location);
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

