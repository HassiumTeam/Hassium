using System;
using System.Collections.Generic;
using System.Text;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class FuncNode: AstNode
    {
        public string Name { get; private set; }
        public List<string> Parameters { get; private set; }
        public string SourceRepresentation { get; private set; }

        public FuncNode(string name, List<string> parameters, AstNode body, string sourceRepresentation, SourceLocation location)
        {
            Name = name;
            Parameters = parameters;
            Children.Add(body);
            SourceRepresentation = sourceRepresentation;
            this.SourceLocation = location;
        }

        public static FuncNode Parse(Parser parser)
        {
            parser.ExpectToken(TokenType.Identifier, "func");
            string name = parser.ExpectToken(TokenType.Identifier).Value;
            List<string> parameters = new List<string>();
            ArgListNode args = ArgListNode.Parse(parser);
            foreach (AstNode child in args.Children)
                parameters.Add(((IdentifierNode)child).Identifier);
            AstNode body = StatementNode.Parse(parser);
            StringBuilder sourceRepresentation = new StringBuilder(string.Format("func {0} ({1}", name, parameters.Count != 0 ? parameters[0] : ""));
            for (int i = 1; i < parameters.Count; i++)
                sourceRepresentation.Append(", " + parameters[i]);
            sourceRepresentation.Append(")");

            return new FuncNode(name, parameters, body, sourceRepresentation.ToString(), parser.Location);
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

