using System;
using System.Collections.Generic;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class MultiFuncNode: AstNode
    {
        public string Name { get; private set; }
        public MultiFuncNode(string name, SourceLocation location)
        {
            Name = name;
            this.SourceLocation = location;
        }

        public static MultiFuncNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "MultiFunc");
            MultiFuncNode multiFunc = new MultiFuncNode(parser.ExpectToken(TokenType.Identifier).Value, parser.Location);
            parser.ExpectToken(TokenType.LeftBrace);
            while (!parser.AcceptToken(TokenType.RightBrace))
                multiFunc.Children.Add(LambdaNode.Parse(parser));
            return multiFunc;
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

