using System;
using System.Collections.Generic;
using System.Text;

using Hassium.Lexer;

namespace Hassium.Parser
{
    public class FuncNode: AstNode
    {
        public class Parameter
        {
            public bool IsEnforced { get; private set; }
            public string Name { get; private set; }
            public string Type { get; private set; }
            public Parameter(string name)
            {
                IsEnforced = false;
                Name = name;
            }
            public Parameter(string name, string type)
            {
                IsEnforced = true;
                Name = name;
                Type = type;
            }
            public override string ToString()
            {
                return Type == null ? Name : string.Format("{0} : {1}", Name, Type);
            }
        }

        public string Name { get; private set; }
        public List<Parameter> Parameters { get; private set; }
        public string SourceRepresentation { get; private set; }

        public FuncNode(string name, List<Parameter> parameters, AstNode body, string sourceRepresentation, SourceLocation location)
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
            parser.ExpectToken(TokenType.LeftParentheses);

            List<Parameter> parameters = new List<Parameter>();
            if (!parser.AcceptToken(TokenType.RightParentheses))
            {
                while (!parser.AcceptToken(TokenType.RightParentheses))
                {
                    string paramName = parser.ExpectToken(TokenType.Identifier).Value;
                    if (parser.AcceptToken(TokenType.Colon))
                        parameters.Add(new Parameter(paramName, parser.ExpectToken(TokenType.Identifier).Value));
                    else
                        parameters.Add(new Parameter(paramName));
                    parser.AcceptToken(TokenType.Comma);
                }
            }
            AstNode body = StatementNode.Parse(parser);

            StringBuilder sourceRepresentation = new StringBuilder(string.Format("func {0} ({1}", name, parameters.Count != 0 ? parameters[0].ToString() : ""));
            for (int i = 1; i < parameters.Count; i++)
                sourceRepresentation.Append(", " + parameters[i].ToString());
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

